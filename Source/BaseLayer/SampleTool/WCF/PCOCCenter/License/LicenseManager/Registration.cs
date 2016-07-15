using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.DXperience.Demos;
using DevExpress.XtraBars;
using DevExpress.XtraNavBar;

namespace OPT.PEOfficeCenter.LicenseManager
{
	public class TutorialsInfo : DevExpress.DXperience.Demos.ModulesInfo {
		const string languageDir = "CS\\";
        static void SetBarManager(Control ctrl, BarManager manager) {
            foreach(Control element in ctrl.Controls) {
                NavBarControl nc = element as NavBarControl;
                if(nc != null) nc.MenuManager = manager;
                SetBarManager(element, manager);
            }
        }
		public static DevExpress.Tutorials.ModuleBase ShowModule(string name, DevExpress.XtraEditors.GroupControl group) {
			ModuleInfo item = TutorialsInfo.GetItem(name);
			Cursor currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			group.Parent.SuspendLayout();
			group.SuspendLayout();
			try {
				Control oldTutorial = null;
				if(Instance.CurrentModuleBase != null) 
					oldTutorial = Instance.CurrentModuleBase.TModule;
								
				TutorialControl tutorial = item.TModule as TutorialControl;
				tutorial.Bounds = group.DisplayRectangle;
				Instance.CurrentModuleBase = item;
				tutorial.Visible = false;
				group.Controls.Add(tutorial);
				tutorial.Dock = DockStyle.Fill;

				tutorial.Visible = true;
				if(oldTutorial != null) oldTutorial.Visible = false;
			}
			finally {
				group.ResumeLayout(true);
				group.Parent.ResumeLayout(true);
				Cursor.Current = currentCursor;
			}
			RaiseModuleChanged();
			DevExpress.Tutorials.ModuleBase module = Instance.CurrentModuleBase.TModule as DevExpress.Tutorials.ModuleBase;
			if(module != null) {
				module.TutorialInfo.Description = Instance.CurrentModuleBase.Description;
				module.TutorialInfo.TutorialName = Instance.CurrentModuleBase.Name;
				module.TutorialInfo.WhatsThisCodeFile = Instance.CurrentModuleBase.CodeFile;
				module.TutorialInfo.WhatsThisXMLFile = Instance.CurrentModuleBase.XMLFile;
				string xmlFile = DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, module.TutorialInfo.WhatsThisXMLFile, false);
				string codeFile = DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, module.TutorialInfo.WhatsThisCodeFile, false);
				if(xmlFile == "") module.TutorialInfo.WhatsThisXMLFile = languageDir + module.TutorialInfo.WhatsThisXMLFile;
				if(codeFile == "") module.TutorialInfo.WhatsThisCodeFile = languageDir + module.TutorialInfo.WhatsThisCodeFile;
			}
			return module;
		}
	}
	class RegisterTutorials {
        internal static string NewFeaturesString = "DevExpress XtraNavBar " + AssemblyInfo.MarketingVersion;
		public static void Register() {
			TutorialsInfo.Add(RegisterTutorials.NewFeaturesString, "DevExpress.XtraNavBar.Demos.About");
			TutorialsInfo.Add("Add Groups", "DevExpress.XtraNavBar.Demos.AddGroups", 
				"This example demonstrates how to add/delete the navbar's groups.", null, 
				"NavBarMainDemo\\AddGroups\\AddGroups.cs", "Data\\Tutorials\\XtraNavBar\\AddGroups.xml");
			TutorialsInfo.Add("Add Item Links", "DevExpress.XtraNavBar.Demos.AddItemLinks", 
				"This example demonstrates how to add/delete Items and ItemLinks in navbar control groups.", null, 
				"NavBarMainDemo\\AddItemLinks\\AddItemLinks.cs", "Data\\Tutorials\\XtraNavBar\\AddItemLinks.xml");
			TutorialsInfo.Add("Group Container", "DevExpress.XtraNavBar.Demos.GroupContainer", 
				"Setting a group's GroupStyle property to ControlContainer automatically creates a container control within the group's client area. This enables you to fill the group with any Windows Forms controls just by dragging them onto the group and managing their layout in the same manner as on any other container control.", null, 
				"NavBarMainDemo\\GroupContainer\\GroupContainer.cs", "");
			TutorialsInfo.Add("Group Styles", "DevExpress.XtraNavBar.Demos.GroupStyles", 
				"This example displays all possible representation styles that can be applied to links in navbar control groups.", null, 
				"NavBarMainDemo\\GroupStyles\\GroupStyles.cs", "Data\\Tutorials\\XtraNavBar\\GroupStyles.xml");
			TutorialsInfo.Add("Hit Info", "DevExpress.XtraNavBar.Demos.HitInfo", 
				"This example demonstrates how to obtain information on a navbar control element based on its coordinate.", null, 
				"NavBarMainDemo\\HitInfo\\HitInfo.cs", "");
			TutorialsInfo.Add("NavBar Hints", "DevExpress.XtraNavBar.Demos.NavBarHints", 
				"This example demonstrates how to implement hints into the NavBar control.", null, 
				"NavBarMainDemo\\NavBarHints\\NavBarHints.cs", "Data\\Tutorials\\XtraNavBar\\NavBarHints.xml");
			TutorialsInfo.Add("NavBar Info", "DevExpress.XtraNavBar.Demos.NavBarInfo", 
				"This example demonstrates how to obtain information that relates to navbar control elements (Groups, Items, ItemLinks).", null, 
				"NavBarMainDemo\\NavBarInfo\\NavBarInfo.cs", "Data\\Tutorials\\XtraNavBar\\NavBarInfo.xml");
			TutorialsInfo.Add("View Styles", "DevExpress.XtraNavBar.Demos.ViewStyles", 
				"This example displays view styles which can be applied to the navbar control and demonstrates how to switch them. Select a style from the 'Views' - the result can be viewed via the NavBar Control.", null, 
				"NavBarMainDemo\\ViewStyles\\ViewStyles.cs", "Data\\Tutorials\\XtraNavBar\\ViewStyles.xml");
			TutorialsInfo.Add("Customizable Distances", "DevExpress.XtraNavBar.Demos.CustomizableDistances", 
				"", null, 
				"NavBarMainDemo\\CustomizableDistances\\CustomizableDistances.cs", "Data\\Tutorials\\XtraNavBar\\CustomizableDistances.xml");
			TutorialsInfo.Add("Custom Draw", "DevExpress.XtraNavBar.Demos.CustomDraw", 
				"This example demonstrates how to use CustomDraw Events for the NavBar Control.", null, 
				"NavBarMainDemo\\CustomDraw\\CustomDraw.cs", "Data\\Tutorials\\XtraNavBar\\CustomDraw.xml");
		}
	}
	class RegisterDemos {
		public static void Register() {
			TutorialsInfo.Add("Blending And CustomDraw", "DevExpress.XtraNavBar.Demos.frmNavBarBlending", 
				"This demo demonstrates the XtraNAvBar's Alpha Blending feature. With this feature you can provide a background image for the control and for each individual group and customize the transparency for each element. Click one of the right images to select a background image for the corresponding group. DoubleClick the 'Image' label to set the default background.", null, "", "");
			TutorialsInfo.Add("Navigation Pane", "DevExpress.XtraNavBar.Demos.frmNavBarNavigationPane", 
				"This demo demonstrates Microsoft Office 2003 Navigation Pane style. This style supports all the features introduced in MS Outlook.", null, "", "");
			TutorialsInfo.Add("Drag And Drop", "DevExpress.XtraNavBar.Demos.frmNavBarDragDrop", 
				"This demo shows you how to implement internal and external Drag and Drop so that end-users can  move item links both to 'Items List' and 'Recycle Bin' and from 'Items List' to the NavBar.", null, "", "");
			TutorialsInfo.Add("Properties", "DevExpress.XtraNavBar.Demos.frmNavBarProperties", 
				"This demo provides the means that allow you to change the visual and behavior settings of the NavBar control and its elements (items, groups). You can also right-click a link or group to activate an option menu for it.", null, "", "");
		}
	}
}
