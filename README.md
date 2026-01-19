# **NavisworksCLI – Command‑Line Automation for Autodesk Navisworks**

NavisworksCLI is a lightweight, script‑friendly command‑line application designed to automate common operations in **Autodesk Navisworks Simulate/Manage**.  
It enables batch processing, integration with external systems, and hands‑free model manipulation without opening the Navisworks UI.

The tool supports both:

- **GUI Mode** – Launches Navisworks with UI  
- **TTY Mode** – Runs silently without UI (ideal for automation)

---

## 🚀 **Features**

### **File Operations**
- Open Navisworks‑supported files (`.nwd`, `.nwf`, `.nwc`, etc...)
- Append multiple models in a single command
- Save the final model as `.nwd`

### **Model Transformations**
- Set **orientation** (X/Y/Z rotation)
- Set **scale** (uniform or per‑axis)
- Set **position** (translation)
- Apply **colour override**
- Apply **transparency override**

### **Execution Pipeline (Fixed Order)**  
The CLI always executes operations in the following sequence:

1. Open file  
2. Append files  
3. Set orientation for document  
4. Set scale for document
5. Set position for document
6. Set colour for document
7. Set transparency for document
8. Save NWD file

Any missing argument is simply skipped.

---

# 📦 **Installation**

Place the compiled executable (`NavisworksCLI.exe`) anywhere in your PATH or call it directly.

---

# 🧭 **Usage**

### **Example Command**
```
NavisworksCLI.exe ^
  --isgui=false ^
  --open="C:\db\model.nwd" ^
  --append="C:\db\part1.nwc;C:\db\pa12.step;C:\db\part1ww.rvm;\\Server32\x.nwd" ^
  --position=(10,0,5) ^
  --orientation=(45,0,35) ^
  --scale=(1,1,0.5) ^
  --transperancy=0.5 ^
  --colour=(0,255,0) ^
  --save="C:\db\Output.nwd"
```

---

# 📝 **Argument Reference**

| Argument | Description | Example |
|---------|-------------|---------|
| `--isgui=true|false` | Run with or without Navisworks UI | `--isgui=false` |
| `--open=<file>` | Opens a Navisworks file | `--open=model.nwd` |
| `--append=<file1;file2;...>` | Appends multiple files | `--append="a.nwc;b.rvm"` |
| `--orientation=(x,y,z)` | Rotation in degrees | `--orientation=(45,0,35)` |
| `--scale=(x,y,z)` | Scale factors | `--scale=(1,1,0.5)` |
| `--position=(x,y,z)` | Translation | `--position=(10,0,5)` |
| `--colour=(r,g,b)` | RGB override | `--colour=(0,255,0)` |
| `--transperancy=<value>` | Transparency (0–1) | `--transperancy=0.5` |
| `--save=<file>` | Save output NWD | `--save="output.nwd"` |

---

# 🧩 **Internal Workflow**

The CLI parses arguments, validates them, and calls your `NavisworksCLI` class in the correct order:

```csharp
cli.OpenNavisworks(...)
cli.AppendFile(...)
cli.SetOrientation(...)
cli.SetScale(...)
cli.SetPosition(...)
cli.SetColour(...)
cli.SetTransperancy(...)
cli.SaveNWD(...)
```

Each step is optional—if an argument is missing, the step is skipped.

---


# 🔮 **Future Scope**

### **1. Attribute‑Based Colour Overrides**
A powerful enhancement would be:

- Read model item attributes  
- Apply colour overrides based on rules  
- Example:  
  - If `Material = Steel` → Colour = Gray  
  - If `Status = Delayed` → Colour = Red  

This enables:

- Automated visual reporting  
- Rule‑based model classification  
- Integration with external metadata systems  

### **2. Batch Processing of Multiple Models**
Support for:

```
NavisworksCLI.exe --batch="jobs.json"
```

### **3. JSON/YAML Configuration Mode**
Instead of long CLI commands:

```
NavisworksCLI.exe --config="settings.json"
```

### **4. Logging & Diagnostics**
- Operation logs
- Error logs
- Performance metrics

### **5. Plugin Architecture**
Allow custom operations via DLL plugins.
