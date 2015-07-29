using System;
using System.CodeDom.Compiler;
using System.Text;
using Microsoft.CSharp;

namespace Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var code = @"
    using System;

    namespace First
    {
        public class Program
        {
            public static void Main()
            {
            " +
                       "Console.WriteLine(\"Hello, world! -- I made a change\");"
                       + @"
            }
        }
    }
";

            var provider = new CSharpCodeProvider();
            var parameters = new CompilerParameters();

            // Reference to System.Drawing library
            //parameters.ReferencedAssemblies.Add("System.Drawing.dll");
            // True - memory generation, false - external file generation
            parameters.GenerateInMemory = true;
            // True - exe file generation, false - dll file generation
            //parameters.GenerateExecutable = true;

            var results = provider.CompileAssemblyFromSource(parameters, code);

            if (results.Errors.HasErrors)
            {
                var sb = new StringBuilder();

                foreach (CompilerError error in results.Errors)
                {
                    //sb.AppendLine(string.Format("Error ({0}): {1}", error.ErrorNumber, error.ErrorText));

                    Console.WriteLine("Error ({0}): {1} | Line = {2}", error.ErrorNumber, error.ErrorText, error.Line);
                }

            }
            else
            {
                var assembly = results.CompiledAssembly;
                var program = assembly.GetType("First.Program");
                var main = program.GetMethod("Main");

                main.Invoke(null, null);
            }
        }
    }
}