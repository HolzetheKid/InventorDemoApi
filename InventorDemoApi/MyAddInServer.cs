using Inventor;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Attribute = System.Attribute;

namespace InventorDemoApi
{
    [GuidAttribute("a7695474-67ee-49da-9a09-283ef242a9b2")]
    public class MyAddInServer : Inventor.ApplicationAddInServer
    {
        public MyAddInServer()
        {
            Debug.WriteLine("constructor");
        }

        public void Activate(ApplicationAddInSite AddInSiteObject, bool FirstTime)
        {
            Debug.WriteLine("Activate");

            CreateRibbonPanel(GetAddInId(), AddInSiteObject.Application);
        }

        private string GetAddInId()
        {
            var id = (GuidAttribute)Attribute.GetCustomAttribute(typeof(MyAddInServer), typeof(GuidAttribute));
            return "{" + id.Value + "}";
        }

        public void Deactivate()
        {
            Debug.WriteLine("Deactivate");
        }

        public void ExecuteCommand(int CommandID)
        {
            Debug.WriteLine("ExecuteCommand");
        }

        public object Automation { get; }

        private void CreateRibbonPanel(string addinId, Application application)
        {

            if (addinId is null) throw new ArgumentNullException(nameof(addinId));
            if (application == null) throw new ArgumentNullException(nameof(application));

            var ribbons = application.UserInterfaceManager.Ribbons;
            var ribbonName = "ZeroDoc";
            var idMyOwnTab = "id_Tab_MyOwnTab";
            var idMyOwnPanel = "id_Tab_MyOwnPanel";
            RibbonTab ribbonTab;

            if (!RibbonTabExists(application.UserInterfaceManager, ribbonName, idMyOwnTab))
            {
                ribbonTab = ribbons[ribbonName].RibbonTabs.Add("NoUseForAName", idMyOwnTab, addinId);
            }
            else
            {
                ribbonTab = ribbons[ribbonName].RibbonTabs[idMyOwnTab];
            }

            if (RibbonPanelExists(application.UserInterfaceManager, ribbonName, idMyOwnTab, idMyOwnPanel)) return;

            var panel = ribbonTab.RibbonPanels.Add("MyPanel", idMyOwnPanel, addinId, "", false);
          
            var buttonDescription = GetShowTextButton(addinId, application);
            panel.CommandControls.AddButton(buttonDescription);

        }

        private static bool RibbonTabExists(UserInterfaceManager userInterfaceManager, string ribbonName, string tabName)
        {
            var ribbons = userInterfaceManager.Ribbons;
            foreach (RibbonTab tab in ribbons[ribbonName].RibbonTabs)
            {
                if (tab.InternalName == tabName) return true;
            }

            return false;
        }

        private static bool RibbonPanelExists(UserInterfaceManager userInterfaceManager, string ribbonName, string tabName, string panelName)
        {
            var ribbons = userInterfaceManager.Ribbons;
            var ribbonTabAnnotate = ribbons[ribbonName].RibbonTabs[tabName];
            foreach (RibbonPanel ribbonPanel in ribbonTabAnnotate.RibbonPanels)
            {
                if (ribbonPanel.InternalName == panelName) return true;
            }

            return false;
        }

        private ButtonDefinition GetShowTextButton(string addinId, Application application)
        {
            CommandCategory slotCmdCategory = application.CommandManager.CommandCategories.Add("Slot", "Autodesk:YourAddIn:ShowTextCmd", addinId);

            var btn = new TestButton(application, addinId);
            slotCmdCategory.Add(btn.ButtonDefinition);

            return btn.ButtonDefinition;
        }

       
    }
}