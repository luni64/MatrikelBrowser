using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using RMDatabase.Microsoft.EntityFrameworkCore.Query;
using RMDatabase.Models;
using System.Data.Common;

namespace RMDatabase
{


    public static class DelegateDecompilerDbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder AddDelegateDecompiler(this DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.AddInterceptors(new DelegateDecompilerQueryPreprocessor2());
    }


    namespace Microsoft.EntityFrameworkCore.Query
    {
        using System.Linq.Expressions;
        using DelegateDecompiler;
        public class DelegateDecompilerQueryPreprocessor2 : IQueryExpressionInterceptor
        {
            Expression IQueryExpressionInterceptor.QueryCompilationStarting(Expression queryExpression, QueryExpressionEventData eventData)
                => DecompileExpressionVisitor.Decompile(queryExpression);
        }

        public interface IQueryPreprocessor
        {
            Expression Process(Expression query);
        }

        public class DelegateDecompilerQueryPreprocessor : IQueryExpressionInterceptor
        {
            public Expression Process(Expression query) => DecompileExpressionVisitor.Decompile(query);
        }


    }


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

    public class DB : rmContext
    {
        public DB() : base(makeOptions())
        {
        }

        public static string sqLiteFile { get; set; } = string.Empty;

        static private DbContextOptions<rmContext> makeOptions()
        {
            var opt = new DbContextOptionsBuilder<rmContext>();



            opt
                .AddDelegateDecompiler()
                .AddInterceptors(new SQLiteExtensionInterceptor())
                .UseSqlite($"Data Source={sqLiteFile}")
                .UseLazyLoadingProxies();

            return opt.Options;
        }
    }
}
