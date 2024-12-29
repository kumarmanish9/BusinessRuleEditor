using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository
{
    public interface IWorkflowWriteRepository
    {
        string AddWorkflowCategory(string WorkflowCategoryName);
        
        string AddRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName);

        string AddUpdateRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail);
    }
}
