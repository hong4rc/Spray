using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Spray
{
	public partial class SettingsFm : Form
	{
		private static String strStopped = "This app is stopped.";
		private static String strRunning = "This app is running.";
		private static bool closeToTray = true;
		public static Gun[] Guns = { Gun.Ak47, Gun.M4a4, Gun.M4a1s, Gun.Gali, Gun.Famas, Gun.Ump45, Gun.Aug, Gun.Sg };
		private List<RadioButton> rbs = new List<RadioButton>();
		public static Gun NowGun = Guns[0];

		public void updateCb(bool b)
		{
			Console.WriteLine("updateCb " + b);
			cbRunning.Checked = b;
		}
		public bool IsRun {
			get { return cbRunning.Checked; }
			set { cbRunning.Checked = value; }
		}
		
		public bool OnlyCs {
			get { return cbOnlyCs.Checked; }
			set { cbOnlyCs.Checked = value; }
		}
		public SettingsFm()
		{
			InitializeComponent();
			initForm();
			initGun();

		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);

			if (e.CloseReason == CloseReason.WindowsShutDown) return;
			Console.WriteLine(e.CloseReason);
			// Confirm user wants to close
			e.Cancel = closeToTray && true;
			ntIcon.Visible = true;
			this.Hide();
		}


		private void initForm()
		{
			ContextMenu ctxMn = new ContextMenu();

			ntIcon.ContextMenu = ctxMn;

			MenuItem menuItem = new MenuItem();
			menuItem.Index = 0;
			menuItem.Text = "E&xit";
			menuItem.Click += menuExit;

			ctxMn.MenuItems.Add(menuItem);

			updateText();
		}
		private void initGun()
		{
			for (int i = 0; i < Guns.Length; i++)
			{
				Gun gun = Guns[i];
				RadioButton rb = new RadioButton();

				rb.Text = gun.Name;
				rb.Location = new Point(20, 30 + 20 * i);
				rb.CheckedChanged += gunChange;

				rbs.Add(rb);
				GunBox.Controls.Add(rb);
			}
			GunBox.Size = new Size(200, 24 * Guns.Length + 30);
			rbs[0].Checked = true;
		}
		public void tickGun(int a)
		{
			if (a >= Guns.Length || a < 0)
			{
				a = 0;
			}
			rbs[a].Checked = true;
		}
		private void menuExit(object sender, EventArgs e){
			closeToTray = false;
			this.Close();
		}
		private void gunChange(object sender, EventArgs e)
		{
			RadioButton tCb = sender as RadioButton;
			if (tCb == null || !tCb.Checked)
			{
				return;
			}
			NowGun = Guns[rbs.IndexOf(tCb)];
			Console.WriteLine("Changed to " + NowGun.Name);
		}

		private void cbRunning_CheckedChanged(object sender, EventArgs e)
		{
			updateText();
		}
		private void updateText()
		{
			if (cbRunning.Checked)
			{
				cbRunning.Text = strRunning;
				Console.Beep(1000, 200);
			}
			else
			{
				cbRunning.Text = strStopped;
				Console.Beep(700, 50);
			};
		}

		private void ntIcon_DoubleClick(object sender, EventArgs e)
		{
			NotifyIcon nc = sender as NotifyIcon;
			nc.Visible = false;
			this.WindowState = FormWindowState.Normal;
			this.Show();
		}
	}
}
