using DotNet.Testcontainers.Containers;
using FlexLabs.EntityFrameworkCore.Upsert.IntegrationTests.Base;
using FlexLabs.EntityFrameworkCore.Upsert.Tests.EF;
using Microsoft.EntityFrameworkCore;
using Testcontainers.FirebirdSql;
using Xunit;

namespace FlexLabs.EntityFrameworkCore.Upsert.IntegrationTests
{
#if !NOMSSQL
    public class DbTests_FireBird : DbTestsBase, IClassFixture<DbTests_FireBird.DatabaseInitializer>
    {
        public sealed class DatabaseInitializer : DatabaseInitializerFixture
        {
            public override DbDriver DbDriver => DbDriver.FireBird;

            protected override IContainer BuildContainer()
                => new FirebirdSqlBuilder().Build();

            protected override void ConfigureContextOptions(DbContextOptionsBuilder<TestDbContext> builder)
            {
                var connectionString = (TestContainer as IDatabaseContainer)?.GetConnectionString();
                builder.UseFirebird(connectionString);
            }
        }

        public DbTests_FireBird(DatabaseInitializer contexts)
            : base(contexts)
        { }
    }
#endif
}
