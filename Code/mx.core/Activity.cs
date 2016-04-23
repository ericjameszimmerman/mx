using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class Activity : ActivityObjectBase
    {
        public Activity()
        {
            this.ShortName = string.Empty;
            this.Description = string.Empty;
        }
        
        public string Description { get; set; }

        public string ShortName { get; set; }

        public override string GetDataToHash()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Name);
            sb.Append(this.Parent.ID);
            sb.Append(this.ShortName);
            sb.Append(this.Description);

            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}",
                this.ID,
                this.Name);
        }
    }
}
