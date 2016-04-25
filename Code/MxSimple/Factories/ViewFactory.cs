using MxSimple.ViewModels;
using MxSimple.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MxSimple.Factories
{
    public class ViewFactory
    {
        public static ActivityTreeViewModel CreateActivityTreeView()
        {
            ActivityTreeView view;

            view = new ActivityTreeView();
            ActivityTreeViewModel viewModel = new ActivityTreeViewModel();
            viewModel.View = view;
            view.DataContext = viewModel;
            return viewModel;
        }

        public static ActivityViewModel CreateActivityView()
        {
            ActivityView view;

            view = new ActivityView();
            ActivityViewModel viewModel = new ActivityViewModel();
            viewModel.View = view;
            view.DataContext = viewModel;
            return viewModel;
        }

        public static ActivityNavigationViewModel CreateActivityNavigationViewModel()
        {
            ActivityNavigationView view;
            ActivityTreeViewModel activityTreeViewModel = CreateActivityTreeView();
            ActivityViewModel activityViewModel = CreateActivityView();

            view = new ActivityNavigationView();
            ActivityNavigationViewModel viewModel = new ActivityNavigationViewModel(activityTreeViewModel, activityViewModel);
            viewModel.View = view;
            view.DataContext = viewModel;
            return viewModel;
        }
    }
}
