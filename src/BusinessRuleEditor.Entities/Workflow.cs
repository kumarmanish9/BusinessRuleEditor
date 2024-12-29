using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRuleEditor.Entities
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Workflow>>(myJsonResponse);   
    public class Workflow
    {
        public string WorkflowName { get; set; }
        public List<Rule> Rules { get; set; }
    }
}
