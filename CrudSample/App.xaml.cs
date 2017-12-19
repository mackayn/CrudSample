using CrudSample.Services;
using CrudSample.ViewModels;
using Prism.Unity;
using CrudSample.Views;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace CrudSample
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("app:///MainNavigationPage/Dashboard");
        }

        protected override void RegisterTypes()
        {
            // Register navigation views
            Container.RegisterTypeForNavigation<MainNavigationPage>();
            Container.RegisterTypeForNavigation<MainPage, MainPageViewModel>("Dashboard");
            Container.RegisterTypeForNavigation<IncidentEditor, IncidentEditorViewModel>("Editor");

            // register core services
            Container.RegisterType<IIncidentService, IncidentService>(new TransientLifetimeManager());
        }
    }
}
