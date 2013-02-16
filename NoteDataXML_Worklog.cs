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
		public override void CreateParent (Layout.LayoutPanelBase Layout)
		{
			base.CreateParent (Layout);
			properties.DropDownItems.Add (new ToolStripSeparator ());
			CaptionLabel.Dock = DockStyle.Top;
		
			JournalPanel Journal = new JournalPanel(this.Layout.GUID);
			Journal.Dock = DockStyle.Fill;

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

