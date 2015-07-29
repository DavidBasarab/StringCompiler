namespace StringCompiler.Library
{
    /// <summary>
    ///     This will take the internal guts of a void method fragment.  It accepts no parameters and returns no value.
    ///     The only good this will do you is if you wanted to run a bunch of lines and made sure they performed the way you expected (i.e. no errors)
    /// </summary>
    public class VoidReturnMethodFragmentCompiler : MethodComplier
    {
        public bool CompileMethodFramgment(string methodFragment)
        {
            var fullMethod = string.Format("public static void MethodToRun() {{ {0} }}", methodFragment);

            return CompileMethod(fullMethod);
        }

        public override object Run(params object[] args)
        {
            return RunByName("MethodToRun");
        }
    }
}