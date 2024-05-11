using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Extensions.SelectionExtensions
{
    internal class ElementSelectionFilter : ISelectionFilter
    {
        private readonly Func<Element, bool> _validateElement;

        //Call by Element
        public ElementSelectionFilter(Func<Element, bool> validateElement)
        {
            _validateElement = validateElement;
        }

        //Call by Reference
        public ElementSelectionFilter(Func<Element, bool> validateElement, Func<Reference, bool> validateReference) 
            : this(validateElement)
        {
            _validateElement = validateElement;
        }

        public bool AllowElement(Element elem)
        {
            return _validateElement(elem);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
