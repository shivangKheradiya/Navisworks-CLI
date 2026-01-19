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

        public void AppendFile(string FileFullPathWithExtension)
        {
            m_doc.AppendFile(FileFullPathWithExtension);
        }

        public void OpenNavisworks(string FileFullPathWithExtension)
        {
            m_doc = new NavisworksAutomationAPI23.Document();
            m_state = m_doc.State();
            m_doc.Visible = IsGUI;
            m_doc.OpenFile(FileFullPathWithExtension);
        }

        public void SaveNWD(string FileFullPathWithExtension)
        {
            m_doc.SaveAs(FileFullPathWithExtension);
        }

        public void SetColour(byte R, byte G, byte B)
        {
            NavisworksIntegratedAPI23.InwLVec3f colorVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);
            colorVec.SetValue(R , G , B);

            NavisworksIntegratedAPI23.InwOpSelection selection = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);
            selection.SelectAll();

            m_state.OverrideColor(selection, colorVec);
        }

        public void SetOrientation(double XRotation, double YRotation, double ZRotation)
        {
            ApplyAxisRotation(1, 0, 0, XRotation);
            ApplyAxisRotation(0, 1, 0, YRotation);
            ApplyAxisRotation(0, 0, 1, ZRotation);
        }

        private void ApplyAxisRotation(double unitVectorX, double unitVectorY, double unitVectorZ, double angle)
        {
            NavisworksIntegratedAPI23.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
            NavisworksIntegratedAPI23.InwLRotation3f rotVal = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLRotation3f);
            NavisworksIntegratedAPI23.InwLUnitVec3f Axis = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLUnitVec3f);

            Axis.SetValue(unitVectorX, unitVectorY, unitVectorZ);
            rotVal.SetValue(Axis, angle);
            transVec.MakeRotation(rotVal);

            NavisworksIntegratedAPI23.InwOpSelection selection = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);
            selection.SelectAll();

            m_state.OverrideTransform(selection, transVec);
        }

        public void SetPosition(double XPos, double YPos, double ZPos)
        {
            NavisworksIntegratedAPI23.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
            NavisworksIntegratedAPI23.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);

            transVal.SetValue(XPos, YPos, ZPos);
            transVec.MakeTranslation(transVal);

            NavisworksIntegratedAPI23.InwOpSelection selection = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);
            selection.SelectAll();

            m_state.OverrideTransform(selection, transVec);
        }

        public void SetScale(double XScale, double YScale, double ZScale)
        {
            NavisworksIntegratedAPI23.InwLTransform3f3 transVec = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
            NavisworksIntegratedAPI23.InwLVec3f transVal = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);

            transVal.SetValue(XScale, YScale, ZScale);
            transVec.MakeScale(transVal);

            NavisworksIntegratedAPI23.InwOpSelection selection = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);
            selection.SelectAll();
            m_state.OverrideTransform(selection, transVec);
        }

        public void SetTransperancy(double TransperancyNumber)
        {
            NavisworksIntegratedAPI23.InwOpSelection selection = m_state.ObjectFactory(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);
            selection.SelectAll();
            m_state.OverrideTransparency(selection, TransperancyNumber);
        }
    }
}
