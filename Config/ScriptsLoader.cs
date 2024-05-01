//using Microsoft.CodeAnalysis.CSharp.Scripting;
//using Microsoft.CodeAnalysis.Scripting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;

public class ScriptsLoader
{

    Form1 Form1_0;

    //private ScriptState<object> scriptState; // Store the state of the script

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }


    public async void LoadScripts()
    {
        try
        {
            string scriptFilePath = Application.StartupPath + @"\Scripts\Runs\Pit.cs";

            string scriptCode = File.ReadAllText(scriptFilePath);

            // Create a script options with references to commonly used assemblies
            /*ScriptOptions options = ScriptOptions.Default
                //.WithReferences(Assembly.GetExecutingAssembly())
                .WithReferences(typeof(Console).Assembly, typeof(Andariel).Assembly) // Add reference to assemblies needed
                .WithImports("System.Windows.Forms"); // Importing the namespace of the application*/

            //ScriptOptions options = ScriptOptions.Default.AddReferences(typeof(Form1).Assembly);

            // Evaluate the script asynchronously
            //scriptState = await CSharpScript.RunAsync(scriptCode, options);

            // Display the result (if any)
            //MessageBox.Show(scriptState.ReturnValue?.ToString(), "Script Execution Result", MessageBoxButtons.OK, MessageBoxIcon.Information);



            // Create the script options
            /*ScriptOptions options = ScriptOptions.Default
                .WithReferences(typeof(Console).Assembly) // Add reference to assemblies needed
                .WithImports("System"); // Import namespaces as needed*/

            // Compile the script
            //Script script = CSharpScript.Create(scriptCode, options);

            // Execute the script
            //await script.RunAsync();

            // Call a function from the script
            //await script.RunAsync("Andariel.MyFunction");

            //var myClassInstance = scriptState.GetVariable("Andariel");
            //myClassInstance.MyFunction();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error executing script: " + ex.Message, "Script Execution Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
