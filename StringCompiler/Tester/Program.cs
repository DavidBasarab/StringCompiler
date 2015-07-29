using System;

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

            if (!success) PrintErrorsToConsole(stringCompiler);
            else
            {
                stringCompiler.RunMethod("First.Program", "Main", "Chopped");
                stringCompiler.RunMethod("First.Program", "Main2");
                var result = stringCompiler.RunMethod("First.Program", "Sum", 7, 3);

                Console.WriteLine("The Result of Sum is {0}", result);
            }
        }

        private static void PrintErrorsToConsole(StringCompiler.Library.StringCompiler stringCompiler)
        {
            foreach (var compilerError in stringCompiler.GetErrors()) Console.WriteLine(compilerError.FormattedError);
        }
    }
}