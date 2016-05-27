using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core
{
    public class ActivityCollection
    {
        private Dictionary<string, ActivityObjectBase> activityLookup;

        int nextId = 1;

        public ActivityCollection()
        {
            this.activityLookup = new Dictionary<string, ActivityObjectBase>();
            this.Root = new RootGroup();
        }

        public bool TryGetActivity(string key, out ActivityObjectBase item)
        {
            return this.activityLookup.TryGetValue(key, out item);
        }

        public IEnumerable<ActivityObjectBase> Items
        {
            get
            {
                return this.activityLookup.Values;
            }
        }

        public void AddNew(Group group, ActivityObjectBase item)
        {
            item.ID = Guid.NewGuid().ToString(); // GetNextId().ToString();
            item.Parent = group;
            this.activityLookup.Add(item.ID, item);
            group.AddItem(item);
        }

        public RootGroup Root { get; private set; }

        private int GetNextId()
        {
            int id = nextId;
            nextId++;
            return id;
        }
    }
}
