using Microsoft.Extensions.Configuration;

namespace BusinessRuleEditor.Repository
{
    public interface IConfigManagerRepository
    {
        string DatabaseConnection { get; }
        string DevEmailId { get; }       
        string WorkflowFilePath { get; }

        string ErrorType { get; }
        string RuleExpressionType { get; }

        string GetConnectionString(string connectionName);

        IConfigurationSection GetConfigurationSection(string Key);
    }
}
