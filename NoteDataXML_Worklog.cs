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

