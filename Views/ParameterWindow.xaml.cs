using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassLibrary1.Extensions.SelectionExtensions;
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

    [Transaction(TransactionMode.Manual)]
    public partial class ParameterWindow : Window
    {
        public UIDocument uidoc { get; }
        public Document doc { get; }

        public ParameterWindow(UIDocument UiDoc)
        {
            uidoc = UiDoc;
            doc = UiDoc.Document;
           
            
            InitializeComponent();
        }

        private void IsolateClick(object sender, RoutedEventArgs e)
        {
            string parameterName = textBox_pmtName.Text;
            string parameterValue = textBox_pmtValue.Text;

            SelectCeilingAndFloor(parameterName, parameterValue, 1);

        }

        private void SelectClick(object sender, RoutedEventArgs e)
        {
            //Getting the texts

            string parameterName = textBox_pmtName.Text;
            string parameterValue = textBox_pmtValue.Text;

            SelectCeilingAndFloor(parameterName, parameterValue, 2);

        }

        private void pmtNameDataContextChanges(object sender, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private async void SelectCeilingAndFloor(string pn, string pv, int buttonPressed)
        {
           
            //Isolate In View --Button
            if (buttonPressed == 1)
            {
                 var activeDocument = doc.ActiveView;

                //Create a filter for Ceiling and Floor
                List<BuiltInCategory> ceilingAndFloor = new List<BuiltInCategory>()
            {
                BuiltInCategory.OST_Floors,
                BuiltInCategory.OST_Ceilings,
            };


                ElementMulticategoryFilter multiCategoryFilter = new ElementMulticategoryFilter(ceilingAndFloor);

                //Collector for Ceiling and Floor
                FilteredElementCollector collector = new FilteredElementCollector(doc).
                    WherePasses(multiCategoryFilter).WhereElementIsNotElementType();

                using (Transaction t = new Transaction(doc, "Isolate In View"))
                {
                    t.Start();

                    //Get ElementIDs
                    ICollection<ElementId> elementids = collector.ToElementIds();
                    activeDocument.IsolateElementsTemporary(elementids);
                                                        
                    t.Commit();
                }
            }
            
            //Select --Button
            else if (buttonPressed == 2)
            {
                //Filtering the Parameters for a List
                List<CeilingAndFloor> list = new FilteredElementCollector(doc).OfClass(typeof(CeilingAndFloor)).Where(
                    a => a.LookupParameter(pn).AsString() == pv).Cast<CeilingAndFloor>().ToList();

                var resultForm = new ResultForm(list);
                resultForm.ShowDialog();
            }       

        }
    }
}
