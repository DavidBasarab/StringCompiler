using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CSharp;
using SystemCompilerError = System.CodeDom.Compiler.CompilerError;

namespace StringCompiler.Library
{
    public class StringCompiler
    {
        private readonly CompilerParameters _parameters;
        private readonly CSharpCodeProvider _provider;
        private CompilerResults _compileResults;

        private bool CompileErrors
        {
            get { return _compileResults != null && _compileResults.Errors.HasErrors; }
        }

        private bool NoCompileErrors
        {
            get { return _compileResults == null || !_compileResults.Errors.HasErrors; }
        }

        public StringCompiler()
        {
            _provider = new CSharpCodeProvider();

            _parameters = new CompilerParameters
                          {
                                  GenerateInMemory = true
                          };
        }

        public bool Compile(string code)
        {
            _compileResults = _provider.CompileAssemblyFromSource(_parameters, code);

            return !_compileResults.Errors.HasErrors;
        }

        public List<CompilerError> GetErrors()
        {
            return NoCompileErrors ? new List<CompilerError>() : (from SystemCompilerError systemError in _compileResults.Errors select new CompilerError(systemError)).ToList();
        }

        public object RunMethod(string typeName, string methodName, params object[] args)
        {
            if (CompileErrors) return null;

            var program = _compileResults.CompiledAssembly.GetType(typeName);

            var method = program.GetMethod(methodName);

            return method.Invoke(null, args);
        }
    }
}