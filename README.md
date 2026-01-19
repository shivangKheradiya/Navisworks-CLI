# **NavisworksCLI**

### Command-Line Automation Tool for Autodesk Navisworks

NavisworksCLI is a lightweight, script-friendly command-line automation tool for **Autodesk Navisworks Simulate and Manage**.
It enables fully automated model processing, batch transformations, and headless execution for CI/CD pipelines, data preparation workflows, and enterprise BIM integrations.

The tool supports both interactive and headless execution modes:

* **GUI Mode** — Runs Navisworks with visible UI
* **Headless Mode** — Runs silently without UI (recommended for automation)

---

## ✨ Key Features

### 📂 File Operations

* Open Navisworks-supported formats (`.nwd`, `.nwf`, `.nwc`, `.rvm`, `.step`, and more)
* Append multiple models in a single run
* Export processed models as `.nwd`

---

### 🔧 Model Transformations

Apply transformations globally to the active document:

* Rotation (X / Y / Z axis)
* Scaling (uniform or per-axis)
* Translation (position offset)
* Colour override (RGB)
* Transparency override (0–1 range)

---

### ⚙ Execution Pipeline (Deterministic Order)

Operations are always executed in a fixed and predictable sequence:

1. Open file
2. Append files
3. Apply orientation
4. Apply scale
5. Apply position
6. Apply colour
7. Apply transparency
8. Save output NWD

> Any missing argument is automatically skipped without error.

This guarantees reproducible results across automation runs.

---

## 📦 Installation

### Prerequisites

* Autodesk Navisworks Simulate or Manage (installed locally) e.g. Repository built using Simulate 2026
* Windows OS
* .NET Runtime (as required by build target)

---

### Installation Steps

1. Build or download `NavisworksCLI.exe`
2. Place the executable in:

   * Any folder included in your system `PATH`
     **OR**
   * Use absolute path while executing

No additional configuration is required.

---

## 🧭 Usage

### Basic Example

```bash
NavisworksCLI.exe ^
  --isgui=false ^
  --open="C:\Models\main.nwd" ^
  --append="C:\Models\part1.nwc;C:\Models\part2.rvm;\\Server\data\plant.nwd" ^
  --position=(10,0,5) ^
  --orientation=(45,0,35) ^
  --scale=(1,1,0.5) ^
  --transparency=0.5 ^
  --colour=(0,255,0) ^
  --save="C:\Output\FinalModel.nwd"
```

---

### Headless Automation Example

Recommended for servers and batch pipelines:

```bash
NavisworksCLI.exe --isgui=false --open=input.nwd --save=output.nwd
```

---

## 📝 Command-Line Arguments

| Argument                 | Description            | Example                         |
| ------------------------ | ---------------------- | ------------------------------- |
| `--isgui=true or false`  | Enable or disable Navisworks UI | `--isgui=false`        |
| `--open=<file>`          | Open Navisworks file   | `--open=model.nwd`              |
| `--append=<file1;file2>` | Append multiple models | `--append="a.nwc;b.rvm"`        |
| `--orientation=(x,y,z)`  | Rotation in degrees    | `--orientation=(45,0,0)`        |
| `--scale=(x,y,z)`        | Scale factors          | `--scale=(1,1,0.5)`             |
| `--position=(x,y,z)`     | Translation offset     | `--position=(10,0,5)`           |
| `--colour=(r,g,b)`       | RGB override (0–255)   | `--colour=(0,255,0)`            |
| `--transparency=<value>` | Transparency (0.0–1.0) | `--transparency=0.5`            |
| `--save=<file>`          | Save output NWD        | `--save=final.nwd`              |

---

## 🧩 Internal Execution Workflow

Internally, the CLI parses arguments and executes API calls in a controlled sequence:

```csharp
cli.OpenNavisworks(...)
cli.AppendFile(...)
cli.SetOrientation(...)
cli.SetScale(...)
cli.SetPosition(...)
cli.SetColour(...)
cli.SetTransparency(...)
cli.SaveNWD(...)
```

Each step is optional and executed only when the corresponding argument is supplied.

---

## 🏗 Architecture Overview

NavisworksCLI is built on top of:

* Autodesk Navisworks Automation API
* Autodesk Navisworks Integrated API

Design goals:

* Minimal memory footprint
* Deterministic execution order
* Script-friendly interface
* Automation-safe (headless capable)
* Extensible internal structure

---

## 🔮 Roadmap / Future Enhancements

### 🎨 Attribute-Based Colour Rules

Planned support for rule-based visual automation:

Example:

* `Material = Steel` → Gray
* `Status = Delayed` → Red

Benefits:

* Automated QA visualization
* Model classification
* Metadata-driven reporting
* BIM data analytics workflows

---

### 📁 Batch Processing Mode

Execute multiple jobs from a single configuration:

```bash
NavisworksCLI.exe --batch="jobs.json"
```

---

### 📄 Configuration File Support

Replace long CLI commands with structured configs:

```bash
NavisworksCLI.exe --config="settings.json"
```

Supports:

* JSON
* YAML

---

## ⚠ Important Notes

* NavisworksCLI requires a licensed installation of Autodesk Navisworks.
* This tool acts as an automation wrapper and does not bypass Autodesk licensing.
* Headless mode still requires Navisworks components to be present.

---

## 📄 License

* [MIT License](./LICENSE)

---

## 🤝 Contributions

Contributions are welcome.