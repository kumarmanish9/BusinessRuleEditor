using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessRuleEditor.Models
{
    public class RuleEditorViewModel
    {
        public List<SelectListItem>? Categorys { get; set; }
        public List<CategoryRule> CategoryRule { get; set; }
        public CategoryRuleDetail CategoryRuleDetail { get; set; }

    }
}
