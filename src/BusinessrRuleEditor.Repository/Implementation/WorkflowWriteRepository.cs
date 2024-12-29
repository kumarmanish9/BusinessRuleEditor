using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository.Implementation
{
    public class WorkflowWriteRepository : IWorkflowWriteRepository
    {
        private readonly IConfigManagerRepository _configManager;
        private readonly IFileReaderRepository _fileReader;
        private readonly IFileWriterRepository _fileWriter;

        public WorkflowWriteRepository(
            IConfigManagerRepository configManager,
            IFileReaderRepository fileReader,
            IFileWriterRepository fileWriter
            )
        {
            _configManager = configManager;
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        public string AddWorkflowCategory(string WorkflowCategoryName)
        {
            List<Workflow> workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);
            var workflow = workflows.Where(wf => wf.WorkflowName == WorkflowCategoryName).FirstOrDefault();
            if(workflow != null)
            {
                return "Duplicate workflow category not allowed!";
            }
            else
            {
                Workflow wf = new Workflow();
                wf.WorkflowName = WorkflowCategoryName;
                //wf.Rules = new List<Rule> { new Rule() };
                workflows.Add(wf);
                string response = _fileWriter.WriteWorkflowDataAsync(workflows);
                return response;
            }
        }

        public string AddRuleUnderWorkflowCategory(string WorkflowCategoryName, string RuleName)
        {
            List<Workflow> workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);
            var workflow = workflows.Where(wf => wf.WorkflowName == WorkflowCategoryName).FirstOrDefault();
            if (workflow!.Rules != null)
            {
                var rule = workflow.Rules.Where(r => r.RuleName == RuleName).FirstOrDefault();
                if(rule != null)
                {
                    return "Duplicate Rule not allowed!";
                }
                else
                {
                    foreach(Workflow wf in workflows)
                    {
                        if (wf.WorkflowName.Equals(WorkflowCategoryName))
                        {
                            wf.Rules.Add(new Rule() { RuleName = RuleName });
                            break;
                        }
                    }
                    string writeResponse = _fileWriter.WriteWorkflowDataAsync(workflows);
                    return writeResponse;
                }
            }
            else
            {
                foreach (Workflow wf in workflows)
                {
                    if (wf.WorkflowName.Equals(WorkflowCategoryName))
                    {
                        wf.Rules = new List<Rule> { new Rule() {  RuleName = RuleName } };
                        break;
                    }
                }
                string writeResponse = _fileWriter.WriteWorkflowDataAsync(workflows);
                return writeResponse;
            }
        }

        public string AddUpdateRuleExpressionDetails(WorkflowCategoryRuleDetail ruleDetail)
        {
            List<Workflow> workflows = GetUpdatedWorkflow(ruleDetail);
            string response = _fileWriter.WriteWorkflowDataAsync(_configManager.WorkflowFilePath, workflows);
            return response;
        }

        private List<Workflow> GetUpdatedWorkflow(WorkflowCategoryRuleDetail ruleDetail)
        {
            List<Workflow> workflows = _fileReader.ReadWorkflowDataAsync(_configManager.WorkflowFilePath);
            if (workflows != null)
            {
                foreach (Workflow workflow in workflows)
                {
                    if (workflow.WorkflowName.Equals(ruleDetail.WorkflowName))
                    {
                        foreach (Rule rule in workflow.Rules)
                        {
                            if (rule.RuleName.Equals(ruleDetail.ActualRuleName))
                            {
                                rule.RuleName = ruleDetail.RuleName;
                                rule.ErrorMessage = ruleDetail.ErrorMessage;
                                rule.ErrorType = ruleDetail.ErrorType;
                                rule.RuleExpressionType = ruleDetail.RuleExpressionType;
                                rule.SuccessEvent = ruleDetail.SuccessEvent;
                                rule.Expression = ruleDetail.Expression;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            return workflows!;
        }
    }
}
