using BusinessRuleEditor.Entities;
using BusinessRuleEditor.Repository;
using BusinessRuleEditor.Service;

namespace BusinessRuleEditor.Implementation
{
    public class WorkflowDeleteService : IWorkflowDeleteService
    {

        private readonly IWorkflowDeleteRepository _workflowDeleteRepository;
        public WorkflowDeleteService(IWorkflowDeleteRepository workflowDeleteRepository)
            => _workflowDeleteRepository = workflowDeleteRepository;

        public string RemoveWorkflowCategory(string WorkflowCategoryName)
        {
            return _workflowDeleteRepository.RemoveWorkflowCategory(WorkflowCategoryName); 
        }

        public string RemoveRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName)
        {
           return _workflowDeleteRepository.RemoveRuleUnderWorkflowCategory(WorkflowCategoryName, RuleName);  
        }

        public string RemoveRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail)
        {
            return _workflowDeleteRepository.RemoveRuleExpressionDetails(WorkflowCategoryRuleDetail);
        }

    }
}
