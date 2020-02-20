using DiabetesRisk.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Models
{
    public class RiskAssessmentModel
    {
        public RiskLevel RiskLevel { get; set; }

        public string Assessment { get; set; }
    }
}
