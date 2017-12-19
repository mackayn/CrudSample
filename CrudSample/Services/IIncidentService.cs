using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudSample.Model;

namespace CrudSample.Services
{
    public interface IIncidentService
    {
        Task<int> AddIncident(Incident item);
        Task<int> UpdateIncident(Incident item);
        Task<int> DeleteIncident(Incident item);
        Task<List<Incident>> GetAll();
    }
}
