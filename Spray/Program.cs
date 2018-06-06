using System;
using System.Threading;
using System.Windows.Forms;


namespace Spray {
    internal static class Program {
        private static SettingsFm _form;
        private static HookKeys _hook;
        private const string WindowName = "Spray";

        private static readonly Mutex Mutex = new Mutex(true, WindowName);

        [STAThread]
        private static void Main() {
            if (Mutex.WaitOne(TimeSpan.Zero, true)) {
                Mutex.ReleaseMutex();
            } else {
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            _form = new SettingsFm();
            _hook = new HookKeys();
            _hook.SetHook();
            Application.Run(_form);
            _hook.StopThread();
            _hook.UnHook();
        }

        public static bool IsRun {
            get => _form.IsRun;
            set => _form.IsRun = value;
        }

        public static bool OnlyCs => _form.OnlyCs;

        public static void SelectGun(int index) {
            _form.TickGun(index);
        }
    }
}
