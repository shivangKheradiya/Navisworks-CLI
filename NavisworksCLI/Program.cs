using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavisworksCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var parsed = ParseArguments(args);
            var cli = new NavisworksCLI();

            // 1. GUI Mode (optional)
            bool isGui = parsed.TryGetValue("isgui", out var guiVal) &&
                         bool.TryParse(guiVal, out var guiFlag) &&
                         guiFlag;

            if (isGui)
            {
                Console.WriteLine("Running in GUI mode...");
                // cli.IsGUI() already exists but you may call it if needed
            }
            else
            {
                cli.IsGUI = false;
            }

            // 2. OPEN FILE
            if (parsed.TryGetValue("open", out var openFile))
            {
                cli.OpenNavisworks(openFile);
            }

            // 3. APPEND FILES
            if (parsed.TryGetValue("append", out var appendList))
            {
                var files = appendList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var f in files)
                    cli.AppendFile(f.Trim());
            }

            // 4. ORIENTATION
            if (parsed.TryGetValue("orientation", out var orientation))
            {
                var (x, y, z) = ParseVector3(orientation);
                cli.SetOrientation(x, y, z);
            }

            // 5. SCALE
            if (parsed.TryGetValue("scale", out var scale))
            {
                var (sx, sy, sz) = ParseVector3(scale);
                cli.SetScale(sx, sy, sz);
            }

            // 6. POSITION
            if (parsed.TryGetValue("position", out var position))
            {
                var (px, py, pz) = ParseVector3(position);
                cli.SetPosition(px, py, pz);
            }

            // 7. COLOUR
            if (parsed.TryGetValue("colour", out var colour))
            {
                var (r, g, b) = ParseVector3Byte(colour);
                cli.SetColour(r, g, b);
            }

            // 8. TRANSPARENCY
            if (parsed.TryGetValue("transperancy", out var trans))
            {
                if (double.TryParse(trans, out var tVal))
                    cli.SetTransperancy(tVal);
            }

            // 9. SAVE
            if (parsed.TryGetValue("save", out var saveFile))
            {
                cli.SaveNWD(saveFile);
            }

            Console.WriteLine("Operation completed.");
        }

        // -----------------------------
        // ARGUMENT PARSER
        // -----------------------------
        static Dictionary<string, string> ParseArguments(string[] args)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            foreach (var arg in args)
            {
                if (!arg.StartsWith("--")) continue;

                var trimmed = arg.Substring(2);
                var parts = trimmed.Split(new[] { '=' }, 2);

                if (parts.Length == 2)
                {
                    dict[parts[0].ToLower()] = parts[1].Trim('"');
                }
            }

            return dict;
        }

        // -----------------------------
        // HELPERS
        // -----------------------------
        static (double X, double Y, double Z) ParseVector3(string input)
        {
            input = input.Trim('(', ')');
            var parts = input.Split(',');

            return (
                double.Parse(parts[0]),
                double.Parse(parts[1]),
                double.Parse(parts[2])
            );
        }

        static (byte R, byte G, byte B) ParseVector3Byte(string input)
        {
            input = input.Trim('(', ')');
            var parts = input.Split(',');

            return (
                byte.Parse(parts[0]),
                byte.Parse(parts[1]),
                byte.Parse(parts[2])
            );
        }
    }
}