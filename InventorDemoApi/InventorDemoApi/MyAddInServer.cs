using Inventor;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

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
    }
}