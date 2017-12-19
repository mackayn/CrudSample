using System.IO;
using SQLite;
using Windows.Storage;
using CrudSample.Services;



namespace CrudSample.UWP.Services
{
    internal class PlatformDBUWP : IPlatformDb
    {
        private const string DbFile = "marineops.db";

        public PlatformDBUWP()
        {
            var path = Path.Combine(ApplicationData.Current.LocalFolder.Path, DbFile);
            var conn = new SQLiteAsyncConnection(path);
            Connection = conn;
            Filename = path;
        }

        public string Filename { get; }
        public SQLiteAsyncConnection Connection { get; }
    }
}