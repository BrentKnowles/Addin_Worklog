// TotalProgressPanel.cs
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

namespace Worklog
{
	public class TotalProgressPanel : Panel
	{
		RichTextBox ProgressPanel = null;
		/// <summary>
		/// Initializes a new instance of the <see cref="Worklog.TotalProgressPanel"/> class.
		/// 
		/// This panel dispalys the: 
		//     Minutes This Week: 150 (~2 hours) 
		//     Words This Week: 3100

		/// </summary>
		public TotalProgressPanel (Func<bool> _BringFront)
		{
			 ProgressPanel = new RichTextBox();
			ProgressPanel.Click+= (object sender, EventArgs e) => _BringFront();
			ProgressPanel.Dock = DockStyle.Fill;
			ProgressPanel.ReadOnly = true;
			this.Controls.Add (ProgressPanel);
		}

		public void SetProgressPanel(string Text)
		{
			ProgressPanel.Text = Text;
		}

		public void UpdateAppearance (Layout.AppearanceClass app)
		{
			ProgressPanel.Font = app.captionFont;
			ProgressPanel.BackColor = app.mainBackground;
			ProgressPanel.ForeColor =  app.secondaryForeground;

		}
	}
}

