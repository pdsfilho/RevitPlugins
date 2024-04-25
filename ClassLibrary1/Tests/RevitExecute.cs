using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
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
                //Select elements
                var element = uidoc.Selection.GetElementIds().Select(
                    x => doc.GetElement(x)).First();

                //Get parameter value ("Mark")
                // var value = element.get_Parameter(BuiltInParameter.ALL_MODEL_MARK).AsString();

                //Pick elements with built in parameters "MARK"
               // uidoc.Selection.PickObjects(ObjectType.Element);

                List<FamilyInstance> list = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Where(
                    a => a.LookupParameter("Mark").AsString() == "ENG").Cast<FamilyInstance>().ToList();

                //TaskDialog.Show("Message", value);
                TaskDialog.Show("Message", list.ToString());


                return Result.Succeeded;
            }
            catch (Exception e)
            {
                return Result.Failed;
            }

        }
    }
}
