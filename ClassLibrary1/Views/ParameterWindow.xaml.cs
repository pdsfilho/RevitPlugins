using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public ParameterWindow(UIDocument UiDoc)
        {
            uidoc = UiDoc;
            doc = UiDoc.Document;

            InitializeComponent();
           
            //Forcing label contents
            label_pmtName.Content = "*Parameter Name: ";
            label_pmtValue.Content = "Parameter Value: ";
        }

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            //Getting the texts

            string parameterName = textBox_pmtName.Text;
            string parameterValue = textBox_pmtValue.Text;

            //Select elements
            var element = uidoc.Selection.GetElementIds().Select(
                x => doc.GetElement(x)).First();

            //Pick elements with built in parameters "MARK"
            // uidoc.Selection.PickObjects(ObjectType.Element);

            //Create a filter for Ceiling and Floor
            var ceilingAndFloor = new List<BuiltInCategory>()
            {
                BuiltInCategory.OST_Floors,
                BuiltInCategory.OST_Ceilings,

            };

            var multiCategoryFilter = new ElementMulticategoryFilter(ceilingAndFloor);

            var collector = new FilteredElementCollector(doc).WherePasses(multiCategoryFilter);

            List<CeilingAndFloor> list = new FilteredElementCollector(doc).OfClass(typeof(CeilingAndFloor)).Where(
               a => a.LookupParameter(parameterName).AsString() == parameterValue).Cast<CeilingAndFloor>().ToList();

            //TaskDialog.Show("Message", value);
            //TaskDialog.Show("Message", list.Count().ToString());

            var resultForm = new ResultForm(collector, list);
            resultForm.ShowDialog();

        }

        private void pmtNameDataContextChanges(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }
    }
}
