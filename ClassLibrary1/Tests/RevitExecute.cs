using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Tests
{
    [Transaction(TransactionMode.Manual)]
    public class RevitExecute : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            //Select elements
            var element = uidoc.Selection.GetElementIds().Select(
                x => doc.GetElement(x)).First();
            
            //Get parameter element
            var value = element.LookupParameter("Mark").AsString();


            TaskDialog.Show("Message", value);
            
            return Result.Succeeded;

        }
    }
}
