using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Service
{
    public interface IWorkflowDeleteService
    {
        string RemoveWorkflowCategory(string WorkflowCategoryName);

        string RemoveRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName);

        string RemoveRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail);
    }
}
