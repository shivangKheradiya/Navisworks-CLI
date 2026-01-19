# **NavisworksCLI – Command‑Line Automation for Autodesk Navisworks**

NavisworksCLI is a lightweight, script‑friendly command‑line application designed to automate common operations in **Autodesk Navisworks Simulate/Manage**.  
It enables batch processing, integration with external systems, and hands‑free model manipulation without opening the Navisworks UI.

The tool supports both:

- **GUI Mode** – Launches Navisworks with UI  
- **TTY Mode** – Runs silently without UI (ideal for automation)

---

## 🚀 **Features**

### **File Operations**
- Open Navisworks‑supported files (`.nwd`, `.nwf`, `.nwc`)
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
3. Set orientation  
4. Set scale  
5. Set position  
6. Set colour  
7. Set transparency  
8. Save file  

Any missing argument is simply skipped.

---

# 📦 **Installation**

Place the compiled executable (`navicli.exe`) anywhere in your PATH or call it directly.

---

# 🧭 **Usage**

### **Example Command**
```
navicli.exe ^
  --isgui=false ^
  --open="C:\db\model.nwd" ^
  --append="C:\db\part1.nwc;C:\db\pa12.step;C:\db\part1ww.rvm;\\Server32\x.nwd" ^
  --position=(10,0,5) ^
  --orientation=(45,0,35) ^
  --scale=(1,1,0.5) ^
  --transperancy=50 ^
  --setcolour=(0,255,0) ^
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
| `--setcolour=(r,g,b)` | RGB override | `--setcolour=(0,255,0)` |
| `--transperancy=<value>` | Transparency (0–100) | `--transperancy=50` |
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
navicli.exe --batch="jobs.json"
```

### **3. JSON/YAML Configuration Mode**
Instead of long CLI commands:

```
navicli.exe --config="settings.json"
```

### **4. Logging & Diagnostics**
- Operation logs  
- Error logs  
- Performance metrics  

### **5. Plugin Architecture**
Allow custom operations via DLL plugins.
