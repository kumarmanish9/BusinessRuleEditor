using BusinessRuleEditor.Entities;
using BusinessRuleEditor.Repository;
using BusinessRuleEditor.Service;

namespace BusinessRuleEditor.Implementation
{
    public class WorkflowService : IWorkflowService
    {
        private readonly IWorkflowRepository _workflowRepository;
        public WorkflowService(IWorkflowRepository workflowRepository) 
            => _workflowRepository = workflowRepository;

        public List<WorkflowCategory> GetWorkflowCategoryAsync() =>
            _workflowRepository.GetWorkflowCategoryAsync();

        public List<WorkflowCategoryRule> GetWorkflowCategoryRulesAsync(string workflowCategory)
        {
            if (string.IsNullOrWhiteSpace(workflowCategory)) return new();
            return _workflowRepository.GetWorkflowCategoryRulesAsync(workflowCategory);
        }

        public WorkflowCategoryRuleDetail GetCategoryRuleDetailsAsync(string workflowCategory, string ruleName)
        {
            var rule = _workflowRepository.GetCategoryRuleDetailsAsync(workflowCategory, ruleName);
            if (rule != null)
            {
                return new WorkflowCategoryRuleDetail()
                {
                    WorkflowName = workflowCategory,
                    RuleName = ruleName,

                    SuccessEvent = rule.SuccessEvent,
                    ErrorMessage = rule.ErrorMessage,
                    ErrorType = rule.ErrorType,
                    RuleExpressionType = rule.RuleExpressionType,
                    Expression = rule.Expression
                };
            }
            return new WorkflowCategoryRuleDetail();
        }

    }
}
