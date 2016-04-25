using mx.core;
using mx.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zaf.Services;

namespace MxSimple.ViewModels
{
    public class ActivityTreeViewModel : ViewModelBase
    {
        public ActivityTreeViewModel()
        {
            IDataService dataService = ServiceLocator.Resolve<IDataService>();
            this.Root = dataService.GetProjectHierarchy();
        }

        public RootGroup Root { get; set; }

        public List<ActivityObjectBase> Collection
        {
            get { return Root.Items; }
        }
    }
}
