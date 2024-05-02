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

public class ScriptsRunner
{
    Form1 Form1_0;

    public void SetForm1(Form1 form1_1)
    {
        Form1_0 = form1_1;
    }

    public void ExcuteScripts_MethodFunction(object ThisClassInstance, string ThisClassName, string ThisMethodName, object[] ParametersList)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type myClassType = assembly.GetType(ThisClassName);

        if (myClassType == null)
        {
            Console.WriteLine("Class not found: " + ThisClassName);
            return;
        }

        MethodInfo methodInfo = myClassType.GetMethod(ThisMethodName);
        if (methodInfo != null) methodInfo.Invoke(ThisClassInstance, ParametersList);
    }

}
