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

            var stringCompiler = new StringCompiler.Library.StringCompiler();

            var success = stringCompiler.Compile(code);

            if (!success)
            {
                foreach (var compilerError in stringCompiler.GetErrors())
                {
                    Console.WriteLine(compilerError.FormattedError);
                }
            }
            else
            {
                stringCompiler.RunMethod("First.Program", "Main");
            }
        }
    }
}