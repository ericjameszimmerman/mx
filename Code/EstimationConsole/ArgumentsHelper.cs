using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationConsole
{
    public class ArgumentsHelper
    {
        public static string[] ConvertStringToArgs(string command, string input)
        {
            StringBuilder sb = new StringBuilder();
            List<string> argList = new List<string>();
            bool inQuotes = false;
            argList.Add(command);

            foreach (char c in input)
            {
                if (inQuotes)
                {
                    if (c == '"')
                    {
                        Add(argList, sb);
                        inQuotes = false;
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                else
                {
                    if (c == '"')
                    {
                        inQuotes = true;
                    }
                    else if (c == ' ' || c == '\t')
                    {
                        Add(argList, sb);
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
            }

            Add(argList, sb);

            return argList.ToArray();
        }

        public static void Add(List<string> argList, StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                argList.Add(sb.ToString());
            }

            sb.Clear();
        }
    }
}
