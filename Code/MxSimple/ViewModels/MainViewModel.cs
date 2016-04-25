using MxSimple.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MxSimple.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ActivityNavigationViewModel activityNavViewModel;

        public MainViewModel()
        {
            this.activityNavViewModel = ViewFactory.CreateActivityNavigationViewModel();
            this.ActiveControl = this.activityNavViewModel.View;
        }

        public ContentControl ActiveControl { get; set; }
    }
}
