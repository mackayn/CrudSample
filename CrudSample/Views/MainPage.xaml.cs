using Prism.Navigation;
using Xamarin.Forms;

namespace CrudSample.Views
{
    public partial class MainPage : ContentPage, IDestructible
    {
        public MainPage()
        {
            InitializeComponent();
            ToolbarItemEdit.IsVisible = false;
            ToolbarItemDelete.IsVisible = false;
        }

        /// <summary>
        /// Force page resources to be released so page can be GC'd
        /// </summary>
        public void Destroy()
        {
            ListViewIncidents.Behaviors.Clear();
        }
    }
}
