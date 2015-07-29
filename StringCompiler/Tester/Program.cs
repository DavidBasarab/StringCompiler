using System;
using System.Collections.Generic;
using StringCompiler.Library;

namespace Tester
{
    internal class Program
    {
        private static void ExampleOfMethodDefinationNoParamters()
        {
            var method = "protected int SumNumbers() {{ return 1 + 4; }}";

            var complier = new MethodComplier();

            var success1 = complier.CompileMethod(method);

            if (!success1) PrintErrorsToConsole(complier.GetErrors());
            else
            {
                var result = complier.Run();

                Console.WriteLine("No Return Value Results = {0}", result);
            }
        }

        private static void ExampleOfMethodDefinationWithParamters()
        {
            var method = "public int SumNumbers(int x, int y) {{ return x + y; }}";

            var complier = new MethodComplier();

            var success1 = complier.CompileMethod(method);

            if (!success1) PrintErrorsToConsole(complier.GetErrors());
            else
            {
                var result = complier.Run(15, 4);

                Console.WriteLine("No Return Value Results = {0}", result);
            }
        }

        private static void ExampleOfMethodFragment()
        {
            var fragment = "Console.WriteLine(\"Hello WORLD!!!!!!\");";

            var complier = new VoidReturnMethodFragmentCompiler();

            var success1 = complier.CompileMethodFramgment(fragment);

            if (!success1) PrintErrorsToConsole(complier.GetErrors());
            else complier.Run();
        }

        private static void ExampleOfStringCompilerWithWholeProgram()
        {
            var code = @"
    using System;

    namespace First
    {
        public class Program
        {
            public static void Main(string someText)
            {
            " +
                       "Console.WriteLine(\"Hello, world! -- I made a change = {0}\", someText);"
                       + @" 

            }

            public static void Main2()
            {
            " +
                       "Console.WriteLine(\"What is with this?\");"
                       + @" 

            }

            public static int Sum(int x, int y)
            {
            " +
                       "return x + y;"
                       + @" 

            }
        }
    }
";

            var stringCompiler = new StringCompiler.Library.StringCompiler();

            var success = stringCompiler.Compile(code);

            if (!success) PrintErrorsToConsole(stringCompiler.GetErrors());
            stringCompiler.RunMethod("First.Program", "Main", "Chopped");
            stringCompiler.RunMethod("First.Program", "Main2");
            var result = stringCompiler.RunMethod("First.Program", "Sum", 7, 3);

            Console.WriteLine("The Result of Sum is {0}", result);
        }

        private static void Main(string[] args)
        {
            try
            {
                //ExampleOfMethodFragment();
                //ExampleOfStringCompilerWithWholeProgram();
                ExampleOfMethodDefinationNoParamters();
                ExampleOfMethodDefinationWithParamters();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error      : {0}", ex.Message);
                Console.WriteLine("StackTrace : {0}", ex.StackTrace);
            }
        }

        private static void PrintErrorsToConsole(List<CompilerError> compilerErrors)
        {
            foreach (var compilerError in compilerErrors) Console.WriteLine(compilerError.FormattedError);
        }
    }
}