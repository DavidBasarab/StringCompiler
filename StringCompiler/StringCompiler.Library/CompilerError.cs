namespace StringCompiler.Library
{
    public class CompilerError
    {
        private readonly System.CodeDom.Compiler.CompilerError _error;

        public string FormattedError
        {
            get { return string.Format("Error ({0}): {1} | Line = {2}", _error.ErrorNumber, _error.ErrorText, _error.Line); }
        }

        public System.CodeDom.Compiler.CompilerError SystemError
        {
            get { return _error; }
        }

        public CompilerError(System.CodeDom.Compiler.CompilerError error)
        {
            _error = error;
        }
    }
}