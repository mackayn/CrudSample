using System;
using Prism.Mvvm;
using SQLite;

namespace CrudSample.Model
{
    [Table("tbIncident")]
    public class Incident : BindableBase
    {
        private int _incid;
        private string _incidentType;
        private DateTime _dateCreated;
        private DateTime _lastUpdated;
        private string _reportDetails;

        [NotNull]
        [PrimaryKey, AutoIncrement]
        public int IncId
        {
            get { return _incid; }
            set { SetProperty(ref _incid, value); }
        }

        [NotNull]
        [MaxLength(20), Column("inctype")]
        public string IncType
        {
            get { return _incidentType; }
            set { SetProperty(ref _incidentType, value); }
        }

        [NotNull]
        [Column("datecreated")]
        public DateTime DateCreated
        {
            get { return _dateCreated; }
            set { SetProperty(ref _dateCreated, value); }
        }

        [NotNull]
        [Column("lastupdated")]
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            set { SetProperty(ref _lastUpdated, value); }
        }

        [NotNull]
        [MaxLength(400), Column("reportdetails")]
        public string ReportDetails
        {
            get { return _reportDetails; }
            set { SetProperty(ref _reportDetails, value); }
        }
    }
}
