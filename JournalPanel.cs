// JournalPanel.cs
//
// Copyright (c) 2013 Brent Knowles (http://www.brentknowles.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// Review documentation at http://www.yourothermind.com for updated implementation notes, license updates
// or other general information/
// 
// Author information available at http://www.brentknowles.com or http://www.amazon.com/Brent-Knowles/e/B0035WW7OW
// Full source code: https://github.com/BrentKnowles/YourOtherMind
//###
using System;
using System.Windows.Forms;
using Transactions;
using Layout;
using CoreUtilities;
using System.Drawing;
namespace Worklog
{
	public class JournalPanel : Panel
	{

		#region GUI
		ListOfTransactionsPanel TransactionsPanel=null;
		dashboardDayTime dayTime=null;
		TotalProgressPanel ProgressPanel = null;
		Button YearsReport = null;

		#endregion
		#region variables
		string GUID = Constants.BLANK;
		Func<bool> BringFront = null;
		#endregion
		public JournalPanel (string LayoutGUID, Func<bool> _BringFront)
		{
			BringFront = _BringFront;
			if (Constants.BLANK == LayoutGUID) throw new Exception("A non-valid GUID was passed into the Journal Panel");
			GUID = LayoutGUID;


			Panel LeftSide = new Panel();
			LeftSide.Click+= (object sender, EventArgs e) => BringFront();

			YearsReport = new Button();
			YearsReport.Text = Loc.Instance.GetString ("Report");
			YearsReport.Click+= HandleYearsReportClick;
		//	dashboardDayLog dayLog = new dashboardDayLog();
			//this.Controls.Add (dayLog);
			dayTime = new dashboardDayTime(RefreshPanels);

			TransactionsPanel = new ListOfTransactionsPanel(LayoutGUID, RefreshPanels,CurrentDate, BringFront);
			ProgressPanel = new TotalProgressPanel(BringFront);





			LeftSide.Controls.Add (dayTime);
			LeftSide.Controls.Add (ProgressPanel);
			LeftSide.Controls.Add (YearsReport);
			ProgressPanel.BringToFront();

			this.Controls.Add (LeftSide);
			this.Controls.Add (TransactionsPanel);


			dayTime.Dock = DockStyle.Top;
			ProgressPanel.Dock = DockStyle.Fill;
			YearsReport.Dock = DockStyle.Bottom;

			LeftSide.Dock = DockStyle.Left;
			TransactionsPanel.Dock = DockStyle.Fill;
			TransactionsPanel.BringToFront();


			dayTime.SetDate();
			//dayLog.Dock = DockStyle.Fill;
			//dayLog.BringToFront();
		}
		
