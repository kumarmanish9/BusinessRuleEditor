using BusinessRuleEditor.Entities;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BusinessRuleEditor.Repository.Implementation
{
    public class JsonFileReaderRepository : IFileReaderRepository, IFileWriterRepository
    {
        private IConfigManagerRepository _configManager;  
        public JsonFileReaderRepository(IConfigManagerRepository configManager) => _configManager = configManager;

        #region Read File
        public List<Workflow> ReadWorkflowDataAsync()
        {
            string filePath = _configManager.WorkflowFilePath;
            return ReadWorkflowDataAsync(filePath);
        }

        public List<Workflow> ReadWorkflowDataAsync(string filePath) 
        {
            if (File.Exists(filePath))
            {
                var jsonData = File.ReadAllText(filePath);
                var workflow = JsonSerializer.Deserialize<List<Workflow>>(jsonData);
                if (workflow != null)
                {
                    return workflow;
                }
            }
            return new List<Workflow>();
        }

        #endregion

        #region Write File

        public string WriteWorkflowDataAsync(List<Workflow> workflows)
        {
            string filePath = _configManager.WorkflowFilePath;
            return WriteWorkflowDataAsync(filePath, workflows);
        }

        public string WriteWorkflowDataAsync(string filePath, List<Workflow> workflows)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            var workflowJsonData = JsonSerializer.Serialize(workflows, options);
            File.WriteAllText(filePath, workflowJsonData);

            return "Success";

            //FileStream createStream = File.Create(filePath);
            //await JsonSerializer.SerializeAsync(createStream, workflows, options);
            //await createStream.DisposeAsync();
        }
        
        #endregion

    }
}
