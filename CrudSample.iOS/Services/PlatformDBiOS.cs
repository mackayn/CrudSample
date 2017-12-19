using System;
using System.IO;
using CrudSample.Services;
using SQLite;

namespace CrudSample.iOS.Services
{
    internal class PlatformDBiOS : IPlatformDb
    {
        private const string DbFile = "marineops.db";

        public PlatformDBiOS()
        {
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            Filename = Path.GetFullPath(Path.Combine(folder, DbFile)); 
            Connection = new SQLiteAsyncConnection(Filename);
        }

        public string Filename { get; private set; }
        public SQLiteAsyncConnection Connection { get; private set; }
    }
}