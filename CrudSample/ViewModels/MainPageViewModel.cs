using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CrudSample.Model;
using CrudSample.Services;
using CrudSample.StringResources;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace CrudSample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigatedAware
    {
        // Services
        private readonly IDeviceService _deviceService;
        private readonly IIncidentService _incidentService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        // Commands
        private DelegateCommand _addCommand;
        private DelegateCommand _editCommand;
        private DelegateCommand _deleteCommand;

        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddIncident));

        public DelegateCommand EditCommand => _editCommand ?? (_editCommand = new DelegateCommand(EditIncident, () => IsItemSelected)
                                                                     .ObservesProperty(() => IsItemSelected));
        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteIncident, () => IsItemSelected)
                                                                 .ObservesProperty(() => IsItemSelected));

        public MainPageViewModel(IIncidentService incidentService, IDeviceService deviceService,
                                 IPageDialogService pageDialogService, INavigationService navigationService)
        {
            AppTitle = "Incident Manager";

            _deviceService = deviceService;
            _incidentService = incidentService;
            _pageDialogService = pageDialogService;
            _navigationService = navigationService;

            IsItemSelected = false;
            Incidents = new ObservableCollection<Incident>();

        }

        private async void AddIncident()
        {
            await _navigationService.NavigateAsync("Editor");
        }

        private async void EditIncident()
        {
            await _navigationService.NavigateAsync("Editor", 
                   new NavigationParameters { { NavigationParams.IncidentId, SelectedIncident } },true,false);
        }

        private async void DeleteIncident()
        {
            try
            {
                var res = await _pageDialogService.DisplayAlertAsync("Delete", "Deleted Selected Incident", "Yes", "No");
                if (res)
                {
                    var deleteOk = await _incidentService.DeleteIncident(SelectedIncident);
                    if (deleteOk > 0)
                    {
                        await RefreshDashboard();
                    }
                    else
                    {
                        await _pageDialogService.DisplayAlertAsync("Error", "Incident could not be deleted", "OK");
                    }
                }
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
            

        }

        private async Task<bool> RefreshDashboard()
        {
            try
            {
                IsItemSelected = false;
                SelectedIncident = null;

                var res = await _incidentService.GetAll();

                if (!Equals(res, null))
                    _deviceService.BeginInvokeOnMainThread(() =>
                    {
                        Incidents = new ObservableCollection<Incident>(res);
                        IncidentCount = res.Count;
                    });
                return true;
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error",ex.Message,"OK");
                return false;
            }
        }

        /// <summary>
        /// Properties
        /// </summary>

        private bool _isItemSelected;
        public bool IsItemSelected
        {
            get { return _isItemSelected; }
            set { SetProperty(ref _isItemSelected, value); }
        }

        private string _title;
        public string AppTitle
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private int _incidentCount;
        public int IncidentCount
        {
            get { return _incidentCount; }
            set { SetProperty(ref _incidentCount, value);}
        }

        private Incident _selectedIncident;
        public Incident SelectedIncident
        {
            get { return _selectedIncident; }
            set
            {
                SetProperty(ref _selectedIncident, value);
                IsItemSelected = !Equals(_selectedIncident,null);
            }
        }

        private ObservableCollection<Incident> _incidents;
        public ObservableCollection<Incident> Incidents
        {
            get { return _incidents; }
            set { SetProperty(ref _incidents, value);}
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {

            if (parameters.GetNavigationMode() == NavigationMode.New || 
                parameters.ContainsKey(NavigationParams.PageRefresh))
            {
                await RefreshDashboard();
            }
        }

    }
}
