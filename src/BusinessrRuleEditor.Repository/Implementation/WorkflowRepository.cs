using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository.Implementation
{
    public class WorkflowRepository : IWorkflowRepository
    {
        //If database connection required
        //private readonly ApplicationDbContext _dbContext;

        private readonly IConfigManagerRepository _configManager;
        private readonly IFileReaderRepository _fileReader;

        public WorkflowRepository(
            IConfigManagerRepository configManager,
            IFileReaderRepository fileReader)
        {
            _configManager = configManager;
            _fileReader = fileReader;
        }

        public List<WorkflowCategory> GetWorkflowCategoryAsync()
        {
            List<WorkflowCategory> workflowCategorys = new();
            var workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);

            if (workflows != null)
            {
                workflowCategorys = workflows.Select(x => new WorkflowCategory
                {
                    Workflow = x.WorkflowName
                }).ToList();
            }
            return workflowCategorys;
        }

        public List<WorkflowCategoryRule> GetWorkflowCategoryRulesAsync(string workflowCategory)
        {
            List<WorkflowCategoryRule> workflowCategoryRules = new();
            var workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);

            var rules = workflows.Where(w => w.WorkflowName.Equals(workflowCategory)).FirstOrDefault();
            if (rules!.Rules != null)
            {
                workflowCategoryRules = rules.Rules.Select(x => new WorkflowCategoryRule
                {
                    Rule = x.RuleName
                }).ToList();
            }
            return workflowCategoryRules;
        }

        public Rule GetCategoryRuleDetailsAsync(string workflowCategory, string ruleName)
        {
            var workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);
            var rules = workflows.Where(w => w.WorkflowName.Equals(workflowCategory)).FirstOrDefault()!;
            Rule rule = rules.Rules.Where(r => r.RuleName.Equals(ruleName)).FirstOrDefault()!;
            return rule!;
        }
    }
}
