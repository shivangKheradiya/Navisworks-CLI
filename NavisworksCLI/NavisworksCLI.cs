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

        #region Public API

        public void OpenNavisworks(string filePath)
        {
            m_doc = new NavisworksAutomationAPI23.Document();
            m_state = m_doc.State();
            m_doc.Visible = IsGUI;
            m_doc.OpenFile(filePath);
        }

        public void AppendFile(string filePath)
        {
            m_doc.AppendFile(filePath);
        }

        public void SaveNWD(string filePath)
        {
            m_doc.SaveAs(filePath);
        }

        public void SetColour(byte r, byte g, byte b)
        {
            var colorVec = CreateObject<NavisworksIntegratedAPI23.InwLVec3f>(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);

            colorVec.SetValue(r, g, b);

            m_state.OverrideColor(GetSelectionAll(), colorVec);
        }

        public void SetOrientation(double xRotation, double yRotation, double zRotation)
        {
            ApplyAxisRotation(1, 0, 0, xRotation);
            ApplyAxisRotation(0, 1, 0, yRotation);
            ApplyAxisRotation(0, 0, 1, zRotation);
        }

        public void SetPosition(double xPos, double yPos, double zPos)
        {
            var vec = CreateVector(xPos, yPos, zPos);
            var transform = CreateTransform();

            transform.MakeTranslation(vec);

            OverrideTransform(transform);
        }

        public void SetScale(double xScale, double yScale, double zScale)
        {
            var vec = CreateVector(xScale, yScale, zScale);
            var transform = CreateTransform();

            transform.MakeScale(vec);

            OverrideTransform(transform);
        }

        public void SetTransperancy(double transparency)
        {
            m_state.OverrideTransparency(GetSelectionAll(), transparency);
        }

        #endregion

        #region Rotation Helpers

        private void ApplyAxisRotation(double x, double y, double z, double angle)
        {
            var axis = CreateObject<NavisworksIntegratedAPI23.InwLUnitVec3f>(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLUnitVec3f);

            var rotation = CreateObject<NavisworksIntegratedAPI23.InwLRotation3f>(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLRotation3f);

            var transform = CreateTransform();

            axis.SetValue(x, y, z);
            rotation.SetValue(axis, angle);
            transform.MakeRotation(rotation);

            OverrideTransform(transform);
        }

        #endregion

        #region Common Factory Helpers

        private T CreateObject<T>(NavisworksIntegratedAPI23.nwEObjectType type)
        {
            return (T)m_state.ObjectFactory(type);
        }

        private NavisworksIntegratedAPI23.InwLTransform3f3 CreateTransform()
        {
            return CreateObject<NavisworksIntegratedAPI23.InwLTransform3f3>(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLTransform3f);
        }

        private NavisworksIntegratedAPI23.InwLVec3f CreateVector(double x, double y, double z)
        {
            var vec = CreateObject<NavisworksIntegratedAPI23.InwLVec3f>(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwLVec3f);

            vec.SetValue(x, y, z);
            return vec;
        }

        private NavisworksIntegratedAPI23.InwOpSelection GetSelectionAll()
        {
            var selection = CreateObject<NavisworksIntegratedAPI23.InwOpSelection>(NavisworksIntegratedAPI23.nwEObjectType.eObjectType_nwOpSelection);

            selection.SelectAll();
            return selection;
        }

        private void OverrideTransform(NavisworksIntegratedAPI23.InwLTransform3f3 transform)
        {
            m_state.OverrideTransform(GetSelectionAll(), transform);
        }

        #endregion
    }
}
