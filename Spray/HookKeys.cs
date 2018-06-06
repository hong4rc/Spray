using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Spray
{
	class HookKeys
	{
		private NativeMethods.LowLevelProc _kProc, _mProc;
		private IntPtr _hookMouseID = IntPtr.Zero;
		private IntPtr _hookKeyboardID = IntPtr.Zero;
		private bool ShiftHold = false;
		private IntPtr threadId = IntPtr.Zero;
		private SprayThread thread;


		public Keys keyTurn = Keys.Tab;

		public HookKeys()
		{
			_kProc = HookKeyboardCb;
			_mProc = HookMouseCb;

			thread = new SprayThread();
		}
		public void SetHook()
		{
			UnHook();
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule)
			{
				_hookKeyboardID = NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD_LL, _kProc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
				_hookMouseID = NativeMethods.SetWindowsHookEx(NativeMethods.WH_MOUSE_LL, _mProc, NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
			}
		}
		
		public void UnHook()
		{
			if (_hookKeyboardID != IntPtr.Zero)
			{
				NativeMethods.UnhookWindowsHookEx(_hookKeyboardID);
			}
			if (_hookMouseID != IntPtr.Zero)
			{
				NativeMethods.UnhookWindowsHookEx(_hookMouseID);
			}
		}
		public void stopThread()
		{
			thread.exit();
		}
		private IntPtr HookKeyboardCb(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0)
			{
				Keys key = (Keys)Marshal.ReadInt32(lParam);
				switch ((uint)wParam)
				{
					case NativeMethods.WM_KEYDOWN:
					case NativeMethods.WM_SYSKEYDOWN:
						switch (key)
						{
							case Keys.LShiftKey:
							case Keys.RShiftKey:
								ShiftHold = true;
								break;

						}
						break;
					case NativeMethods.WM_KEYUP:
					case NativeMethods.WM_SYSKEYUP:
						switch (key)
						{
							case Keys.LShiftKey:
							case Keys.RShiftKey:
								ShiftHold = false;
								break;
						}
						if (key == keyTurn)
						{
							toggle();
						}
						if (ShiftHold && key >= Keys.D1 && key < Keys.D1 + SettingsFm.Guns.Length)
						{
							Program.SelectGun(key - Keys.D1);
						}
						break;
					default:
						Console.WriteLine("wParam = " + wParam);
						break;

				}
			}

			return NativeMethods.CallNextHookEx(_hookKeyboardID, nCode, wParam, lParam);
		}
		private void toggle()
		{
			Program.IsRun = !Program.IsRun;
			Console.WriteLine("toogle -> " + Program.IsRun);
		}
		private IntPtr HookMouseCb(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (Program.IsRun && nCode >= 0)
			{
				if ((uint)wParam == NativeMethods.WM_LBUTTONDOWN)
				{
					thread.start();
					Console.WriteLine("spraying = true");
				}
				if ((uint)wParam == NativeMethods.WM_LBUTTONUP)
				{
					thread.stop();
					thread.count = 0;
					Console.WriteLine("spraying = false");
				}
			}
			return NativeMethods.CallNextHookEx(_hookMouseID, nCode, wParam, lParam);

		}
	}
}