		/// <summary>
		///  BuildReport("Work History", "Years", 20, "Minutes", "Words", "Finished", "Retired" 
		///   , "Georgia", 
		/// 
		/// You run this after assigning the text to the richtext and then it does a formatting pass over it
		/// </summary>
		/// <param name="sTitle"></param>
		/// <param name="sMainColumnTitle"></param>
		/// <param name="nSpacing"></param>
		/// <param name="cols"></param>
		static public void BuildReport(RichTextBox richText, string sTitle, string sMainColumnTitle, int nSpacing, string[] cols
		                               , string sFontFamily)
		{
			richText.SelectAll();
			richText.SelectionFont = new Font("Courier New", 12);
			
			// select and format title
			richText.SelectionStart = 0;
			richText.SelectionLength = sTitle.Length; // bold title
			General.FormatRichText(richText, FontStyle.Bold);
			richText.SelectionFont = new Font("Courier New", 18);
			
			// undelrine title
			int nLength = richText.Text.IndexOf(cols[0]);
			richText.SelectionStart = sTitle.Length;
			richText.SelectionLength = nLength - sTitle.Length;
			General.FormatRichText(richText, FontStyle.Underline);
			General.FormatRichText(richText, FontStyle.Bold);
			
			
			// bold minutes
			
			// do twice to catch BOTH months
			
			for (int j = 1; j <= 2; j++)
			{
				int nTemp = 0;
				foreach (string s in cols)
				{
					nTemp = richText.Text.IndexOf(s, nLength);
					if (nTemp > -1)
					{
						
						
						
						richText.SelectionStart = nTemp;
						richText.SelectionLength = s.Length;
						if (richText.SelectionFont.Bold == false)
						{
							General.FormatRichText(richText, FontStyle.Bold);
						}
					}
				}
				nLength = nTemp + 1;
			}
			
			richText.SelectionLength = 0;
		}
		void HandleYearsReportClick (object sender, EventArgs e)
		{
			GenericTextForm Report = LayoutDetails.Instance.GetTextFormToUse();
			Report.GetRichTextBox().Font = new Font("Courier New", 12);
			//Report.GetRichTextBox().Text = LayoutDetails.Instance.TransactionsList.QueryMonthsInYearReport(2013,01);;


			this.Cursor = Cursors.WaitCursor;
			

			// how to figure out which years available? Another function with an int array of years? Then do a foreach?
			// int[] nYear = new int[4]  {2006, 2007, 2008, 2009};
			int[] nYear = LayoutDetails.Instance.TransactionsList.GetWorkHistoryYears();
			int nSpacing = 10;
			int InteriorSpace = 7;
			string sDisplay = Loc.Instance.GetStringFmt("Work History\r\n\r\n{0}", General.properspaces(Loc.Instance.GetString ("Year"), nSpacing));
			
			foreach (int ny in nYear)
			{
				// build header
				sDisplay = sDisplay + General.properspaces(ny.ToString(),InteriorSpace);
				
			}
			
			
			sDisplay = String.Format("{0}\r\n{1}", sDisplay, General.properspaces(Loc.Instance.GetString ("Minutes"), nSpacing));
			// Total Submissions
			foreach (int ny in nYear)
			{
				//hours
				string minutes = LayoutDetails.Instance.TransactionsList.Query(TransactionsTable.DATA3, ny, "type=5");
				if (Constants.BLANK == minutes)
				{
					minutes = "0";
				}

				sDisplay =  sDisplay + General.properspaces(String.Format("{0}", minutes), InteriorSpace);
			}

			
			sDisplay = String.Format("{0}\r{1}", sDisplay, General.properspaces(Loc.Instance.GetString ("Words"), nSpacing));
			string sFInished ="";// "\t";
			string sRetired ="";// = "\t";
			string sAdded ="";// "\t";
			string sSubmissions ="";//= "\t";
			
			string sMaxWordsDay = "";//"\t";
			
			string sNags = "";//"\t";
			
			// Total Submissions
			foreach (int ny in nYear)
			{
				string words= LayoutDetails.Instance.TransactionsList.Query(TransactionsTable.DATA4, ny, "type=5");
				if (Constants.BLANK == words)
				{
					words = "0";
				}

				sDisplay = sDisplay + General.properspaces(String.Format("{0}", words), InteriorSpace);

				string s = General.properspaces(LayoutDetails.Instance.TransactionsList.QueryCount(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_FINISHED),false), InteriorSpace);

				sFInished = String.Format("{0}{1}", sFInished, s);

				s =General.properspaces(LayoutDetails.Instance.TransactionsList.QueryCount(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_RETIRED),false), InteriorSpace);

