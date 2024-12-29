using System.ComponentModel.DataAnnotations;

namespace BusinessRuleEditor.Models
{
    public class CategoryRuleDetail
    {
        [Required(ErrorMessage = "Category name is required")]
        public string WorkflowName { get; set; }

        public string ActualRuleName { get; set; }
        [Required(ErrorMessage = "Rule name is required")]
        public string RuleName { get; set; }
       

        public string SuccessEvent { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }

        [Required(ErrorMessage = "Rule expression type is required")]
        public string RuleExpressionType { get; set; }

        [Required(ErrorMessage = "Expression is required")]
        public string Expression { get; set; }
    }
}
