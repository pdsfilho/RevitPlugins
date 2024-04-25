using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using ClassLibrary1.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Tests
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class RevitExecute : IExternalCommand
    {
       
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;

            try
            {
                //Window pop up
                var v = doc.ActiveView;
                var vName = v.Name;
                var vId = v.Id;
                var vTemplateId = v.ViewTemplateId.ToString();

                ParameterWindow parameterWindow = new ParameterWindow(uidoc);
                parameterWindow.label_pmtName.Content = vName;

                parameterWindow.ShowDialog();


                return Result.Succeeded;
            }
            catch (Exception e)
            {
                return Result.Failed;
            }

        }
    }
}
