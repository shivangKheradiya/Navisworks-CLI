using NavisworksIntegratedAPI23;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisworksCLI
{
    internal class NavisworksCLI : INavisworksCLI
    {
        private NavisworksIntegratedAPI23.InwOpState10 m_state;
        private NavisworksAutomationAPI23.Document m_doc;

        public bool IsGUI { get; set; } = true;

        public void SelectAll()
        {
            NavisworksIntegratedAPI23.InwOpSelection selection = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);
            selection.SelectAll();
        }

        public void AppendFile(string FileFullPathWithExtension)
        {
            m_doc.AppendFile(FileFullPathWithExtension);
        }

        public void OpenNavisworks(string FileFullPathWithExtension)
        {
            m_doc = new NavisworksAutomationAPI23.Document();
            m_state = m_doc.State();
            m_doc.Visible = IsGUI;
        }

        public void SaveNWD(string FileFullPathWithExtension)
        {
            m_doc.SaveAs(FileFullPathWithExtension);
        }

        public void SetColour(byte R, byte G, byte B)
        {
            SelectAll();

            NavisworksIntegratedAPI23.InwLVec3f colorVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);
            colorVec.SetValue(R / 255.0, G / 255.0, B / 255.0);
            m_state.OverrideColor(m_state.CurrentSelection, colorVec);
        }

        public void SetOrientation(double XRotation, double YRotation, double ZRotation)
        {
            SelectAll();

            NavisworksIntegratedAPI23.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
            NavisworksIntegratedAPI23.InwLRotation3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLRotation3f);
            NavisworksIntegratedAPI23.InwLVec3f upVector = m_state.CurrentView.ViewPoint.Camera.GetUpVector();

            NavisworksIntegratedAPI23.InwLUnitVec3f xAxis = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLUnitVec3f);
            NavisworksIntegratedAPI23.InwLUnitVec3f yAxis = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLUnitVec3f);
            NavisworksIntegratedAPI23.InwLUnitVec3f zAxis = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLUnitVec3f);
            
            xAxis.SetValue(1, 0, 0);
            transVal.SetValue(xAxis, XRotation);
            transVec.MakeRotation(transVal);
            yAxis.SetValue(0, 1, 0);
            transVal.SetValue(yAxis, YRotation);
            transVec.MakeRotation(transVal);
            zAxis.SetValue(0, 0, 1);
            transVal.SetValue(zAxis, ZRotation);
            transVec.MakeRotation(transVal);

            m_state.OverrideTransform(m_state.CurrentSelection, transVec);
        }

        public void SetPosition(double XPos, double YPos, double ZPos)
        {
            SelectAll();
            NavisworksIntegratedAPI23.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
            NavisworksIntegratedAPI23.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);
            NavisworksIntegratedAPI23.InwLVec3f upVector = m_state.CurrentView.ViewPoint.Camera.GetUpVector();
            transVal.SetValue(XPos, YPos, ZPos);
            transVec.MakeTranslation(transVal);
            m_state.OverrideTransform(m_state.CurrentSelection, transVec);
        }

        public void SetScale(double XScale, double YScale, double ZScale)
        {
            SelectAll();
            NavisworksIntegratedAPI23.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
            NavisworksIntegratedAPI23.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);
            NavisworksIntegratedAPI23.InwLVec3f upVector = m_state.CurrentView.ViewPoint.Camera.GetUpVector();
            transVal.SetValue(XScale, YScale, ZScale);
            transVec.MakeScale(transVal);
            m_state.OverrideTransform(m_state.CurrentSelection, transVec);
        }

        public void SetTransperancy(double TransperancyNumber)
        {
            SelectAll();
            m_state.OverrideTransparency(m_state.CurrentSelection, TransperancyNumber);
        }
    }
}
