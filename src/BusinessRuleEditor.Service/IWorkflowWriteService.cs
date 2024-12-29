using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Service
{
    public interface IWorkflowWriteService
    {
        string AddWorkflowCategory(string WorkflowCategoryName);

        string AddRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName);

        string AddUpdateRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail);
    }
}
