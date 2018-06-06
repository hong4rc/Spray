namespace Spray
{
	partial class SettingsFm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsFm));
			this.GunBox = new System.Windows.Forms.GroupBox();
			this.cbRunning = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cbExitToTray = new System.Windows.Forms.CheckBox();
			this.cbOnlyCs = new System.Windows.Forms.CheckBox();
			this.ntIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GunBox
			// 
			this.GunBox.Location = new System.Drawing.Point(29, 107);
			this.GunBox.Name = "GunBox";
			this.GunBox.Size = new System.Drawing.Size(275, 280);
			this.GunBox.TabIndex = 0;
			this.GunBox.TabStop = false;
			this.GunBox.Text = "Choose gun";
			// 
			// cbRunning
			// 
			this.cbRunning.AutoSize = true;
			this.cbRunning.Location = new System.Drawing.Point(29, 27);
			this.cbRunning.Name = "cbRunning";
			this.cbRunning.Size = new System.Drawing.Size(98, 21);
			this.cbRunning.TabIndex = 1;
			this.cbRunning.Text = "cbRunning";
			this.cbRunning.UseVisualStyleBackColor = true;
			this.cbRunning.CheckedChanged += new System.EventHandler(this.cbRunning_CheckedChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cbExitToTray);
			this.panel1.Controls.Add(this.cbOnlyCs);
			this.panel1.Controls.Add(this.GunBox);
			this.panel1.Controls.Add(this.cbRunning);
			this.panel1.Location = new System.Drawing.Point(12, 12);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(337, 390);
			this.panel1.TabIndex = 2;
			// 
			// cbExitToTray
			// 
			this.cbExitToTray.AutoSize = true;
			this.cbExitToTray.Location = new System.Drawing.Point(198, 27);
			this.cbExitToTray.Name = "cbExitToTray";
			this.cbExitToTray.Size = new System.Drawing.Size(106, 21);
			this.cbExitToTray.TabIndex = 3;
			this.cbExitToTray.Text = "Exit To Tray";
			this.cbExitToTray.UseVisualStyleBackColor = true;
			// 
			// cbOnlyCs
			// 
			this.cbOnlyCs.AutoSize = true;
			this.cbOnlyCs.Location = new System.Drawing.Point(29, 67);
			this.cbOnlyCs.Name = "cbOnlyCs";
			this.cbOnlyCs.Size = new System.Drawing.Size(160, 21);
			this.cbOnlyCs.TabIndex = 2;
			this.cbOnlyCs.Text = "Just run onle CS:GO";
			this.cbOnlyCs.UseVisualStyleBackColor = true;
			// 
			// ntIcon
			// 
			this.ntIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("ntIcon.Icon")));
			this.ntIcon.Text = "ntIcon";
			this.ntIcon.DoubleClick += new System.EventHandler(this.ntIcon_DoubleClick);
			// 
			// SettingsFm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(377, 414);
			this.Controls.Add(this.panel1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "SettingsFm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Settings";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GunBox;
		private System.Windows.Forms.CheckBox cbRunning;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox cbOnlyCs;
		private System.Windows.Forms.NotifyIcon ntIcon;
		private System.Windows.Forms.CheckBox cbExitToTray;
	}
}
