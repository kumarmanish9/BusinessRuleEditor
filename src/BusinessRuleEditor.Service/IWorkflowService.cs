using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Service
{
    public interface IWorkflowService
    {
        List<WorkflowCategory> GetWorkflowCategoryAsync();

        List<WorkflowCategoryRule> GetWorkflowCategoryRulesAsync(string workflowCategory);

        WorkflowCategoryRuleDetail GetCategoryRuleDetailsAsync(string workflowCategory, string ruleName);
    }
}
