﻿
using FilmSeriesLogsDb;

namespace FilmSeriesLogs
{
	partial class FilteredForm
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
			this.userControlDGV = new FilmSeriesLogs.UserControlDGV();
			this.SuspendLayout();
			// 
			// userControlDGV
			// 
			this.userControlDGV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userControlDGV.Location = new System.Drawing.Point(0, 0);
			this.userControlDGV.Name = "userControlDGV";
			this.userControlDGV.Size = new System.Drawing.Size(565, 317);
			this.userControlDGV.TabIndex = 0;
			// 
			// FilteredForm
			// 
			this.ClientSize = new System.Drawing.Size(565, 317);
			this.Controls.Add(this.userControlDGV);
			this.KeyPreview = true;
			this.Name = "FilteredForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Search result";
			this.Shown += new System.EventHandler(this.FilteredForm_Shown);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FilteredForm_KeyDown);
			this.ResumeLayout(false);

		}

		#endregion

		private UserControlDGV userControlDGV;
	}
}