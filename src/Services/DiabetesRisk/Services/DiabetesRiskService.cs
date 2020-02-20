using DiabetesRisk.Common.Constants;
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
    public class DiabetesRiskService
    {
        private readonly IPatientService _patientService;
        private readonly IPatientNoteService _patientNoteService;

        public DiabetesRiskService(IPatientService patientService, IPatientNoteService patientNoteService)
        {
            _patientService = patientService;
            _patientNoteService = patientNoteService;
        }

        public async Task<RiskLevel> DetermineRiskForPatient(int patientId)
        {
            var patient = await _patientService.GetPatientAsync(patientId);

            if (patient == null)
            {
                throw new NotFoundException("Patient", patientId);
            }

            var patientNotes = await _patientNoteService.GetNotesByPatientIdAsync(patientId);

            if (CheckRiskLevelNone(patient, patientNotes))
            {
                return RiskLevel.None;
            }
            else if (CheckRiskLevelBorderline(patient, patientNotes))
            {
                return RiskLevel.Borderline;
            }
            else if (CheckRiskLevelInDanger(patient, patientNotes))
            {
                return RiskLevel.InDanger;
            }
            else if (CheckRiskLevelEarlyOnset(patient, patientNotes))
            {
                return RiskLevel.EarlyOnset;
            }

            throw new NotFoundException("RiskLevel", patientId);
        }

        private bool CheckRiskLevelNone(PatientModel patient, IEnumerable<string> patientNotes)
        {
            if (DiabetesRiskUtils.GetNumberOfTriggerTermsInNotes(patientNotes) == 0)
            {
                return true;
            }

            return false;
        }

        private bool CheckRiskLevelBorderline(PatientModel patient, IEnumerable<string> patientNotes)
        {
            if (DiabetesRiskUtils.GetAge(patient) >= 30 
                && DiabetesRiskUtils.GetNumberOfTriggerTermsInNotes(patientNotes) == 2)
            {
                return true;
            }

            return false;
        }

        private bool CheckRiskLevelInDanger(PatientModel patient, IEnumerable<string> patientNotes)
        {
            return false;
        }

        private bool CheckRiskLevelEarlyOnset(PatientModel patient, IEnumerable<string> patientNotes)
        {
            return false;
        }
    }
}
