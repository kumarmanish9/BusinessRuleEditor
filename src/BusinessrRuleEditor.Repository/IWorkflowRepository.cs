using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository
{
    public interface IWorkflowRepository
    {
        List<WorkflowCategory> GetWorkflowCategoryAsync();

        List<WorkflowCategoryRule> GetWorkflowCategoryRulesAsync(string workflowCategory);

        Rule GetCategoryRuleDetailsAsync(string workflowCategory, string ruleName);
    }
}
