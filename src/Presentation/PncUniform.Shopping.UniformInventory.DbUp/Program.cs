using System;
using CommandLine;
using DbUp.Engine;

namespace PncUniform.Shopping.UniformInventory.DbUp
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            DbUpSettings dbUpSettings = null;

            Parser.Default.ParseArguments<DbUpSettings>(args)
                .WithParsed(opts => dbUpSettings = opts)
                .WithNotParsed(_ => HandleParseError());

            // Could not parse settings
            if (dbUpSettings is null)
            {
                return -1;
            }

            var dbUpgradeResult = DatabaseUpgrader.RunDbUpgrade(dbUpSettings);

            if (!dbUpgradeResult.Successful)
            {
                return UnsuccessfulUpgrade(dbUpgradeResult);
            }

            return SuccessfulUpgrade();
        }

        private static void HandleParseError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Command Line parameters provided were not valid!");
            Console.ResetColor();
        }

        private static int UnsuccessfulUpgrade(DatabaseUpgradeResult dbUpgradeResult)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Unsuccessful Upgrade...");
            Console.WriteLine(dbUpgradeResult.Error);
            Console.ResetColor();
            return -1;
        }

        private static int SuccessfulUpgrade()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Successful Upgrade!");
            Console.ResetColor();
            return 0;
        }
    }
}
