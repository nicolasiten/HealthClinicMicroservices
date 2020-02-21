using DiabetesRisk.Common.Constants;
using DiabetesRisk.Common.Enums;
using DiabetesRisk.Common.Interfaces;
using DiabetesRisk.Models;
using HealthClinic.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Utils
{
    public static  class DiabetesRiskUtils
    {
        public static Sex GetPatientSex(PatientModel patient)
        {
            string sex = patient.Sex.ToLower();

            if (sex == "f")
            {
                return Sex.Female;
            }
            else if (sex == "m")
            {
                return Sex.Male;
            }

            throw new ArgumentException($"Couldn't match sex {sex}");
        }

        public static int GetAge(PatientModel patient, IDateTimeService dateTimeService)
        {
            DateTime today = dateTimeService.DateTimeNow();
            int age = today.Year - patient.Dob.Year;

            // Go back to the year the person was born in case of a leap year
            if (patient.Dob.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        public static int GetNumberOfTriggerTermsInNotes(IEnumerable<string> notes)
        {
            int numberOfTriggerTerms = 0;

            foreach (string term in DiabetesRiskConstants.TriggerTerms)
            {
                foreach (string note in notes)
                {
                    if (note.Contains(term))
                    {
                        numberOfTriggerTerms++;
                        continue;
                    }
                }
            }

            return numberOfTriggerTerms;
        }

        public static string ResolveRiskLevelString(RiskLevel riskLevel)
        {
            switch (riskLevel)
            {
                case RiskLevel.None:
                    return "None";

                case RiskLevel.Borderline:
                    return "Borderline";

                case RiskLevel.InDanger:
                    return "In Danger";

                case RiskLevel.EarlyOnset:
                    return "Early Onset";

                default:
                    throw new NotFoundException("Risklevel", riskLevel.ToString());
            }
        }
    }
}
