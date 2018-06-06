using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace Spray {
    class SprayThread {
        private const String Csgo = "Counter-Strike: Global Offensive";
        private const int DelaySpray = 98;
        public int Count;
        private INPUT _input;

        private int rdX = 0, rdY = 0;
        private bool _spraying;
        private bool _runThread = true;
        private readonly EventWaitHandle _signal;

        public SprayThread() {
            _signal = new EventWaitHandle(false, EventResetMode.AutoReset);
            Thread t = new Thread(() => {
                while (_runThread) {
                    if (_spraying == false) {
                        _signal.WaitOne();
                    }

                    if (!Program.IsRun) {
                        continue;
                    }

                    if (Program.OnlyCs && NativeMethods.GetActiveWindowTitle() != Csgo) {
                        continue;
                    }

                    Move();
                    Thread.Sleep(DelaySpray);
                }
            });
            t.Start();
        }

        public void Stop() {
            Count = 0;
            _spraying = false;
            _signal.Reset();
        }

        public void Start() {
            _spraying = true;
            _signal.Set();
        }

        public void Exit() {
            _runThread = false;
            _signal.Set();
            _signal.Close();
        }

        void Move() {
            int x = 0, y = 0;
            if (Count < SettingsFm.NowGun.Pattern.Length / 2) {
                x = SettingsFm.NowGun.Pattern[Count, 0];
                y = SettingsFm.NowGun.Pattern[Count, 1];
            }

            Count++;
            _input.type = NativeMethods.INPUT_MOUSE;
            _input.mi.mouseData = 0;
            _input.mi.time = 0;

            x = x - rdX;
            y = y - rdY;
            //rdX = rand() % 4 - 2;
            //rdY = rand() % 4 - 2;
            x = x - rdX;
            y = y - rdY;
            _input.mi.dx = x;
            _input.mi.dy = y;
            _input.mi.dwFlags = NativeMethods.MOUSEEVENTF_MOVE;

            List<INPUT> keyList = new List<INPUT> {
                _input
            };
            NativeMethods.SendInput(1, keyList.ToArray(), Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
