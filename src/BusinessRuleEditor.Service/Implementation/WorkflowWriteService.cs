using BusinessRuleEditor.Entities;
using BusinessRuleEditor.Repository;
using BusinessRuleEditor.Service;

namespace BusinessRuleEditor.Implementation
{
    public class WorkflowWriteService: IWorkflowWriteService
    {
        private readonly IWorkflowWriteRepository _workflowWriteRepository;
        public WorkflowWriteService(IWorkflowWriteRepository workflowWriteRepository)
            => _workflowWriteRepository = workflowWriteRepository;

        public string AddWorkflowCategory(string WorkflowCategoryName)
        {
            string response = _workflowWriteRepository.AddWorkflowCategory(WorkflowCategoryName); 
            return response;
        }

        public string AddRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName)
        {
            string response = _workflowWriteRepository.AddRuleUnderWorkflowCategory(WorkflowCategoryName, RuleName);
            return response;
        }

        public string AddUpdateRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail)
        {
            string response = _workflowWriteRepository.AddUpdateRuleExpressionDetails(WorkflowCategoryRuleDetail);
            return response;
        }
    }
}
