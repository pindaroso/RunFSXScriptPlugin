namespace RunFSXScriptPlugin

open Rhino
open Rhino.UI

open System
open System.IO
open System.Windows.Forms
open System.Text

open Microsoft.FSharp.Compiler.SourceCodeServices
open Microsoft.FSharp.Compiler.Interactive.Shell


type RunFSXScriptPlugin () =  
    inherit Rhino.PlugIns.PlugIn ()

    static member val Instance = RunFSXScriptPlugin ()


type RunFSXScriptPluginCommand () =
    inherit Rhino.Commands.Command ()    

    override this.EnglishName = "RunFSXScript"

    static member val Instance = RunFSXScriptPluginCommand ()

    override this.RunCommand (doc, mode)  =        
        // Intialize output and input streams
        let sbOut = new StringBuilder ()
        let sbErr = new StringBuilder ()
        let inStream = new StringReader ("")
        let outStream = new StringWriter (sbOut)
        let errStream = new StringWriter (sbErr)

        // Build command line arguments and start FSI session
        let argv = [| "dotnet"; "./packages/FSharp.Compiler.Tools.10.0.2/tools/fsi.exe" |]
        let allArgs = Array.append argv [| "--noninteractive" |]

        let fsiConfig = FsiEvaluationSession.GetDefaultConfiguration ()
        let fsiSession = FsiEvaluationSession.Create (fsiConfig, allArgs, inStream, outStream, errStream)  

        // Open FSX script
        let openFileDialog = new Rhino.UI.OpenFileDialog ()

        openFileDialog.Filter <- "fsx file (*.fsx)" 
        openFileDialog.MultiSelect <- false
        openFileDialog.Title <- "Open FSX Script"

        if openFileDialog.ShowDialog () 
            then
                // Evaluate script
                RhinoApp.WriteLine openFileDialog.FileName
                let result, warnings = fsiSession.EvalScriptNonThrowing openFileDialog.FileName
                match result with 
                | Choice1Of2 () -> Rhino.Commands.Result.Success
                | Choice2Of2 except -> 
                    let msg = 
                        [ for w in warnings do yield String.Format ("Line {1}, Column {2}: {0}"
                        , w.Message, w.StartLineAlternate, w.StartColumn) 
                        ] |> String.concat "\n"
                    let showMessage = Rhino.UI.Dialogs.ShowMessage (msg, "FSX Script Error")
                    Rhino.Commands.Result.Failure
            else
                Rhino.Commands.Result.Cancel