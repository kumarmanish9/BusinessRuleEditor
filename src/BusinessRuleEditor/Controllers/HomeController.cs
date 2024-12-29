using BusinessRuleEditor.Entities;
using BusinessRuleEditor.Extensions;
using BusinessRuleEditor.Models;
using BusinessRuleEditor.Repository;
using BusinessRuleEditor.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusinessRuleEditor.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkflowService _workflowService;
        private readonly IWorkflowWriteService _workflowWriteService;
        private readonly IWorkflowDeleteService _workflowDeleteService;
        private readonly IConfigManagerRepository _configuration;

        public HomeController(
            ILogger<HomeController> logger,
            IWorkflowService workflowService,
            IWorkflowWriteService workflowWriteService,
            IWorkflowDeleteService workflowDeleteService,
            IConfigManagerRepository configuration)
        {
            _logger = logger;
            _workflowService = workflowService;
            _workflowWriteService = workflowWriteService;
            _workflowDeleteService = workflowDeleteService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Index()
        {
            RuleEditorViewModel viewModel = new RuleEditorViewModel();
            viewModel.Categorys = _workflowService.GetWorkflowCategoryAsync().ConvertToSelectListItem();
            var firstCategory = viewModel.Categorys.FirstOrDefault();
            viewModel.CategoryRule = _workflowService.GetWorkflowCategoryRulesAsync(
                firstCategory == null ? "" : firstCategory.Value).ConverToCategoryRule();
            viewModel.CategoryRuleDetail = new();

            return View(viewModel);
        }

        [HttpPost]
        [Consumes("application/json")]
        public PartialViewResult WorkflowCategoryRules([FromBody] RuleDetailsRequest ruleDetailsRequest)
        {
            var viewModel = _workflowService.GetWorkflowCategoryRulesAsync(
                ruleDetailsRequest.WorkflowCategory).ConverToCategoryRule();
            return PartialView("~/Views/Partial/_CategoryRulesPartial.cshtml", viewModel);
        }

        [HttpPost]
        [Consumes("application/json")]
        public PartialViewResult CategoryRuleDetails([FromBody] RuleDetailsRequest ruleDetailsRequest)
        {
            var data = _workflowService?.GetCategoryRuleDetailsAsync(ruleDetailsRequest.WorkflowCategory, ruleDetailsRequest.RuleName);
            var viewModel = new CategoryRuleDetail()
            {
                WorkflowName = data!.WorkflowName,
                RuleName = data.RuleName,
                SuccessEvent = data.SuccessEvent,
                ErrorMessage = data.ErrorMessage,
                ErrorType = _configuration.ErrorType,
                RuleExpressionType = _configuration.RuleExpressionType,
                Expression = data.Expression
            };
            return PartialView("~/Views/Partial/_RuleDetailsViewPartial.cshtml", viewModel);
        }

        [HttpPost]
        [Consumes("application/json")]
        public JsonResult AddUpdateRuleDetails([FromBody] CategoryRuleDetail details)
        {
            if (ModelState.IsValid)
            {
                var ruleDetails = new WorkflowCategoryRuleDetail()
                {
                    WorkflowName = details.WorkflowName,
                    RuleName = details.RuleName,
                    ActualRuleName = details.ActualRuleName,
                    SuccessEvent = details.SuccessEvent,
                    ErrorMessage = details.ErrorMessage,
                    ErrorType = details.ErrorType,
                    RuleExpressionType = details.RuleExpressionType,
                    Expression = details.Expression
                };
                string response = _workflowWriteService.AddUpdateRuleExpressionDetails(ruleDetails);
                return Json(response);
            }
            return Json("Please provide all the required details");
        }

        [HttpPost]
        [Consumes("application/json")]
        public JsonResult AddWorkflowCategory([FromBody] RuleDetailsRequest ruleDetailsRequest)
        {
            string workflowCategory = ruleDetailsRequest.WorkflowCategory!.ToString().ToLowerInvariant();
            if (!string.IsNullOrEmpty(workflowCategory))
            {
                string response = _workflowWriteService.AddWorkflowCategory(workflowCategory);
                return Json(response);
            }
            else
            {
                return Json("Please provide the category name.");
            }
        }

        [HttpPost]
        [Consumes("application/json")]
        public JsonResult AddRuleUnderWorkflowCategory([FromBody] RuleDetailsRequest ruleDetailsRequest)
        {
            if (!string.IsNullOrEmpty(ruleDetailsRequest.WorkflowCategory) &&
                !string.IsNullOrEmpty(ruleDetailsRequest.RuleName))
            {
                string response = _workflowWriteService.AddRuleUnderWorkflowCategory(ruleDetailsRequest.WorkflowCategory, ruleDetailsRequest.RuleName);
                return Json(response);
            }
            else
            {
                return Json("Rule category & rule name is required.");
            }
        }

        [HttpDelete]
        [Consumes("application/json")]
        public JsonResult RemoveRuleUnderWorkflowCategory([FromBody] RuleDetailsRequest ruleDetailsRequest)
        {
            if (!string.IsNullOrEmpty(ruleDetailsRequest.WorkflowCategory) &&
                !string.IsNullOrEmpty(ruleDetailsRequest.RuleName))
            {
                string response = _workflowDeleteService.RemoveRuleUnderWorkflowCategory(ruleDetailsRequest.WorkflowCategory, ruleDetailsRequest.RuleName);
                return Json(response);
            }
            else
            {
                return Json("Rule category & rule name is required.");
            }
        }

        [HttpDelete]
        [Consumes("application/json")]
        public JsonResult RemoveWorkflowCategory([FromBody] RuleDetailsRequest ruleDetailsRequest)
        {
            if (!string.IsNullOrEmpty(ruleDetailsRequest.WorkflowCategory))
            {
                string response = _workflowDeleteService.RemoveWorkflowCategory(ruleDetailsRequest.WorkflowCategory);
                return Json(response);
            }
            else
            {
                return Json("Rule category must be selected.");
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}