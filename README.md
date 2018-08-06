# RunFSXScript Plugin

**NOTE: This plugin is currently experimental. Tested with MacOS Mojave 10.14
using Rhino for Mac 5.4.2**

Rhino script for running FSX files.

## Setup

**Requirements**

* Rhino 5
* Mac Visual Studio (For Development)

**Installation**

Navigate to "Releases" and download the pre-release. Double-click the
`RunFSXScriptPlugin.macrhi` file and Rhino will automatically install the
plugin.

## Running

After the plugin is installed, simply execute the `RunFSXScriptPlugin` command.
The plugin will prompt you to select a FSX file.

```fsharp
#if INTERACTIVE
#r @"/Applications/Rhinoceros.app/Contents/Resources/RhinoCommon.dll"
#endif

open Rhino

RhinoApp.WriteLine "Hello Rhino"
```

### Feedback

This is an experimental release only tested on MacOS, feedback is both welcome and encouraged.
