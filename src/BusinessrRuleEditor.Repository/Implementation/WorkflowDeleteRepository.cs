using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository.Implementation
{
    public class WorkflowDeleteRepository : IWorkflowDeleteRepository
    {
        private readonly IConfigManagerRepository _configManager;
        private readonly IFileReaderRepository _fileReader;
        private readonly IFileWriterRepository _fileWriter;

        public WorkflowDeleteRepository(
            IConfigManagerRepository configManager,
            IFileReaderRepository fileReader,
            IFileWriterRepository fileWriter)
        {
            _configManager = configManager;
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public string RemoveWorkflowCategory(string WorkflowCategoryName)
        {
            List<Workflow> workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);
            foreach (Workflow workflow in workflows)
            {
                if (workflow.WorkflowName.Equals(WorkflowCategoryName))
                {
                    workflows.Remove(workflow);
                    break;
                }
            }
            string response = _fileWriter.WriteWorkflowDataAsync(workflows);
            return response;
        }

        public string RemoveRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName)
        {
            List<Workflow> workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);
            foreach (Workflow workflow in workflows)
            {
                if (workflow.WorkflowName.Equals(WorkflowCategoryName))
                {
                    foreach(Rule rule in workflow.Rules)
                    {
                        if(rule.RuleName.Equals(RuleName))
                        {
                            workflow.Rules.Remove(rule);
                            break;
                        }
                    }
                }
                break;
            }
            string response = _fileWriter.WriteWorkflowDataAsync(workflows);
            return response;
        }

        public string RemoveRuleExpressionDetails(WorkflowCategoryRuleDetail WorkflowCategoryRuleDetail)
        {
            throw new NotImplementedException();
        }
    }
}
