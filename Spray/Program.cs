using System;
using System.Threading;
using System.Windows.Forms;


namespace Spray
{
	static class Program
	{

		private static SettingsFm form;
		private static HookKeys hook;
		private static String windowName = "Spray";

		private static Mutex mutex = new Mutex(true, windowName);
		[STAThread]
		static void Main()
		{


			if (mutex.WaitOne(TimeSpan.Zero, true))
			{
				mutex.ReleaseMutex();

			}
			else
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			form = new SettingsFm();
			hook = new HookKeys();
			hook.SetHook();
			Application.Run(form);
			hook.stopThread();
			hook.UnHook();

		}

		public static bool IsRun {
			get { return form.IsRun; }
			set { form.IsRun = value; }
		}
		public static bool OnlyCs {
			get { return form.OnlyCs; }
			set { form.OnlyCs = value; }
		}

		public static void SelectGun(int index)
		{
			form.tickGun(index);
		}
	}
}
