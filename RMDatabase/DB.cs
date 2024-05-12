using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RMDatabase.Models;
using System.Data.Common;

namespace RMDatabase
{
    public class SQLiteExtensionInterceptor : IDbConnectionInterceptor
    {
        public DbConnection ConnectionCreated(ConnectionCreatedEventData eventData, DbConnection result)
        {
            var sqliteConnection = (SqliteConnection)result;
            sqliteConnection.EnableExtensions();
            sqliteConnection.LoadExtension(@"unifuzz64.dll");
            return sqliteConnection;
        }
    }

    public class DB :  rmContext
    {
        public DB() : base(makeOptions())
        {
        }

        public static string sqLiteFile { get; set; } = string.Empty;

        static private  DbContextOptions<rmContext> makeOptions()
        {
            var opt = new DbContextOptionsBuilder<rmContext>();
            opt
                .AddInterceptors(new SQLiteExtensionInterceptor())
                .UseSqlite($"Data Source={sqLiteFile}")
                .UseLazyLoadingProxies();

            return opt.Options;
        }
    }
}
