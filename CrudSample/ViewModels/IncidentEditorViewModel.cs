using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using CrudSample.Model;
using CrudSample.Services;
using CrudSample.StringResources;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;

namespace CrudSample.ViewModels
{
    public class IncidentEditorViewModel: BindableBase, INavigatingAware
    {
        // Commands
        private DelegateCommand _saveCommand;
        private DelegateCommand _cancelCommand;

        // Services
        private readonly IIncidentService _incidentService;
        private readonly IPageDialogService _pageDialogService;
        private readonly INavigationService _navigationService;

        //Commands

        public DelegateCommand SaveCommand => _saveCommand ?? (_saveCommand = new DelegateCommand(SaveIncident));

        public DelegateCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new DelegateCommand(Cancel));

        public IncidentEditorViewModel(IIncidentService incidentService, INavigationService navigationService,
                                       IPageDialogService pageDialogService)
        {
            _incidentService = incidentService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;

            IncidentTypes = new List<string>() {"Burn","Fall","Slip","Stress"};
        }

        private async void SaveIncident()
        {
            try
            {
                var res = 0;

                if (!CanSave)
                {
                    await _pageDialogService.DisplayAlertAsync("Missing info", "Select a type and enter report details", "OK");
                    return; // I know it's bad but I'm tired and INPC is not working in the button, need to investigate
                }

                if (SelectedIncident.IncId == 0)
                {
                    res = await _incidentService.AddIncident(SelectedIncident);
                }
                else
                {
                    res = await _incidentService.UpdateIncident(SelectedIncident);
                }

                if (res > 0)
                {
                    await _navigationService.GoBackAsync(new NavigationParameters {{NavigationParams.PageRefresh,null}},
                        animated: false);
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Error", "incident could not be saved", "OK");
                }
            }
            catch (Exception ex)
            {
                await _pageDialogService.DisplayAlertAsync("Error", ex.Message, "OK");
                throw;
            }

        }

        private async void Cancel()
        {
            await _navigationService.GoBackAsync();
        }

        private List<string> _incidentTypes;
        public List<string> IncidentTypes
        {
            get { return _incidentTypes; }
            set { SetProperty(ref _incidentTypes, value); }
        }

        private Incident _incident;
        public Incident SelectedIncident
        {
            get { return _incident; }
            set { SetProperty(ref _incident, value); }
        }

        private string _editMode;
        public string EditMode
        {
            get { return _editMode; }
            set { SetProperty(ref _editMode, value); }
        }

        public bool CanSave => !Equals(SelectedIncident, null) && 
                               !Equals(SelectedIncident.IncType, null) && 
                               !Equals(SelectedIncident.ReportDetails, null);


        public void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey(NavigationParams.IncidentId))
            {
                SelectedIncident = parameters.GetValue<Incident>(NavigationParams.IncidentId);
                EditMode = "Edit";
            }
            else
            {
                EditMode = "Add";
                SelectedIncident = new Incident() { DateCreated = DateTime.Now, IncType = null, ReportDetails = null};
            }
        }
    }
}
