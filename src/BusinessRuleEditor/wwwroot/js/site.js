// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Post data to server and receiving response
//===========================================
var CONTENTTYPE = {
    'Appjson': 'application/json; charset=utf-8',
    'Text': 'text'
};

var DATATYPE = { 'HTML': 'html', 'JSON': 'json', 'TEXT': 'text' }
var HTTP = { 'GET': 'GET', 'POST': "POST", 'PUT': 'PUT', 'DELETE': 'DELETE' };
var URL = {
    'AddUpdateRuleDetails': '/Home/AddUpdateRuleDetails',
    'WorkflowCategoryRules': '/Home/WorkflowCategoryRules',
    'CategoryRuleDetails': '/Home/CategoryRuleDetails',
    'AddWorkflowCategory': '/Home/AddWorkflowCategory',
    'AddRuleUnderWorkflowCategory': '/Home/AddRuleUnderWorkflowCategory',
    'RemoveRuleUnderWorkflowCategory': '/Home/RemoveRuleUnderWorkflowCategory',
    'RemoveWorkflowCategory': '/Home/RemoveWorkflowCategory'
};

//Server Calls
//=====================================
function PostDataToServer(Type, URL, Data, ContentType, DataType, Selector) {
    //console.log(Type);
    //console.log(URL);
    //console.log(Data);
    //console.log(ContentType);
    //console.log(DataType);
    //console.log(Selector);

    $.ajax({
        type: Type,
        url: URL,
        data: JSON.stringify(Data),
        contentType: ContentType,
        dataType: DataType,
        async: false,
        success: function (response) {
            console.log('Sanidhya');
            $(Selector).html('').html(response);
        },
        failure: function (response) {
            alert('failure');
            alert(response.responseText);
        },
        error: function (response) {
            alert('error');
            alert(response.responseText);
        }
    });
}

//=====================================

//Helpers
//=====================================
function GetRuleDetails() {
    var CategoryRuleDetail = {
        'WorkflowName': $("#txtWorkflowName").val(),
        'ActualRuleName': $("#txtRuleName").attr("data-oldrulename"),
        'RuleName': $("#txtRuleName").val(),
        'SuccessEvent': $("#txtSuccessEvent").val(),
        'ErrorMessage': $("#txtErrorMessage").val(),
        'ErrorType': $("#txtErrorType").val(),
        'RuleExpressionType': $("#txtRuleExpressionType").val(),
        'Expression': $("#txtExpression").val()
    };
    return CategoryRuleDetail;
}
function CleareAllFields() { $('.resetThis').val(''); }
function RuleFormReset() { $('#formRuleDetails')[0].reset(); }
function SetSelectedColor(obj) {
    $(".rule-selected").removeClass("rule-selected");
    obj.parent('tr').addClass("rule-selected");
}
function SetSelectedRule(ruleName) {
    $('tr td:contains(' + ruleName + ')').each(function () {
        $(this).parent('tr').addClass("rule-selected");
    });
}
function Reload() {
    setTimeout(() => {
        document.location.reload();
    }, 500);
}
//=====================================

//Validators
//=====================================
function IsAllRequiredPresent() {
    var res = false;
    var data = GetRuleDetails();
    if (data.WorkflowName != ''
        && data.RuleName != ''
        && data.ErrorMessage != ''
        && data.ErrorType != ''
        && data.RuleExpressionType != ''
        && data.SuccessEvent != ''
        && data.Expression != ''
    ) {
        res = true;
    }
    return res;
}


//=====================================

//Refresh views/binder/post data
//=====================================
function AddWorkflowCategory() {
    var wfCategory = $("#txtAddWorkflowCategory").val();
    var wfcRuleName = '';
    var _data = { "WorkflowCategory": wfCategory, "RuleName": wfcRuleName };
    if (wfCategory != '') {
        PostDataToServer(HTTP.POST, URL.AddWorkflowCategory,
            _data, CONTENTTYPE.Appjson, DATATYPE.JSON, '');

        Reload();
    }
    else {
        alert('Please provide the category name.');
    }
}

