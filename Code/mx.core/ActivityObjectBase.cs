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
        
        public string Name { get; set; }

        public Group Parent;

        public virtual string GetDataToHash()
        {
            throw new NotImplementedException();
        }
    }
}
