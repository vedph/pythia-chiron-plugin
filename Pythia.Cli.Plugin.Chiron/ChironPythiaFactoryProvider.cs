using Corpus.Core.Plugin.Analysis;
using Fusi.Microsoft.Extensions.Configuration.InMemoryJson;
using Fusi.Tools.Config;
using Microsoft.Extensions.Configuration;
using Pythia.Chiron.Plugin;
using Pythia.Cli.Core;
using Pythia.Core.Config;
using Pythia.Core.Plugin.Analysis;
using Pythia.Sql.PgSql;
using SimpleInjector;
using System;

namespace Pythia.Cli.Plugin.Chiron
{
    /// <summary>
    /// Factory provider including Chiron analysis components.
    /// Currently this is just for demo purposes.
    /// </summary>
    /// <seealso cref="IPythiaFactoryProvider" />
    [Tag("factory-provider.chiron")]
    public sealed class ChironPythiaFactoryProvider : IPythiaFactoryProvider
    {
        public PythiaFactory GetFactory(string profileId, string profile, string connString)
        {
            if (profileId == null)
                throw new ArgumentNullException(nameof(profileId));
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));
            if (connString == null)
                throw new ArgumentNullException(nameof(connString));

            Container container = new Container();
            PythiaFactory.ConfigureServices(container,
                // Corpus.Core.Plugin
                typeof(StandardDocSortKeyBuilder).Assembly,
                // Pythia.Core.Plugin
                typeof(StandardTokenizer).Assembly,
                // Pythia.Chiron.Plugin
                typeof(LatSylCountSupplierTokenFilter).Assembly,
                // Pythia.Sql.PgSql
                typeof(PgSqlTextRetriever).Assembly);
            container.Verify();

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddInMemoryJson(profile);
            IConfiguration configuration = builder.Build();

            return new PythiaFactory(container, configuration)
            {
                ConnectionString = connString
            };
        }
    }
}
