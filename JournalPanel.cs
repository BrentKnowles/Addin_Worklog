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
			Button YearsReport = new Button();
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
				s = General.properspaces(LayoutDetails.Instance.TransactionsList.QueryCount(ny, String.Format("{0}={1}", TransactionsTable.TYPE, TransactionsTable.T_SUBMISSION),false), InteriorSpace);
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
			sDisplay = sDisplay + "\r\n\r\nMonthly Breakdown For: " + LayoutDetails.Instance.TransactionsList.QueryMonthsInYearReport(CurrentDate().Year, CurrentDate().Month);
			
			sDisplay = sDisplay + "\r\n\r\nLast Month Breakdown For: " + LayoutDetails.Instance.TransactionsList.QueryMonthsInYearReport(CurrentDate().AddDays(-31).Year, CurrentDate().AddDays(-31).Month);
			
			
			Report.GetRichTextBox().Text  = sDisplay;
			
			// the array that follows determines which fields are bold texted
			BuildReport(Report.GetRichTextBox(), "Work History", "Years", 20, new string[18] {   "Minutes", "Words", "Finished", "Retired" , 
				"Monthly Breakdown For:", "Added", "Submissions", "Words:", "Minutes:", "Finished:", "Retired:", "Added:", "Submissions:",  "Best Day", "Distractions", "Max Words in One Day", "Last Month Breakdown For:","Distracted"}, "Georgia");
			
			
			
			
			
		
			
			this.Cursor = Cursors.Default;

			Report.ShowDialog();

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

		public void RefreshPanels (string extra)
		{

			// extra not used but added because I think I might use it (also needed for delegate)
			string value = LayoutDetails.Instance.TransactionsList.GetWeekStats(CurrentDate());
			ProgressPanel.SetProgressPanel(value);
			TransactionsPanel.SummaryText =  LayoutDetails.Instance.TransactionsList.GetWorkStats_SpecificLayout(CurrentDate(), GUID); //+ " " + CurrentDate ();
			if (null == TransactionsPanel) {
				throw new Exception("Journal Panel: Transaction Panel not made yet.");
			}
			TransactionsPanel.BuildList();
		}
	}
}

