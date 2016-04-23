using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class Group : ActivityObjectBase
    {
        public Group()
        {
            this.Items = new List<ActivityObjectBase>();
        }

        public List<ActivityObjectBase> Items { get; set; }

        public void AddItem(ActivityObjectBase activity)
        {
            this.Items.Add(activity);
        }

        public override string GetDataToHash()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Name);
            sb.Append((this.Parent == null) ? string.Empty : this.Parent.ID);

            return sb.ToString();
        }
    }
}
