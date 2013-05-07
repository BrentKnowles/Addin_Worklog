// AddEditWorkForm.cs
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
using Layout;
using System.Collections.Generic;
namespace Worklog
{
	public class AddEditWorkForm : Form
	{

		#region gui
		Button ok = null;
		TextBox WordsEdit= null;
		TextBox MinutesEdit = null;
		ComboBox Category = null;
		RichTextBox Notes = null;
		#endregion

		#region properties
		public string Note {
			get{ return Notes.Rtf ;}

		}
		public int Words {
			get{ return Int32.Parse (WordsEdit.Text) ;}
			
		}
		public int Minutes {
			get{ return Int32.Parse (MinutesEdit.Text) ;}
			
		}
		public string CategoryText {
			get{ return Category.Text ;}
			
		}
		#endregion

		public void CommonConstructor ()
		{
			this.Height = 350;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			// I can't use the proper RespondToAction thing I did for pictures
			// becauise this is not invoked from REspond To Action!
			// this *hack* might just be the way I need to this
			this.Icon = LayoutDetails.Instance.MainFormIcon;
			FormUtils.SizeFormsForAccessibility (this, LayoutDetails.Instance.MainFormFontSize);
			
			
			ok = new Button ();
			ok.Text = Loc.Instance.GetString ("OK");
			ok.DialogResult = System.Windows.Forms.DialogResult.OK;
			ok.Dock = DockStyle.Left;
			ok.Width = LayoutDetails.ButtonHeight;
			ok.Enabled = false;
			
			Button cancel = new Button ();
			cancel.Text = Loc.Instance.GetString ("Cancel");
			cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			cancel.Dock = DockStyle.Right;
			
			
			Panel buttons = new Panel ();
			buttons.Height = LayoutDetails.ButtonHeight;
			
			
			buttons.Controls.Add (ok);
			buttons.Controls.Add (cancel);
			buttons.Dock = DockStyle.Bottom;
			
			
			Label Words = new Label ();
			Words.Text = Loc.Instance.GetString ("Words");



			WordsEdit = new TextBox ();
			WordsEdit.Text = "0";
			WordsEdit.TextChanged += HandleTextChanged;
			
			
			Label Minutes = new Label ();
			Minutes.Text = Loc.Instance.GetString ("Minutes");
			
			MinutesEdit = new TextBox ();
			MinutesEdit.Text = "0";
			MinutesEdit.TextChanged += HandleTextChanged;


			Category = new ComboBox ();
			List<string> allitems = LayoutDetails.Instance.TableLayout.GetListOfStringsFromSystemTable (NoteDataXML_Worklog.SYSTEM_WORKLOGCATEGORY, 1);
			//Category.DataSource = allitems;
			foreach (string s in allitems) {
				Category.Items.Add (s);
			}
			Category.DropDownStyle = ComboBoxStyle.DropDownList;
			
			Notes = new RichTextBox();
			Notes.Text= "";




			
			Words.Dock = DockStyle.Top;
			WordsEdit.Dock = DockStyle.Top;
			
			Minutes.Dock = DockStyle.Top;
			MinutesEdit.Dock = DockStyle.Top;
			
			
			Category.Dock = DockStyle.Top;
			Notes.Dock = DockStyle.Top;
			
			this.Controls.Add (buttons);
			
			
			this.Controls.Add (Notes);
			this.Controls.Add (Category);
			this.Controls.Add (MinutesEdit);
			this.Controls.Add (Minutes);
			
			this.Controls.Add (WordsEdit);
			this.Controls.Add (Words);


			// Tabs
			WordsEdit.TabIndex = 0;
			//WordsEdit.TabStop = true;
			MinutesEdit.TabIndex = 1;
			Category.TabIndex = 2;
			Notes.TabIndex = 3;
			buttons.TabIndex = 4;
			cancel.TabIndex = 5;
			ok.TabIndex = 6;

		}

		public AddEditWorkForm (int words, int minutes, string category, string notes)
		{
			CommonConstructor ();

			WordsEdit.Text = words.ToString ();
			MinutesEdit.Text = minutes.ToString ();
			try {
				Notes.Rtf = notes;
			} catch (Exception) {
				Notes.Text = notes;
			}
			//NewMessage.Show ("Setting to " + category);
			for (int i = 0; i < Category.Items.Count; i++) {
				string s = Category.Items [i].ToString ();
			//	NewMessage.Show("s = " + s + " = category " + category);
				if (s == category) {
					Category.SelectedIndex = i;
				}
			}
			//Category.SelectedValue = category;

		}


		public AddEditWorkForm ()
		{


			CommonConstructor();



		}

		void HandleTextChanged (object sender, EventArgs e)
		{
			// test to see if it is valid. If not we disable OK
			int value = 0;
			bool validNumber = Int32.TryParse (((sender as TextBox).Text), out value);
			ok.Enabled = validNumber;

		}
	}
}