				sRetired = String.Format("{0}{1}", sRetired, s);
				s = General.properspaces(LayoutDetails.Instance.TransactionsList.QueryCount(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_ADDED),false), InteriorSpace);
				sAdded = String.Format("{0}{1}", sAdded, s);
				s = General.properspaces(LayoutDetails.Instance.TransactionsList.QueryCount(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionSubmission.T_SUBMISSION),false), InteriorSpace);
				sSubmissions = String.Format("{0}{1}", sSubmissions, s);
				
				// maxes
				s=General.properspaces(LayoutDetails.Instance.TransactionsList.QueryMax(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_USER), false, TransactionsTable.DATA4), InteriorSpace);
				sMaxWordsDay = String.Format("{0}{1}", sMaxWordsDay, s);
				s =General.properspaces(LayoutDetails.Instance.TransactionsList.QueryCount(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_NAGINTERRUPTED),false), InteriorSpace);
				sNags = String.Format("{0}{1}", sNags, s);

			}
			
			// Part 2 - Finished and Subs
			sDisplay = String.Format("{0}\r\n{1}{2}", sDisplay, General.properspaces("Finished", nSpacing), sFInished);
			                         sDisplay = String.Format("{0}\r\n{1}{2}", sDisplay, General.properspaces("Retired", nSpacing), sRetired);
			                         sDisplay = String.Format("{0}\r\n{1}{2}", sDisplay, General.properspaces("Added", nSpacing), sAdded);
			                         sDisplay = String.Format("{0}\r\n{1}{2}", sDisplay, General.properspaces("Subs", nSpacing),sSubmissions);
			
			                         sDisplay = String.Format("{0}\r\n{1}{2}", sDisplay, General.properspaces("Best Day", nSpacing),sMaxWordsDay);
			                         sDisplay = String.Format("{0}\r\n{1}{2}", sDisplay, General.properspaces("Nags", nSpacing), sNags);
			
			
			// Part 3 - month breakdown, simple - CURRENT MONTH
			sDisplay = sDisplay + "\r\n\r\nMonthly Breakdown For: " + QueryMonthsInYearReport(CurrentDate().Year, CurrentDate().Month);
			
			sDisplay = sDisplay + "\r\n\r\nLast Month Breakdown For: " + QueryMonthsInYearReport(CurrentDate().AddDays(-31).Year, CurrentDate().AddDays(-31).Month);
			
			
			Report.GetRichTextBox().Text  = sDisplay;
			
			// the array that follows determines which fields are bold texted
			BuildReport(Report.GetRichTextBox(), "Work History", "Years", 20, new string[18] {   "Minutes", "Words", "Finished", "Retired" , 
				"Monthly Breakdown For:", "Added", "Submissions", "Words:", "Minutes:", "Finished:", "Retired:", "Added:", "Submissions:",  "Best Day", "Distractions", "Max Words in One Day", "Last Month Breakdown For:","Distracted"}, "Georgia");
			
			
			
			
			
		
			
			this.Cursor = Cursors.Default;

			Report.ShowDialog();

		}
		/// <summary>
		/// returns a month by month breakdown of progress
		/// 
		/// will include both Hours, Words, FInished and retired, formatted like
		/// 
		///   January 2008
		///     Minutes: 203  (F_DATA3)
		///     Words: 1200 (F_DATA4)
		///     Finished: 0
		///     Retired: 1
		///  
		/// Example usage
		/// </summary>
		/// <param name="nYear"></param>
		/// <returns></returns>
		public string QueryMonthsInYearReport(int nYear, int nMonth)
		{
			//
			string nMinutes = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA3, nYear, String.Format("{0}='{1}'", TransactionsTable.TYPE, TransactionsTable.T_USER), nMonth,0);
			string Words = "0";
			string Added = "0";
			string AddedSub = "0";
			string Finished = "0";
			string Retired = "0";
			string Nag = "0";
			string MaxWords = "0";
			int minutes = 0;
			int hours = 0;
			
			try
			{
				minutes = Int32.Parse(nMinutes);
				hours = (int)(minutes / 60);
				nMinutes = String.Format("{0} (~{1} hours)", nMinutes, hours.ToString());
				
				
				
				Words = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA4, nYear, String.Format("{0}='{1}'", TransactionsTable.TYPE, TransactionsTable.T_USER), nMonth,0);
				Added = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA1_LAYOUTGUID, nYear, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_ADDED), nMonth,0);
				
				AddedSub = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA1_LAYOUTGUID, nYear, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionSubmission.T_SUBMISSION), nMonth,0);
				
				Finished = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA1_LAYOUTGUID, nYear, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_FINISHED), nMonth,0);
				
				Retired = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA1_LAYOUTGUID, nYear, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_RETIRED), nMonth,0);
				Nag = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA1_LAYOUTGUID, nYear, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_NAGINTERRUPTED), nMonth,0);
				MaxWords = LayoutDetails.Instance.TransactionsList.QueryMonthInYear(TransactionsTable.DATA4, nYear, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_USER), nMonth, 1);
			}
			catch (Exception)
			{
				minutes = 0;
				hours = 0;
				nMinutes = "0";
			}
			
			
			DateTime date = new DateTime(nYear, nMonth, 1);
			
			
			string sValue =
				String.Format("{0}\r\nMinutes: {1} \r\nWords: {2}\r\nFinished: {3}\r\nRetired: {4}\r\nAdded: {5} \r\nSubmissions: {6} \r\nMax Words in One Day: {7} \r\nDistracted: {8}",
				              date.ToString("MMMM"), nMinutes, Words, Finished, Retired, Added, AddedSub, MaxWords, Nag);
			return sValue;
			
		}
