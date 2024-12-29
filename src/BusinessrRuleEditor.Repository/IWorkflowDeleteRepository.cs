using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository
{
    public interface IWorkflowDeleteRepository
    {
        string RemoveWorkflowCategory(string WorkflowCategoryName);

        string RemoveRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName);

        string RemoveRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail);
    }
}
