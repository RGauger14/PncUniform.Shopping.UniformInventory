using System;
using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Support;

namespace PncUniform.Shopping.UniformInventory.DbUp
{
    /// <summary>
    /// The processes required behind a database upgrade using DbUp.
    /// </summary>
    public static class DatabaseUpgrader
    {
        private const string ScriptsLocation = "PncUniform.Shopping.UniformInventory.DbUp.Scripts";
        private const int CommandTimeout = 120;

        /// <summary>
        /// Runs the database upgrade using DbUp.
        /// </summary>
        /// <param name="settings">The settings.</param>
        public static DatabaseUpgradeResult RunDbUpgrade(DbUpSettings settings)
        {
            // Create the database if it doesn't already exist
            EnsureDatabase.For.SqlDatabase(settings.ConnectionString, CommandTimeout);

            // Run the scripts required for DbUp to operate
            return PerformDbUpMigration(settings);
        }

        /// <summary>
        /// Performs the initial database upgrade setup.
        /// </summary>
        /// <param name="settings">The settings.</param>
        private static DatabaseUpgradeResult PerformDbUpMigration(DbUpSettings settings)
        {
            var assemblyFiles = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            // Create initial schema for migration logging
            var dbInitialUpgrader = DeployChanges.To
                .SqlDatabase(settings.ConnectionString)
                .WithScriptsEmbeddedInAssembly(
                    Assembly.GetExecutingAssembly(),
                    x => x.StartsWith($"{ScriptsLocation}.DeploymentScripts.", StringComparison.InvariantCultureIgnoreCase),
                    new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 0 })
                .LogToConsole()
                .Build();

            return dbInitialUpgrader.PerformUpgrade();
        }
    }
}