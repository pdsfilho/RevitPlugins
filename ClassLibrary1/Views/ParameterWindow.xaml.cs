using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassLibrary1.Views
{
    /// <summary>
    /// Interaction logic for ParameterWindow.xaml
    /// </summary>
    public partial class ParameterWindow : Window
    {
        public UIDocument uidoc { get; }
        public Document doc { get; }

        string parameterName;
        string parameterValue;

        public ParameterWindow(UIDocument UiDoc)
        {
            uidoc = UiDoc;
            doc = UiDoc.Document;

            InitializeComponent();
        }

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            //Getting the texts
            
            parameterName = textBox_pmtName.Text;
            parameterValue = textBox_pmtValue.Text;



            //Select elements
            var element = uidoc.Selection.GetElementIds().Select(
                x => doc.GetElement(x)).First();

            //Get parameter value ("Mark")
            // var value = element.get_Parameter(BuiltInParameter.ALL_MODEL_MARK).AsString();

            //Pick elements with built in parameters "MARK"
            // uidoc.Selection.PickObjects(ObjectType.Element);

            List<FamilyInstance> list = new FilteredElementCollector(doc).OfClass(typeof(FamilyInstance)).Where(
                a => a.LookupParameter(parameterName).AsString() == parameterValue).Cast<FamilyInstance>().ToList();

            //TaskDialog.Show("Message", value);
            TaskDialog.Show("Message", list.ToString());

        }
    }
}
