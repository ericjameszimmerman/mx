using mx.services;
using MxSimple.ViewModels;
using System.Windows;
using Zaf.Services;

namespace MxSimple
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure service locator
            //ServiceLocator.RegisterSingleton<IDialogService, DialogService>();
            //ServiceLocator.RegisterSingleton<IWindowViewModelMappings, WindowViewModelMappings>();
            ServiceLocator.RegisterSingleton<IDataService, DataService>();
            //ServiceLocator.RegisterSingleton<ISettingsService, SettingsService>();
            //ServiceLocator.RegisterSingleton<ITaskEngineService, TaskEngineService>();
            //ServiceLocator.RegisterSingleton<IFeedbackService, FeedbackService>();
            //ServiceLocator.RegisterSingleton<ILoggingService, LoggingService>();
            //ServiceLocator.Register<IOpenFileDialog, OpenFileDlgViewModel>();
            //ServiceLocator.Register<ISaveFileDialog, SaveFileDlgViewModel>();

            //ITaskEngineService service = ServiceLocator.Resolve<ITaskEngineService>();
            //service.TaskDispatcher = Application.Current.Dispatcher;
            //service.Start();

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

            // Create and show main window
            MainWindow view = new MainWindow();
            MainViewModel viewModel = new MainViewModel();
            viewModel.View = view;
            view.DataContext = viewModel;
            view.Show();
        }

        void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //try
            //{
            //    ILoggingService service = ServiceLocator.Resolve<ILoggingService>();
            //    service.Error("Unhandled Exception", e.Exception);
            //}
            //catch (Exception)
            //{

            //}

            //try
            //{
            //    IFeedbackService feedbackService = ServiceLocator.Resolve<IFeedbackService>();
            //    feedbackService.ShowError("An Unhandled Exception Occurred", e.Exception.Message);
            //}
            //catch (Exception)
            //{

            //}

            //e.Handled = true;
        }

        private void ShutdownServices()
        {
            //try
            //{
            //    ITaskEngineService service = ServiceLocator.Resolve<ITaskEngineService>();
            //    service.Dispose();
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //}
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.ShutdownServices();
            base.OnExit(e);
        }
    }
}
