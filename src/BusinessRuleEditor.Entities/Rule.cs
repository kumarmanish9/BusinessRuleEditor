﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRuleEditor.Entities
{
    public class Rule
    {
        public string RuleName { get; set; }
        public string SuccessEvent { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
        public string RuleExpressionType { get; set; }
        public string Expression { get; set; }
    }
}
