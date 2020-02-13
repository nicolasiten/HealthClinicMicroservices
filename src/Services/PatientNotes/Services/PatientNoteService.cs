using PatientNotes.Common.Interfaces;
using PatientNotes.Entities;
using PatientNotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Services
{
    public class PatientNoteService : IPatientNoteService
    {
        private readonly INoSqlDbConnector<PatientNote> _dbConnector;

        public PatientNoteService(INoSqlDbConnector<PatientNote> dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public async Task SavePatientNoteAsync(PatientNoteModel patientNote)
        {
            await _dbConnector.InsertAsync(MapPatientNoteModelToPatientNote(patientNote));
        }

        public async Task<IEnumerable<PatientNoteModel>> GetPatientNotesAsync()
        {
            return (await _dbConnector.GetAllAsync()).Select(p => MapPatientNoteToPatientNoteModel(p)).ToList();
        }

        public async Task<IEnumerable<PatientNoteModel>> GetPatientNotesByPatientIdAsync(int patientId)
        {
            return (await _dbConnector.GetAllByFilterAsync(p => p.PatientId == patientId)).Select(p => MapPatientNoteToPatientNoteModel(p)).ToList();
        }

        public async Task UpdatePatientAsync(PatientNoteModel patientNoteModel)
        {
            var patientNoteEntity = MapPatientNoteModelToPatientNote(patientNoteModel);
            await _dbConnector.UpdateAsync(patientNoteEntity);
        }

        public async Task DeletePatientAsync(string patientNoteId)
        {
            await _dbConnector.DeleteAsync(patientNoteId);
        }

        private PatientNote MapPatientNoteModelToPatientNote(PatientNoteModel patientNoteModel)
        {
            return new PatientNote
            {
                Note = patientNoteModel.Note,
                PatientId = patientNoteModel.PatientId
            };
        }

        private PatientNoteModel MapPatientNoteToPatientNoteModel(PatientNote patientNote)
        {
            return new PatientNoteModel
            {
                Id = patientNote.Id.ToString(),
                Note = patientNote.Note,
                PatientId = patientNote.PatientId
            };
        }
    }
}
