using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class ActivityObjectBase
    {
        public ActivityObjectBase()
        {
            this.Parent = null;
            this.Name = string.Empty;
        }

        public string ID { get; set; }

        public string HashCache { get; set; }

        public string Name { get; set; }

        public Group Parent;

        public virtual string GetDataToHash()
        {
            throw new NotImplementedException();
        }

        public string Path
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                this.AppendPath(sb);

                if (sb.Length == 0)
                {
                    return ProjectPath.PATH_DELIMITER;
                }
                else
                {
                    return sb.ToString();
                }
            }
        }

        private void AppendPath(StringBuilder sb)
        {
            if (this.Parent == null)
            {
                // We are the root
                if (!(this is RootGroup))
                {
                    throw new Exception("Broken Group Link");
                }

                // Don't add anything. Root path is an empty string
            }
            else
            {
                this.Parent.AppendPath(sb);
                sb.AppendFormat("{0}{1}", ProjectPath.PATH_DELIMITER, this.Name);
            }
        }
    }
}
