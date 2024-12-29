using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BusinessRuleEditor.Repository.Implementation
{
    public class ConfigManagerRepository : IConfigManagerRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ConfigManagerRepository(IConfiguration configuration, 
            IHostingEnvironment hostingEnvironment) 
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public string DatabaseConnection => _configuration["ConnectionStrings:NorthwindDatabase"]!;
        public string DevEmailId => _configuration["AppSeettings:DevEmailId"]!;
        public string ErrorType => _configuration["AppSeettings:ErrorType"]!;
        public string RuleExpressionType => _configuration["AppSeettings:RuleExpressionType"]!;
        

        private string WorkflowFileLocation => _configuration["AppSeettings:WorkflowFileLocation"]!;
        private string WorkflowFileName => _configuration["AppSeettings:WorkflowFileName"]!;



        public string WorkflowFilePath =>
            Path.Combine(_hostingEnvironment.ContentRootPath, this.WorkflowFileLocation, this.WorkflowFileName);

        public IConfigurationSection GetConfigurationSection(string key) => _configuration.GetSection(key);

        public string GetConnectionString(string connectionName)
        {
            return _configuration.GetConnectionString(connectionName)!;
        }
    }
}
