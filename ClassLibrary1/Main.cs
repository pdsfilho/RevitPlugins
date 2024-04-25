using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace ClassLibrary1
{
    public class Main : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            
            //Tab creation
            string tabName = "Parameters";
            application.CreateRibbonTab(tabName);

            //Ribbon creation
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Parameter Ribbon Panel");

            //Button creation
            String thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            String path = System.IO.Path.GetDirectoryName(thisAssemblyPath);

            PushButtonData buttonData = new PushButtonData("ParametersBtn", "Parameter Scanner",
                thisAssemblyPath, "ClassLibrary1.Tests.RevitExecute");


            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            pushButton.ToolTip = "Hi, This is a Plugin for ENG!";

            buttonData.LargeImage = new BitmapImage(new Uri(path + @"\Images\Eng.png"));
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}
