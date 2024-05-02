using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Reflection;

using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Microsoft.CSharp.RuntimeBinder;

public class ScriptsLoader
{
    Form1 Form1_0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void TestMethod()
    {
        Console.WriteLine("Executing MyMethod from script #2...");
    }

    public void LoadScripts(string ThisScriptPath)
    {
        string scriptCode = File.ReadAllText(ThisScriptPath);
        string className = Path.GetFileNameWithoutExtension(ThisScriptPath);
        /*string scriptCode = @"
            using System;
            using System.Reflection;
            using System.Windows.Forms;

            public class MyClass
            {
                public void SetForm1(Form1 form1_1)
                {
                    Console.WriteLine(Enums.Act.ActI.ToString());
                }

                public void MyMethod(Form1 form)
                {
                    form.ScriptsLoader_0.TestMethod();
                    Console.WriteLine(Enums.Act.ActI.ToString());

                }
            }
        ";
        string className = "MyClass";*/

        /*string scriptCode = @"
            using System;
            using System.Reflection;
            using System.Windows.Forms;

            public class MyClass
            {
                public static int Life = 0;

                public void MyMethod()
                {
                    Console.WriteLine(Enums.Act.ActI.ToString());
                }
            }
        ";
        string className = "MyClass";

        string scriptCode2 = @"
            using System;
            using System.Reflection;
            using System.Windows.Forms;

            public class MyClass2
            {
                public void MyMethod()
                {
                    Console.WriteLine(""Life from MyClass2: "" + MyClass.Life + """");
                }
            }
        ";
        string className2 = "MyClass2";*/

        //using (var codeProvider = new CSharpCodeProvider())
        //{
            try
            {
                // Set compiler parameters
                CompilerParameters compilerParams = new CompilerParameters();
                compilerParams.GenerateExecutable = false;
                compilerParams.GenerateInMemory = true;

                // Compile the script code
                Assembly assembly = CompileScript(scriptCode);

                // Create an instance of MyClass from the compiled assembly
                Type myClassType = assembly.GetType(className);
                //dynamic myClassInstance = Activator.CreateInstance(myClassType);

                // Check if the type was found
                if (myClassType == null || !typeof(Andariel).IsAssignableFrom(myClassType))
                {
                    Console.WriteLine("Invalid or incompatible type.");
                    return;
                }

                //Form1_0.AllClassInstances.Add(myClassInstance);

                // Load Andariel dynamically
                //Andariel newAndarielInstance = LoadAndarielDynamically();

                //Assembly assembly = Assembly.LoadFrom("DynamicallyDefinedAssembly.dll");

                // Replace the existing instance with the new one
                //Form1_0.Andariel_0 = newAndarielInstance;
                Andariel andarielInstance = Activator.CreateInstance(myClassType) as Andariel;
                //myClassInstance.RunScript();
                //Form1_0.Andariel_0 = andarielInstance;
                //Form1_0.Andariel_0 = new Andariel();
                //Form1_0.Andariel_0.SetForm1(Form1_0);
                //Form1_0.Andariel_0.RunScript();


                // Use MethodInfo to get the method information
                //MethodInfo methodInfo = myClassType.GetMethod("SetForm1");
                //if (methodInfo != null) methodInfo.Invoke(myClassInstance, new object[] { Form1_0 });

                // Assign myClassInstance to the property Form1_0.Andariel_0
                /*PropertyInfo propertyInfo = typeof(Form1).GetProperty("Andariel_0");
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(Form1_0, myClassInstance);
                }
                else
                {
                    Console.WriteLine("Property 'Andariel_0' not found.");
                }*/

                //Form1_0.Andariel_0 = new ((Andariel) myClassInstance);

                Form1_0.method_1("Loaded file: " + Path.GetFileName(ThisScriptPath), Color.DarkGreen);
            }
            catch (Exception ex)
            {
                Form1_0.method_1($"Exception occurred: {ex}", Color.Red);
            }
        //}

        /*using (var codeProvider = new CSharpCodeProvider())
        {
            try
            {
                // Set compiler parameters
                CompilerParameters compilerParams = new CompilerParameters();
                compilerParams.GenerateExecutable = false;
                compilerParams.GenerateInMemory = true;

                // Compile the script code
                Assembly assembly = CompileScript(scriptCode2);

                // Create an instance of MyClass from the compiled assembly
                Type myClassType = assembly.GetType(className2);
                dynamic myClassInstance = Activator.CreateInstance(myClassType);

                Form1_0.AllClassInstances.Add(myClassInstance);

                // Use MethodInfo to get the method information
                MethodInfo methodInfo = myClassType.GetMethod("SetForm1");
                if (methodInfo != null) methodInfo.Invoke(myClassInstance, new object[] { Form1_0 });

                // Assign myClassInstance to the property Form1_0.Andariel_0

                //Form1_0.Andariel_0 = new ((Andariel) myClassInstance);

                Form1_0.method_1("Loaded file: " + Path.GetFileName(ThisScriptPath), Color.DarkGreen);
            }
            catch (Exception ex)
            {
                Form1_0.method_1($"Exception occurred: {ex}", Color.Red);
            }
        }


        ExecuteScript(scriptCode2, className2, "MyMethod");*/
    }

