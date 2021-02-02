using CommandLine;

namespace PncUniform.Shopping.UniformInventory.DbUp
{
    public class DbUpSettings
    {
        [Option('c', "connectionString", Required = true, HelpText = "The PNC Database connection string.")]
        public string ConnectionString { get; set; }
    }
}