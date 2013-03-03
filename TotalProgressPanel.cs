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
	}
}

