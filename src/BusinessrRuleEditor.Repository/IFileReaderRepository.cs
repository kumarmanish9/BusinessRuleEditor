using BusinessRuleEditor.Entities;

namespace BusinessRuleEditor.Repository
{
    public interface IFileReaderRepository
    {
        List<Workflow> ReadWorkflowDataAsync();

        List<Workflow> ReadWorkflowDataAsync(string filePath);
    }
}
