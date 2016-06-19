using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mx.core.Persistance
{
    public interface IProjectPersistor
    {
        bool ActivityExists(string path);
        bool GroupExists(string path);

        void CreateActivity(Activity activity);
        void CreateGroup(Group group);
        void UpdateActivity(Activity activity);
        void UpdateGroup(Group group);
        void DeleteActivity(Activity activity);
        void DeleteGroup(Group group);

        //void LoadGroup(string path, out Group group, bool recursive);
        //void LoadActivity(string path, out Activity activity);

        Group LoadProjectTree();
    }
}
