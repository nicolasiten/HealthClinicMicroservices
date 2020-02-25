using DiabetesRisk.Common.Enums;
using DiabetesRisk.Common.Interfaces;
using DiabetesRisk.Models;
using DiabetesRisk.Utils;
using HealthClinic.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Services
{
    public class DiabetesRiskService : IDiabetesRiskService
    {
        private readonly IPatientService _patientService;
        private readonly IPatientNoteService _patientNoteService;
        private readonly IDateTimeService _dateTimeService;

        public DiabetesRiskService(IPatientService patientService, IPatientNoteService patientNoteService, IDateTimeService dateTimeService)
        {
            _patientService = patientService;
            _patientNoteService = patientNoteService;
            _dateTimeService = dateTimeService;
        }

        public async Task<RiskAssessmentModel> DetermineRiskForPatientAsync(int patientId)
        {
            var patient = await _patientService.GetPatientAsync(patientId);

            if (patient == null)
            {
                throw new NotFoundException("Patient", patientId);
            }

            RiskLevel riskLevel;

            var patientNotes = await _patientNoteService.GetNotesByPatientIdAsync(patientId);

            if (CheckRiskLevelBorderline(patient, patientNotes))
            {
                riskLevel = RiskLevel.Borderline;
            }
            else if (CheckRiskLevelInDanger(patient, patientNotes))
            {
                riskLevel = RiskLevel.InDanger;
            }
            else if (CheckRiskLevelEarlyOnset(patient, patientNotes))
            {
                riskLevel = RiskLevel.EarlyOnset;
            }
            else
            {
                riskLevel = RiskLevel.None;
            }

            return new RiskAssessmentModel
            {
                RiskLevel = riskLevel,
                Assessment = $"Patient: {patient.Family} {patient.Given} (age {DiabetesRiskUtils.GetAge(patient, _dateTimeService)}) diabetes assessment is: {DiabetesRiskUtils.ResolveRiskLevelString(riskLevel)}"
            };
        }

        private bool CheckRiskLevelBorderline(PatientModel patient, IEnumerable<string> patientNotes)
        {
            if (DiabetesRiskUtils.GetAge(patient, _dateTimeService) >= 30
                && DiabetesRiskUtils.GetNumberOfTriggerTermsInNotes(patientNotes) == 2)
            {
                return true;
            }

            return false;
        }

        private bool CheckRiskLevelInDanger(PatientModel patient, IEnumerable<string> patientNotes)
        {
            Sex sex = DiabetesRiskUtils.GetPatientSex(patient);
            int age = DiabetesRiskUtils.GetAge(patient, _dateTimeService);
            int triggerTerms = DiabetesRiskUtils.GetNumberOfTriggerTermsInNotes(patientNotes);

            if ((age < 30
                && (sex == Sex.Female && triggerTerms == 4 || sex == Sex.Male && triggerTerms == 3))
                || age >= 30 && triggerTerms == 6)
            {
                return true;
            }

            return false;
        }

        private bool CheckRiskLevelEarlyOnset(PatientModel patient, IEnumerable<string> patientNotes)
        {
            Sex sex = DiabetesRiskUtils.GetPatientSex(patient);
            int age = DiabetesRiskUtils.GetAge(patient, _dateTimeService);
            int triggerTerms = DiabetesRiskUtils.GetNumberOfTriggerTermsInNotes(patientNotes);

            if ((age < 30 && (sex == Sex.Male && triggerTerms == 5 || sex == Sex.Female && triggerTerms == 7))
                || triggerTerms >= 8)
            {
                return true;
            }

            return false;
        }
    }
}
