using System.Collections.Generic;

namespace StringCompiler.Library
{
    /// <summary>
    ///     This will take in a method including parameters and return type.  You will need to provide it the complete method signature.
    /// </summary>
    public class MethodComplier
    {
        protected readonly StringCompiler _stringCompiler;

        public MethodComplier()
        {
            _stringCompiler = new StringCompiler();
        }

        public virtual bool CompileMethod(string method)
        {
            method = EnsureMethodIsStatic(method);

            var code = string.Format("using System; namespace MethodComplier {{ public class FragmentClass {{ {0} }} }}", method);

            return _stringCompiler.Compile(code);
        }

        public List<CompilerError> GetErrors()
        {
            return _stringCompiler.GetErrors();
        }

        public virtual object Run(params object[] args)
        {
            return _stringCompiler.RunMethod("MethodComplier.FragmentClass", 0, args);
        }

        protected object RunByName(string methodName)
        {
            return _stringCompiler.RunMethod("MethodComplier.FragmentClass", methodName);
        }

        private static string EnsureMethodIsStatic(string method)
        {
            method = RunStaticCheck(method, "public");
            method = RunStaticCheck(method, "private");
            method = RunStaticCheck(method, "protected");
            method = RunStaticCheck(method, "internal");

            return method;
        }

        private static string RunStaticCheck(string method, string accessor)
        {
            if (!method.Contains(accessor)) return method;

            var check = string.Format("{0} static", accessor);

            if (!method.Contains(check)) method = method.Replace(accessor, check);

            return method;
        }
    }
}