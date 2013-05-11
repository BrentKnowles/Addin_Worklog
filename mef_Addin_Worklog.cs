// mef_Addin_Worklog.cs
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

// Purpose: Adds the YourOtherMindMarkup + FindInNote + FactInNote

namespace MefAddIns
{
	using MefAddIns.Extensibility;
	using System.ComponentModel.Composition;
	using System;
	//using System.Windows.Forms;
	using CoreUtilities;
	using System.IO;
	using System.Collections.Generic;
	using Worklog;
	/// <summary>
	/// Provides an implementation of a supported language by implementing ISupportedLanguage. 
	/// Moreover it uses Export attribute to make it available thru MEF framework.
	/// </summary>
	[Export(typeof(mef_IBase))]
	public class Addin_Worklog : PlugInBase, mef_IBase
	{
		#region variables
		
		
#endregion
		public Addin_Worklog()
		{
			guid = "worklog";
		}
		
		public string Author
		{
			get { return @"Brent Knowles"; }
		}
		public string Version
		{
			// 1.0.1 - creating worklog table on system page here instead of in main application
			get { return @"1.0.1.0"; }
		}
		public string Description
		{
			get { return "A new notetype that gives a simple journal to update time worked on projects ."; }
		}
		public string Name
		{
			get { return @"Worklog"; }
		}
		
		
		public override bool DeregisterType ()
		{
			

			return true;
			//Layout.LayoutDetails.Instance.AddToList(typeof(NoteDataXML_Picture.NoteDataXML_Pictures), "Picture");
		}
		
		//		public override int TypeOfInformationNeeded {
		//			get {
		//				return (int)GetInformationADDINS.GET_CURRENT_LAYOUT_PANEL;
		//			}
		//		}
		//		public override void SetBeforeRespondInformation (object neededInfo)
		//		{
		//			CurrentPanel = (LayoutPanel)neededInfo;
		//		}
		
		//		mef_IBase myAddInOnMainFormForHotKeys = null;
		//		Action<mef_IBase> myRunnForHotKeys=null;
		//		public override void AssignHotkeys (ref List<HotKeys.KeyData> Hotkeys, ref mef_IBase addin, Action<mef_IBase> Runner)
		//		{
		//			
		//			base.AssignHotkeys (ref Hotkeys, ref addin, Runner);
		//			myAddInOnMainFormForHotKeys = addin;
		//			myRunnForHotKeys=Runner;
		//			Hotkeys.Add (new KeyData (Loc.Instance.GetString ("Picture Capture"), HotkeyAction, Keys.Control, Keys.P, Constants.BLANK, true, "pictureguid"));
		//			
		//		}
		
		//		public void HotkeyAction(bool b)
		//		{
		//			if (myRunnForHotKeys != null && myAddInOnMainFormForHotKeys != null)
		//				myRunnForHotKeys(myAddInOnMainFormForHotKeys);
		//			
		//		}
		public override void RegisterType()
		{
			Layout.LayoutDetails.Instance.AddToList(typeof(NoteDataXML_Worklog), Name, PlugInBase.AddInFolderName);
			//NewMessage.Show ("Registering Picture");
			//Layout.LayoutDetails.Instance.AddToList(typeof(NoteDataXML_Picture.NoteDataXML_Pictures), "Picture");
		}
		public void RespondToMenuOrHotkey<T>(T form) where T: System.Windows.Forms.Form, MEF_Interfaces.iAccess 
		{
			//NewMessage.Show ("Fact or Search! This would only appear if a menu item was hooked up");
			// do nothing. This is not called for mef_Inotes
			return;
		}
	
		
		
		public void ActionWithParamForNoteTextActions (object param)
		{
			

		}
		public override string dependencymainapplicationversion { get { return "1.0.0.0"; }}
		
		//override string GUID{ get { return  "notedataxml_picture"; };
		public PlugInAction CalledFrom { 
			get
			{
				PlugInAction action = new PlugInAction();
				//	action.HotkeyNumber = -1;
				action.MyMenuName = "";//Loc.Instance.GetString ("Screen Capture");
				action.ParentMenuName = "";//"NotesMenu";
				action.IsOnContextStrip = false;
				action.IsANote = true;
				action.IsOnAMenu = false;
				action.IsNoteAction = false;
				action.QuickLinkShows = false;
				action.ToolTip ="";//Loc.Instance.GetString("Allows images to be added to Layouts, as well as a Screen Capture tool.");
				//action.Image = FileUtils.GetImage_ForDLL("camera_add.png");
				action.GUID = GUID;
				return action;
			} 
		}
		
		
		
	}
}