function AddRuleUnderWorkflowCategory() {
    var wfCategory = $('#ddlWorkflowCategory :selected').text();
    var wfcRuleName = $("#txtAddCategoryRule").val();
    var _data = { "WorkflowCategory": wfCategory, "RuleName": wfcRuleName };

    if (wfCategory == '' || wfcRuleName == '') {
        alert("Rule category & rule name is required.");
    }
    else if (wfCategory != '' && wfcRuleName != '') {
        PostDataToServer(HTTP.POST, URL.AddRuleUnderWorkflowCategory,
            _data, CONTENTTYPE.Appjson, DATATYPE.JSON, '');
    }
    BindRules(wfCategory);
    CleareAllFields();
}

function AddUpdateRuleDetails() {
    var data = GetRuleDetails();
    // Check all fields are present or not
    if (IsAllRequiredPresent()) {
        PostDataToServer(HTTP.POST, URL.AddUpdateRuleDetails,
            data, CONTENTTYPE.Appjson, DATATYPE.JSON, '');

        BindRules(data.WorkflowName);

        //SetSelectedRule(data.RuleName);
        CleareAllFields();
    }
    else {
        alert("All fields are mandatory");
    }
}

function BindRules(workflowCategory) {
    var _data = { "WorkflowCategory": workflowCategory, "RuleName": '' };
    PostDataToServer(HTTP.POST, URL.WorkflowCategoryRules,
        _data, CONTENTTYPE.Appjson, DATATYPE.HTML, '#CategoryRulesPartial');
}

function BindRuleExpression(workflowCategory, ruleName) {
    var data = { "WorkflowCategory": workflowCategory, "RuleName": ruleName };
    PostDataToServer(HTTP.POST, URL.CategoryRuleDetails,
        data, CONTENTTYPE.Appjson, DATATYPE.HTML, '#RuleDetailsViewPartial');
}

function DeleteRule() {
    var wfCategory = $("#ddlWorkflowCategory").val();
    var wfcRuleName = $("#txtRuleName").attr("data-oldrulename");
    var _data = { "WorkflowCategory": wfCategory, "RuleName": wfcRuleName };
    if (wfCategory != '' && wfcRuleName != '') {
        PostDataToServer(HTTP.DELETE, URL.RemoveRuleUnderWorkflowCategory,
            _data, CONTENTTYPE.Appjson, DATATYPE.JSON, '');
        BindRules(wfCategory);
        CleareAllFields();
    }
    else {
        alert('Category and rule must be selected!');
    }
}

function DeleteCategory() {
    var wfCategory = $("#ddlWorkflowCategory").val();
    var wfcRuleName = '';
    var _data = { "WorkflowCategory": wfCategory, "RuleName": wfcRuleName };
    if (wfCategory != '') {
        PostDataToServer(HTTP.DELETE, URL.RemoveWorkflowCategory,
            _data, CONTENTTYPE.Appjson, DATATYPE.JSON, '');

        Reload();
    }
    else {
        alert('Rule category must be selected.');
    }
}

//=====================================


//Document Ready
//=====================================
$(function () {
    $("#ddlWorkflowCategory").change(function () {
        CleareAllFields();
        BindRules($(this).val());
    });

    $(document).on("click", "#CategoryRules td", function () {
        SetSelectedColor($(this));
        var ruleName = $(this).text();
        var workflowCategory = $("#ddlWorkflowCategory").val();
        BindRuleExpression(workflowCategory, ruleName);
    });

    $(document).on("click", "#btnAddCategory", function () {
        AddWorkflowCategory();
    });
    $(document).on("click", "#btnDeleteCategory", function () {
        DeleteCategory();
    });
    $(document).on("click", "#btnAddRule", function () {
        AddRuleUnderWorkflowCategory();
    });

    $(document).on("click", "#btnUpdateRule", function () {
        AddUpdateRuleDetails();
    });

    $(document).on("click", "#btnPreviewRule", function () {
        console.log('peview');

    });

    $(document).on("click", "#btnReseteRule", function () {
        //RuleFormReset();
        CleareAllFields();
    });

    $(document).on("click", "#btnDeleteRule", function () {
        DeleteRule();
    });

});

//=====================================