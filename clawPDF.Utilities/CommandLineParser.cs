using System.Collections.Generic;

namespace clawSoft.clawPDF.Utilities
{
    public class CommandLineParser
    {
        private readonly Dictionary<string, string> _args;

        public CommandLineParser(IEnumerable<string> args)
        {
            _args = AnalyzeCommandLine(args);
        }

        public bool HasArgument(string key)
        {
            return _args.ContainsKey(key.ToLowerInvariant());
        }

        public string GetArgument(string key)
        {
            return _args[key.ToLowerInvariant()];
        }

        private static Dictionary<string, string> AnalyzeCommandLine(IEnumerable<string> args)
        {
            var arguments = new Dictionary<string, string>();

            foreach (var arg in args)
            {
                if (string.IsNullOrEmpty(arg))
                    continue;

                var c = arg[0];
                if (c != '/' && c != '-')
                    continue;

                var s = arg.Substring(1);
                var pos = s.IndexOf('=');

                if (pos < 0)
                {
                    arguments.Add(s.ToLowerInvariant(), null);
                }
                else
                {
                    var argPair = s.Split(new[] { '=' }, 2);
                    arguments.Add(argPair[0].ToLowerInvariant(), argPair[1]);
                }
            }

            return arguments;
        }
    }
}