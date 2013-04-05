// ListOfTransactionsPanel.cs
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
using CoreUtilities;
using System.Collections.Generic;
using Layout;
using Transactions;

namespace Worklog
{
	public class ListOfTransactionsPanel : Panel
	{
		#region GUI
		ListBox EventList = null;
		RichTextBox Summary=null;
		#endregion

		#region variables
		string _GUID=Constants.BLANK;
		Action<string> RefreshPanels=null;
		Func<DateTime> CurrentDate=null;
		#endregion

		public string SummaryText {

			set{ Summary.Text = value;}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="Worklog.ListOfTransactionsPanel"/> class.
		/// 
		/// This panel contains:
		/// 
		/// - a listbox showing events assigned to this 
		/// - A summary of the week's activity ON THIS layout
		/// - An Add button
		/// - Ability to edit existing entries from the listbox
		/// </summary>
		public ListOfTransactionsPanel (string LayoutGUID, Action<string> _RefreshPanels, Func<DateTime> _CurrentDate,Func<bool> _BringFront)
		{
			_GUID = LayoutGUID;
			CurrentDate = _CurrentDate;

			RefreshPanels = _RefreshPanels;
			EventList = new ListBox();
			EventList.DoubleClick+= HandleTransactionListDoubleClick;
			EventList.Dock = DockStyle.Fill;

			Button Add = new Button();
			Add.Text = Loc.Instance.GetString ("Add Worklog");
			Add.Dock = DockStyle.Top;
			Add.Click += HandleAddClick;

			 Summary = new RichTextBox();
			Summary.Click+= (object sender, EventArgs e) => _BringFront();
			Summary.Height = 200;
			Summary.Dock = DockStyle.Top;
			Summary.ReadOnly = true;

			this.Controls.Add(EventList);
			this.Controls.Add (Add);
			this.Controls.Add (Summary);
		}

		void HandleTransactionListDoubleClick (object sender, EventArgs e)
		{
			// Edit an existing transaction entry
			if (EventList.SelectedItem != null) {

				string IDOfRowForTransaction = EventList.SelectedValue.ToString ();

				TransactionWorkLog foundNote = (TransactionWorkLog)LayoutDetails.Instance.TransactionsList.GetExisting (new database.ColumnConstant[2] 
			                          {TransactionsTable.TYPE, TransactionsTable.ID}, new string[2] {
				TransactionsTable.T_USER.ToString (),
				IDOfRowForTransaction.ToString ()
			});

				if (foundNote != null) {
					int Words = foundNote.Words;
					int Minutes = foundNote.Minutes;
					string Category =foundNote.Category;
					string Notes = foundNote.Notes;
					AddEditWorkForm AddEdit = new AddEditWorkForm (Words, Minutes, Category, Notes);


					if (AddEdit.ShowDialog () == DialogResult.OK) {
						//	TransactionWorkLog Work = new TransactionWorkLog(oldDate, this._GUID, AddEdit.Note, AddEdit.Words, AddEdit.Minutes, AddEdit.CategoryText);
			

						// if in add mode we Add a new entry
						//LayoutDetails.Instance.TransactionsList.UpdateEvent(new TransactionWorkLog(DateTime.Now, _GUID,  AddEdit.Note, AddEdit.Words, AddEdit.Minutes, AddEdit.CategoryText));
						foundNote.Words = AddEdit.Words;
						foundNote.Minutes = AddEdit.Minutes;
						foundNote.Notes = AddEdit.Note;
						foundNote.Category = AddEdit.CategoryText;
						LayoutDetails.Instance.TransactionsList.UpdateEvent (foundNote);

							UpdateScreen ();

					}
				} else {
					NewMessage.Show (Loc.Instance.GetStringFmt ("Unusual Error... unable to retrieve this transaction. ID = {0}", IDOfRowForTransaction));
				}
			}
		}

		void UpdateScreen ()
		{

			if (null != RefreshPanels) {
				RefreshPanels ("");
			}

		}

		void AddEntry ()
		{
			AddEditWorkForm AddEdit = new AddEditWorkForm ();
			if (AddEdit.ShowDialog () == DialogResult.OK) {

				if (CurrentDate != null)
				{
				// if in add mode we Add a new entry
				LayoutDetails.Instance.TransactionsList.AddEvent(new TransactionWorkLog(CurrentDate(), _GUID, 
				                                                                        AddEdit.Note, AddEdit.Words, AddEdit.Minutes, AddEdit.CategoryText));
				}
				else
				{
					NewMessage.Show ("A date was not passed into the AddEntry method.");
				}

					UpdateScreen ();

			}
		}

		void HandleAddClick (object sender, EventArgs e)
		{
			AddEntry();
		}

		public void BuildList ()
		{
			if (null == LayoutDetails.Instance.TransactionsList) {
				throw new Exception("Transaction Table not created yet");
			}
			try {
				List<Transactions.TransactionBase> LayoutEvents = LayoutDetails.Instance.TransactionsList.GetEventsForLayoutGuid (_GUID, " and type='5' ");
				if (LayoutEvents != null)
				{
				LayoutEvents.Sort ();
				LayoutEvents.Reverse ();
				if (LayoutEvents != null) {
					EventList.DataSource = LayoutEvents;
					EventList.DisplayMember = "Display";
						EventList.ValueMember = "ID";
				}
				}
				else
				{
					//TODO: Remove this message
					NewMessage.Show ("Transaction list for this note was empty. Remove me after debugging.");
				}
			} catch (Exception ex) {
				NewMessage.Show (ex.ToString());
			}
		}
	}
}

