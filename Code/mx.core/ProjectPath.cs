using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class ProjectPath
    {
        public const string PATH_DELIMITER = "/";
        
        public static string Combine(string basePath, string append)
        {
            int count = 0;

            if (basePath.EndsWith(PATH_DELIMITER))
            {
                count++;
            }

            if (append.StartsWith(PATH_DELIMITER))
            {
                count++;
            }
              
            if (count == 0)
            {
                return (basePath + PATH_DELIMITER + append);
            }
            else if (count == 1)
            {
                return (basePath + append);
            }
            else
            {
                return (basePath + append.Substring(PATH_DELIMITER.Length));
            }
        }
    }
}
