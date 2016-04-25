using mx.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaf.Services;

namespace mx.services
{
    public class DataService : ServiceBase, IDataService
    {
        private ActivityCollection collection;

        public DataService()
        {

        }

        public RootGroup GetProjectHierarchy()
        {
            this.collection = new ActivityCollection();

            RootGroup root = collection.Root;
            Group group = new Group() { Name = "Group 1" };
            collection.AddNew(root, group);

            Activity activity1 = new Activity() { Name = "Activity 1", ShortName = "GRP1-1", Description = "The first task" };
            Activity activity2 = new Activity() { Name = "Activity 2", ShortName = "GRP1-2", Description = "The second task" };
            Activity activity3 = new Activity() { Name = "Activity 3", ShortName = "GRP1-3", Description = "The third task" };
            Activity activity4 = new Activity() { Name = "Activity 4", ShortName = "ROOT-1", Description = "The fourth task" };
            Activity activity5 = new Activity() { Name = "Activity 5", ShortName = "ROOT-2", Description = "The fifth task" };

            collection.AddNew(group, activity1);
            collection.AddNew(group, activity2);
            collection.AddNew(group, activity3);
            collection.AddNew(root, activity4);
            collection.AddNew(root, activity5);

            return root;
        }
    }
}
