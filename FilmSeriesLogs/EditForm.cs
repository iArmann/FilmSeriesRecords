﻿using FilmSeriesLogsDb;
using System;
using System.Windows.Forms;

namespace FilmSeriesLogs
{
	public partial class EditForm : Form
	{
		public bool Saved;
		public Series Series { get; }
		private bool _isFavorite;
		private bool IsFavorite
		{
			get => _isFavorite;
			set
			{
				_isFavorite = value;
				if (value)
					SetStarFilledIcon();
				else
					SetStarIcon();
			}
		}
		private readonly SeriesDb db;
		public EditForm(Series series, SeriesDb db)
		{
			InitializeComponent();
			Series = series;
			this.db = db;
			this.IsFavorite = false;
		}

		private void EditForm_Shown(object sender, EventArgs e)
		{
			SetDataFromDb();
		}
		#region Helper
		private void SetDataFromDb()
		{
			txtboxName.Text = Series.Name;
			comboBoxStatus.SelectedIndex = Series.Status.ToCheckState().ToComboBoxItem();
			numericUpDownSeasons.Value = Series.Seasons;
			if (Series.Schedule != null)
			{
				numericUpDownScheduleSeasons.Value = Series.Schedule.Season;
				numericUpDownScheduleEpisode.Value = Series.Schedule.Episode;
				if (Series.Schedule.InterruptionTime.HasValue)
					dateTimePickerInterruptionTime.Value = Series.Schedule.InterruptionTime.Value;
				if (Series.Schedule.WhenNextShowStarts.HasValue)
				{
					dateTimePickerShowStartsAtDate.Value = Series.Schedule.WhenNextShowStarts.Value;
					dateTimePickerShowStartsAtTime.Value = Series.Schedule.WhenNextShowStarts.Value;
				}
			}
			if (Series.Detail != null)
			{
				IsFavorite = Series.Detail.IsFavorite;
				richTextBoxGenres.Text = Series.Detail.Genres;
				richTextBoxDescription.Text = Series.Detail.Description;
				numericUpDownTimesWatched.Value = Series.Detail.TimesWatched;
			}
		}
		private void SetDataFromFormToObject()
		{
			Series.Name = txtboxName.Text;
			Series.Status = comboBoxStatus.ToCheckState().ToSeenState();
			Series.Seasons = (ushort)numericUpDownSeasons.Value;

			if (Series.Schedule == null) Series.Schedule = new SeriesSchedule();

			Series.Schedule.Season = (ushort)numericUpDownScheduleSeasons.Value;
			Series.Schedule.Episode = (ushort)numericUpDownScheduleEpisode.Value;
			Series.Schedule.InterruptionTime = dateTimePickerInterruptionTime.Value;

			var date = dateTimePickerShowStartsAtDate.Value;
			var time = dateTimePickerShowStartsAtTime.Value.TimeOfDay;

			Series.Schedule.WhenNextShowStarts = new DateTime(
				date.Year, date.Month, date.Day,
				time.Hours, time.Minutes, time.Seconds);

			if (Series.Detail == null) Series.Detail = new SeriesDetail();

			Series.Detail.Genres = richTextBoxGenres.Text.Trim();
			Series.Detail.IsFavorite = IsFavorite;
			Series.Detail.Description = richTextBoxDescription.Text.Trim();
			Series.Detail.TimesWatched = (ushort)numericUpDownTimesWatched.Value;
		}
		private void SetStarIcon() =>
			btnFavorite.Image = Properties.Resources.icon_star.ToBitmap();
		private void SetStarFilledIcon() =>
			btnFavorite.Image = Properties.Resources.icon_star_filled.ToBitmap();
		private void SetSaveIcon() =>
			btnSave.Image = Properties.Resources.icon_save48.ToBitmap();
		private void SetSavedIcon() =>
			btnSave.Image = Properties.Resources.icon_save_close48.ToBitmap();
		#endregion
		private void btnSave_Click(object sender, EventArgs e)
		{
			SetDataFromFormToObject();
			if (db.Update(Series))
			{
				SetSavedIcon();
				timerAnimateSavedNotification.Start();
				Saved = true;
			}
			else
			{
				MessageBox.Show("Series wasn't found!", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void EditForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
			{
				btnSave.PerformClick();
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
			if (e.KeyCode == Keys.Escape)
			{
				Close();
				e.Handled = true;
				e.SuppressKeyPress = true;
			}
		}
		private void timerAnimateSavedNotification_Tick(object sender, EventArgs e)
		{
			SetSaveIcon();
			timerAnimateSavedNotification.Stop();
		}
		private void btnFavorite_Click(object sender, EventArgs e) => IsFavorite = !IsFavorite;
		private void btnResetScheduleStartsAt_Click(object sender, EventArgs e)
		{
			dateTimePickerShowStartsAtDate.Value = DateTime.Today;
			dateTimePickerShowStartsAtTime.Value = DateTime.Today;
		}
		private void btnResetScheduleInterruptionTime_Click(object sender, EventArgs e) =>
			dateTimePickerInterruptionTime.Value = DateTime.Today.Date;
	}
}