/// <summary>
/// Gets the currents Date as set in the dateime control
/// </summary>
/// <returns>
/// The date.
/// </returns>
		private DateTime CurrentDate()
		{
			return dayTime.currentDate;
		}

		// These are specific to a GUID, but still time-limited
		public  string GetWorkStats_SpecificLayout(DateTime daytouse, string GUID)
		{
			//DateTime todaysDate = DateTime.Today();
			
			
			string ExtraFilter =  String.Format("{0}='{1}' and {2}='{3}'", TransactionsTable.TYPE, TransactionsTable.T_USER, TransactionsTable.DATA1_LAYOUTGUID, GUID);
			
			string nMinutes =  LayoutDetails.Instance.TransactionsList.QueryLastWeek(daytouse, TransactionsTable.DATA3, ExtraFilter,false);
			
			int minutes = 0;
			int hours = 0;
			LayoutDetails.Instance.TransactionsList.GetHoursAndMinutes(nMinutes, out minutes, out hours);
			nMinutes = Loc.Instance.GetStringFmt("{0} (~{1} hours)", minutes.ToString(), hours.ToString());
			
			string Words =  LayoutDetails.Instance.TransactionsList.QueryLastWeek(daytouse, TransactionsTable.DATA4, ExtraFilter,false);
			if (Words.IndexOf("null") > -1) Words = "0"; // sometimes they show null compute
			
			
			string ProjectName = MasterOfLayouts.GetNameFromGuid(GUID);
			
			string sResult = Loc.Instance.GetStringFmt("WORKLOG - {3} (THIS WEEK){1}Minutes Worked: {0} {1}Words Written: {2}{1}", nMinutes, Environment.NewLine, Words, ProjectName);
			
			
			// Now Get 'All Time Stats' for this layout
			nMinutes =  LayoutDetails.Instance.TransactionsList.QueryLastWeek(daytouse, TransactionsTable.DATA3, ExtraFilter, true);
			LayoutDetails.Instance.TransactionsList.GetHoursAndMinutes(nMinutes, out minutes, out hours);
			nMinutes = Loc.Instance.GetStringFmt("{0} (~{1} hours)", minutes.ToString(), hours.ToString());
			Words =  LayoutDetails.Instance.TransactionsList.QueryLastWeek(daytouse, TransactionsTable.DATA4, ExtraFilter,true);
			if (Words.IndexOf("null") > -1) Words = "0"; // sometimes they show null compute
			
			sResult = String.Format ("{0}{1}{4} (ALL TIME){1}Minutes Worked: {2} {1}Words Written: {3}", sResult, Environment.NewLine, nMinutes, Words, ProjectName);
			
			
			return sResult;
			
		}
		/// <summary>
		///  get week states for current week
		/// </summary>
		/// <param name="daytouse">Will build the week based off of this date</param>
		/// <returns></returns>
		public  string GetWeekStats(DateTime daytouse)
		{
			//DateTime todaysDate = DateTime.Today();
			
			string nMinutes = LayoutDetails.Instance.TransactionsList.QueryLastWeek(daytouse, TransactionsTable.DATA3, String.Format("{0}='{1}'", TransactionsTable.TYPE,
			                                                                                                                         TransactionsTable.T_USER),false);
			
			int minutes = 0;
			int hours = 0;
			LayoutDetails.Instance.TransactionsList.GetHoursAndMinutes(nMinutes, out minutes, out hours);
			nMinutes = Loc.Instance.GetStringFmt("{0} (~{1} hours)", minutes.ToString(), hours.ToString());
			
			string Words = LayoutDetails.Instance.TransactionsList.QueryLastWeek(daytouse, TransactionsTable.DATA4, String.Format("{0}='{1}'", TransactionsTable.TYPE, 
			                                                                                                                      TransactionsTable.T_USER),false);
			if (Words.IndexOf("null") > -1) Words = "0"; // sometimes they show null compute
			
			string sResult = Loc.Instance.GetStringFmt("TOTAL WORK THIS WEEK{1}Minutes Worked: {0} {1}Words Written: {2}", nMinutes, Environment.NewLine, Words);
			return sResult;
			
		}
		public void RefreshPanels (string extra)
		{

			// extra not used but added because I think I might use it (also needed for delegate)
			string value = GetWeekStats(CurrentDate());
			ProgressPanel.SetProgressPanel(value);
			TransactionsPanel.SummaryText = GetWorkStats_SpecificLayout(CurrentDate(), GUID); //+ " " + CurrentDate ();
			if (null == TransactionsPanel) {
				throw new Exception("Journal Panel: Transaction Panel not made yet.");
			}
			TransactionsPanel.BuildList();
		}

		/// <summary>
		/// Updates the appearance, using the AppearanceClass object
		/// </summary>
		/// <param name='app'>
		/// App.
		/// </param>
		public void UpdateAppearance (AppearanceClass app)
		{
			YearsReport.BackColor = app.captionBackground;
			YearsReport.ForeColor = app.captionForeground;

			TransactionsPanel.UpdateAppearance(app);
			dayTime.UpdateAppearance(app);
			ProgressPanel.UpdateAppearance(app);
		}
	}
}

