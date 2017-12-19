using System.Collections.Generic;
using System.Threading.Tasks;
using CrudSample.Model;

namespace CrudSample.Services
{
    public class IncidentService: IIncidentService
    {
        private readonly IPlatformDb _platformDb;

        public IncidentService(IPlatformDb platformDb)
        {
            _platformDb = platformDb;
            _platformDb.Connection.CreateTableAsync<Incident>().Wait();
        }

        public async Task<int> AddIncident(Incident item)
        {
            var result = await _platformDb.Connection.InsertAsync(item);
            return result;
        }

        public async Task<int> UpdateIncident(Incident item)
        {
            var result = await _platformDb.Connection.UpdateAsync(item);
            return result;
        }

        public async Task<int> DeleteIncident(Incident item)
        {
            var result = await _platformDb.Connection.DeleteAsync(item);
            return result;
        }

        public Task<List<Incident>> GetAll()
        {
            var result = _platformDb.Connection.Table<Incident>().OrderByDescending(t => t.DateCreated).ToListAsync();
            return result;
        }
    }
}
