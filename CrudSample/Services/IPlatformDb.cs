using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudSample.Services
{
    public interface IPlatformDb
    {
        /// <summary>
        ///     The filename we are using to store the local DB.
        /// </summary>
        /// <value>The filename.</value>
        string Filename { get; }

        /// <summary>
        ///     The platform-specific connection created for iOS or Android
        /// </summary>
        /// <value>The connection.</value>
        SQLite.SQLiteAsyncConnection Connection { get; }
    }
}
