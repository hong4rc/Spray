using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Spray
{
	public class NativeMethods
	{
		public delegate IntPtr LowLevelProc(int nCode, IntPtr wParam, IntPtr lParam);

		public const uint WH_KEYBOARD_LL = 13;
		public const uint WH_MOUSE_LL = 14;

		public const uint WM_KEYDOWN = 0x100;
		public const uint WM_KEYUP = 0x101;
		public const uint WM_SYSKEYDOWN = 0x0104;
		public const uint WM_SYSKEYUP = 0x0105;

		public const uint WM_LBUTTONDOWN = 0x201;
		public const uint WM_LBUTTONUP = 0x202;

		public const uint WM_CHAR = 0x102;
		public const int MK_LBUTTON = 0x01;
		public const int VK_RETURN = 0x0d;
		public const int VK_ESCAPE = 0x1b;
		public const int VK_TAB = 0x09;
		public const int VK_LEFT = 0x25;
		public const int VK_UP = 0x26;
		public const int VK_RIGHT = 0x27;
		public const int VK_DOWN = 0x28;
		public const int VK_F5 = 0x74;
		public const int VK_F6 = 0x75;
		public const int VK_F7 = 0x76;

		public const int INPUT_MOUSE = 0;
		public const int INPUT_KEYBOARD = 1;
		public const int INPUT_HARDWARE = 2;
		public const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
		public const uint KEYEVENTF_KEYUP = 0x0002;
		public const uint KEYEVENTF_UNICODE = 0x0004;
		public const uint KEYEVENTF_SCANCODE = 0x0008;
		public const uint XBUTTON1 = 0x0001;
		public const uint XBUTTON2 = 0x0002;
		public const uint MOUSEEVENTF_MOVE = 0x0001;
		public const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
		public const uint MOUSEEVENTF_LEFTUP = 0x0004;
		public const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
		public const uint MOUSEEVENTF_RIGHTUP = 0x0010;
		public const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
		public const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
		public const uint MOUSEEVENTF_XDOWN = 0x0080;
		public const uint MOUSEEVENTF_XUP = 0x0100;
		public const uint MOUSEEVENTF_WHEEL = 0x0800;
		public const uint MOUSEEVENTF_VIRTUALDESK = 0x4000;
		public const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("user32.dll")]
		public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out()] byte[] lpBuffer, int dwSize, out int lpNumberOfBytesRead);

		[DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr SetWindowsHookEx(uint idHook, LowLevelProc lpfn, IntPtr hMod, uint dwThreadId);
		
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string lpModuleName);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);


		public static string GetActiveWindowTitle()
		{
			const int nChars = 256;
			StringBuilder Buff = new StringBuilder(nChars);
			IntPtr handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, nChars) > 0)
			{
				return Buff.ToString();
			}
			return null;
		}

		public static void SwitchWindow(IntPtr windowHandle)
		{
			if (GetForegroundWindow() == windowHandle)
				return;

			IntPtr foregroundWindowHandle = GetForegroundWindow();
			uint currentThreadId = GetCurrentThreadId();
			uint temp;
			uint foregroundThreadId = GetWindowThreadProcessId(foregroundWindowHandle, out temp);
			AttachThreadInput(currentThreadId, foregroundThreadId, true);
			SetForegroundWindow(windowHandle);
			AttachThreadInput(currentThreadId, foregroundThreadId, false);

			while (GetForegroundWindow() != windowHandle)
			{
			}
		}

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

		[DllImport("User32.Dll", EntryPoint = "PostMessageA")]
		public static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

		[DllImport("user32.dll")]
		public static extern byte VkKeyScan(char ch);

		[DllImport("user32.dll")]
		public static extern uint MapVirtualKey(uint uCode, uint uMapType);

		public static IntPtr FindWindow(string name)
		{
			Process[] procs = Process.GetProcesses();
			foreach (Process proc in procs)
			{
				if (proc.ProcessName == name)
				{
					return proc.MainWindowHandle;
				}
			}

			return IntPtr.Zero;
		}

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);


		public static int MakeLong(int low, int high)
		{
			return (high << 16) | (low & 0xffff);
		}

		[DllImport("user32.dll")]
		public static extern IntPtr GetMessageExtraInfo();
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MOUSEINPUT
	{
		public int dx;
		public int dy;
		public uint mouseData;
		public uint dwFlags;
		public uint time;
		IntPtr dwExtraInfo;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct KEYBDINPUT
	{
		public ushort wVk;
		public ushort wScan;
		public uint dwFlags;
		public uint time;
		public IntPtr dwExtraInfo;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct HARDWAREINPUT
	{
		uint uMsg;
		ushort wParamL;
		ushort wParamH;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct INPUT
	{
		[FieldOffset(0)]
		public int type;
		[FieldOffset(4)] //*
		public MOUSEINPUT mi;
		[FieldOffset(4)] //*
		public KEYBDINPUT ki;
		[FieldOffset(4)] //*
		public HARDWAREINPUT hi;
	}

	[StructLayout(LayoutKind.Sequential)]

	public struct POINT
	{
		public int x;
		public int y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MSLLHOOKSTRUCT
	{
		public POINT pt;
		public uint mouseData;
		public uint flags;
		public uint time;
		public IntPtr dwExtraInfo;
	}

}