    Andariel LoadAndarielDynamically()
    {
        // Load the assembly containing Andariel dynamically
        Assembly assembly = Form1_0.AllClassInstances[0] as Assembly;

        // Get the type of Andariel
        Type andarielType = assembly.GetType("Andariel"); // Replace "Namespace" with the namespace of Andariel

        // Create an instance of Andariel dynamically
        Andariel andarielInstance = (Andariel)Activator.CreateInstance(andarielType);

        return andarielInstance;
    }

    public void ExecuteScript(string scriptCode, string className, string methodName)
    {
        // Create an instance of the class
        object myClassInstance = Form1_0.AllClassInstances[0];

        // Get the type of the retrieved instance
        Type myClassType = myClassInstance.GetType();

        // Get the method dynamically based on method name
        MethodInfo methodInfo = myClassType.GetMethod(methodName);

        // Check if method was found
        if (methodInfo == null)
        {
            Console.WriteLine("Method not found: " + methodName);
            return;
        }

        // Execute the method on the instance
        methodInfo.Invoke(myClassInstance, null);
    }

    Assembly CompileScript(string scriptCode)
    {
        // Create a CSharpCodeProvider instance
        using (var codeProvider = new Microsoft.CSharp.CSharpCodeProvider())
        {
            // Set compiler parameters
            CompilerParameters compilerParams = new CompilerParameters();
            compilerParams.GenerateExecutable = false;
            compilerParams.GenerateInMemory = true;
            compilerParams.ReferencedAssemblies.Add("System.dll"); // Add reference to System.Windows.Forms
            compilerParams.ReferencedAssemblies.Add("System.Reflection.dll");
            compilerParams.ReferencedAssemblies.Add("System.Collections.dll");
            compilerParams.ReferencedAssemblies.Add("System.Windows.dll"); // Add reference to System.Windows.Forms
            compilerParams.ReferencedAssemblies.Add("System.Windows.Forms.dll"); // Add reference to System.Windows.Forms
            compilerParams.ReferencedAssemblies.Add("System.Linq.dll");
            compilerParams.ReferencedAssemblies.Add("System.Drawing.dll");
            //compilerParams.ReferencedAssemblies.Add("System.Text.dll");
            compilerParams.ReferencedAssemblies.Add("System.Threading.Tasks.dll");

            // Add reference to the current assembly (which includes Enums)
            compilerParams.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

            // Compile the script code
            CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(compilerParams, scriptCode);

            // Check for compilation errors
            if (compilerResults.Errors.HasErrors)
            {
                foreach (CompilerError error in compilerResults.Errors)
                {
                    Form1_0.method_1($"Error ({error.ErrorNumber}): {error.ErrorText}", Color.Red);
                }
                throw new InvalidOperationException("Script compilation failed.");
            }

            // Compilation succeeded, return the compiled assembly
            return compilerResults.CompiledAssembly;
        }
    }

}
