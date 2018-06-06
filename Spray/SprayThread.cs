using System;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;

namespace Spray
{
	class SprayThread
	{
		private static String csgo = "Counter-Strike: Global Offensive";
		public int count = 0;
		private INPUT input;

		private int rdX = 0, rdY = 0;
		private bool spraying = false;
		private bool runThread = true;
		private EventWaitHandle signal;

		public SprayThread()
		{
			signal = new EventWaitHandle(false, EventResetMode.AutoReset);
			Thread t = new Thread(()=>
			{
				Console.WriteLine("Thread: started");
				while (runThread)
				{
					if (spraying == false)
					{
						signal.WaitOne();
					}
					if (Program.IsRun)
					{
						if (!Program.OnlyCs || NativeMethods.GetActiveWindowTitle() == csgo)
						{
							Move(0, 10);
							Thread.Sleep(98);
						}
					}
				}

				Console.WriteLine("Thread: stopped");
			});
			t.Start();
		}
		public void stop()
		{
			count = 0;
			spraying = false;
			signal.Reset();
		}
		public void start()
		{
			spraying = true;
			signal.Set();
		}
		public void exit()
		{
			runThread = false;
			signal.Set();
			signal.Close();
		}
		void Move(int x, int y)
		{
			if (count < SettingsFm.NowGun.Pattern.Length / 2)
			{
				x = SettingsFm.NowGun.Pattern[count, 0];
				y = SettingsFm.NowGun.Pattern[count, 1];
			}
			else
			{
				x = 0;
				y = 0;
			}

			Console.WriteLine("x = " + x + ", y = " + y);
			count++;
			input.type = NativeMethods.INPUT_MOUSE;
			input.mi.mouseData = 0;
			input.mi.time = 0;

			x = x - rdX;
			y = y - rdY;
			//rdX = rand() % 4 - 2;
			//rdY = rand() % 4 - 2;
			x = x - rdX;
			y = y - rdY;
			input.mi.dx = x;
			input.mi.dy = y;
			input.mi.dwFlags = NativeMethods.MOUSEEVENTF_MOVE;



			List<INPUT> keyList = new List<INPUT>();
			keyList.Add(input);
			NativeMethods.SendInput(1, keyList.ToArray(), Marshal.SizeOf(typeof(INPUT)));
		}
	}
}
