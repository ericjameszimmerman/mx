using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MxSimple.ViewModels
{
    public class ActivityNavigationViewModel : ViewModelBase
    {
        private ActivityTreeViewModel activityTreeViewModel;
        private ActivityViewModel activityViewModel;

        public ActivityNavigationViewModel(ActivityTreeViewModel treeViewModel, ActivityViewModel viewModel)
        {
            this.activityTreeViewModel = treeViewModel;
            this.activityViewModel = viewModel;

            this.ActiveTreeView = this.activityTreeViewModel.View;
            this.ActiveItemView = this.activityViewModel.View;
        }

        public ContentControl ActiveTreeView { get; set; }

        public ContentControl ActiveItemView { get; set; }
    }
}
