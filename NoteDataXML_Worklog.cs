// NoteDataXML_Worklog.cs
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
using Layout;
using CoreUtilities;
using System.Windows.Forms;
namespace Worklog
{
	public class NoteDataXML_Worklog: NoteDataXML
	{
		public NoteDataXML_Worklog () : base()
		{

		}
		public override int defaultHeight {
			get {
				return 600;
			}
		}
		public override int defaultWidth {
			get {
				return 600;
			}
		}
		
	public NoteDataXML_Worklog (int height, int width):base(height, width)
		{

		}
		protected override void CommonConstructorBehavior ()
		{
			base.CommonConstructorBehavior ();
			Caption = Loc.Instance.GetString("Worklog");
		}

		protected bool BringFrontWrapper ()
		{
			// want to pass an action into the subpanels
			BringToFrontAndShow();
			return true;
		}

		protected override void DoBuildChildren (LayoutPanelBase Layout)
		{
			base.DoBuildChildren (Layout);
			properties.DropDownItems.Add (new ToolStripSeparator ());
			CaptionLabel.Dock = DockStyle.Top;
		
			JournalPanel Journal = new JournalPanel(this.Layout.GUID, BringFrontWrapper);
			Journal.Dock = DockStyle.Fill;
			//Journal.Click+= (object sender, EventArgs e) => BringToFrontAndShow();

			ParentNotePanel.Controls.Add (Journal);

			Journal.BringToFront();
			Journal.RefreshPanels("");
		
		}


		public override void Save ()
		{
			base.Save ();
		}

	}

}

