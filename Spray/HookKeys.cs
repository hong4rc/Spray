using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Spray {
    internal class HookKeys {
        private readonly NativeMethods.LowLevelProc _kProc, _mProc;
        private IntPtr _hookMouseId = IntPtr.Zero;
        private IntPtr _hookKeyboardId = IntPtr.Zero;
        private bool _shiftHold;
        private readonly SprayThread _thread;


        public readonly Keys KeyTurn = Keys.Tab;

        public HookKeys() {
            _kProc = HookKeyboardCb;
            _mProc = HookMouseCb;

            _thread = new SprayThread();
        }

        public void SetHook() {
            UnHook();
            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule) {
                _hookKeyboardId = NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD_LL, _kProc,
                    NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
                _hookMouseId = NativeMethods.SetWindowsHookEx(NativeMethods.WH_MOUSE_LL, _mProc,
                    NativeMethods.GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public void UnHook() {
            if (_hookKeyboardId != IntPtr.Zero) {
                NativeMethods.UnhookWindowsHookEx(_hookKeyboardId);
            }

            if (_hookMouseId != IntPtr.Zero) {
                NativeMethods.UnhookWindowsHookEx(_hookMouseId);
            }
        }

        public void StopThread() {
            _thread.Exit();
        }

        private IntPtr HookKeyboardCb(int nCode, IntPtr wParam, IntPtr lParam) {
            if (nCode < 0) return NativeMethods.CallNextHookEx(_hookKeyboardId, nCode, wParam, lParam);
            var key = (Keys) Marshal.ReadInt32(lParam);
            if ((uint) wParam == NativeMethods.WM_KEYDOWN || (uint) wParam == NativeMethods.WM_SYSKEYDOWN) {
                if (key == Keys.LShiftKey || key == Keys.RShiftKey) {
                    _shiftHold = true;
                }
            }
            else if ((uint) wParam == NativeMethods.WM_KEYUP || (uint) wParam == NativeMethods.WM_SYSKEYUP) {
                if (key == Keys.LShiftKey || key == Keys.RShiftKey) {
                    _shiftHold = false;
                }

                if (key == KeyTurn) {
                    Toggle();
                }

                if (_shiftHold && key >= Keys.D1 && key < Keys.D1 + SettingsFm.Guns.Length) {
                    Program.SelectGun(key - Keys.D1);
                }
            }

            return NativeMethods.CallNextHookEx(_hookKeyboardId, nCode, wParam, lParam);
        }

        private static void Toggle() {
            Program.IsRun = !Program.IsRun;
        }

        private IntPtr HookMouseCb(int nCode, IntPtr wParam, IntPtr lParam) {
            if (!Program.IsRun || nCode < 0) {
                return NativeMethods.CallNextHookEx(_hookMouseId, nCode, wParam, lParam);
            }

            if ((uint) wParam == NativeMethods.WM_LBUTTONDOWN) {
                _thread.Start();
            }

            if ((uint) wParam != NativeMethods.WM_LBUTTONUP) {
                return NativeMethods.CallNextHookEx(_hookMouseId, nCode, wParam, lParam);
            }

            _thread.Stop();
            _thread.Count = 0;

            return NativeMethods.CallNextHookEx(_hookMouseId, nCode, wParam, lParam);
        }
    }
}
