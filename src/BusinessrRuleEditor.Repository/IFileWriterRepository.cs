using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository
{
    public interface IFileWriterRepository
    {
        string WriteWorkflowDataAsync(List<Workflow> workflows);
        string WriteWorkflowDataAsync(string filePath, List<Workflow> workflows);
    }
}
