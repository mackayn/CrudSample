using System;
using System.IO;
using CrudSample.Services;
using SQLite;
using Xamarin.Forms;

namespace CrudSample.Droid.Services
{
    internal class PlatformDBAndroid : IPlatformDb
    {
        private const string DbFile = "marineops.db";

        public PlatformDBAndroid()
        {
            if (Connection == null)
            {
                var folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                Filename = Path.GetFullPath(Path.Combine(folder, DbFile));
                //Connection = new SQLiteConnection(Filename);
                Connection = new SQLiteAsyncConnection(Filename);
            }
        }

        public string Filename { get; private set; }
        public SQLiteAsyncConnection Connection { get; private set; }
    }
}