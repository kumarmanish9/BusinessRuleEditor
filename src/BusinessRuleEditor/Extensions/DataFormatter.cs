using BusinessRuleEditor.Entities;
using BusinessRuleEditor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BusinessRuleEditor.Extensions
{
    public static class DataFormatter
    {
        public static List<SelectListItem> ConvertToSelectListItem(this List<WorkflowCategory> data)
        {
            List<SelectListItem> workflow = new();
            if (data != null)
            {
                workflow = data.Select(x => new SelectListItem
                {
                    Text = x.Workflow,
                    Value = x.Workflow
                }).ToList();
            }
            return workflow;
        }
        public static List<CategoryRule> ConverToCategoryRule(this List<WorkflowCategoryRule> data)
        {
            List<CategoryRule> categoryRule = new();
            if (data != null)
            {
                categoryRule = data.Select(x => new CategoryRule
                {
                    Rule = x.Rule
                }).ToList();
            }
            return categoryRule;
        }
    }
}
