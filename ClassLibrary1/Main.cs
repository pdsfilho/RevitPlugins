﻿using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("cmdMyTest", "Parameter Scanner",
                thisAssemblyPath, "ClassLibrary1.Tests.TestRevit");

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            pushButton.ToolTip = "Hi, This is a Plugin for ENG!";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

    }
}