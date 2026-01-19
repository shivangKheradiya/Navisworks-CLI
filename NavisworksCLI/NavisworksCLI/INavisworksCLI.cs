using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisworksCLI
{
    internal interface INavisworksCLI
    {
        void OpenNavisworks(string FileFullPathWithExtension);
        void SetTransperancy(double TransperancyNumber);
        void SetColour(byte R, byte G, byte B);
        void SetOrientation(double XRotation, double YRotation, double ZRotation);
        void SetPosition(double XScale, double YScale, double ZScale);
        void SetScale(double XScale, double YScale, double ZScale);
        void AppendFile(string FileFullPathWithExtension);
        void SaveNWD(string FileFullPathWithExtension);
        void SelectAll();
    }
}
