using HealthClinic.Common.Exceptions;
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
        private readonly IPatientService _patientService;

        public PatientNoteService(INoSqlDbConnector<PatientNote> dbConnector, IPatientService patientService)
        {
            _dbConnector = dbConnector;
            _patientService = patientService;
        }

        public async Task SavePatientNoteAsync(PatientNoteModel patientNote)
        {
            if (!await _patientService.PatientExists(patientNote.PatientId))
            {
                throw new NotFoundException("Patient", patientNote.PatientId);
            }

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

        public async Task<PatientNoteModel> GetPatientNoteByIdAsync(string id)
        {
            var patientNote = (await _dbConnector.GetAllByFilterAsync(p => p.Id == new MongoDB.Bson.ObjectId(id))).SingleOrDefault();

            if (patientNote == null)
            {
                throw new NotFoundException("PatientNote", id);
            }

            return MapPatientNoteToPatientNoteModel(patientNote);
        }

        public async Task UpdatePatientAsync(PatientNoteModel patientNoteModel)
        {
            if (!await _patientService.PatientExists(patientNoteModel.PatientId))
            {
                throw new NotFoundException("Patient", patientNoteModel.PatientId);
            }

            var patientNoteEntity = MapPatientNoteModelToPatientNote(patientNoteModel);
            await _dbConnector.UpdateAsync(patientNoteEntity);
        }

        public async Task DeletePatientAsync(string patientNoteId)
        {
            await _dbConnector.DeleteAsync(patientNoteId);
        }

        private PatientNote MapPatientNoteModelToPatientNote(PatientNoteModel patientNoteModel)
        {
            var patientNote = new PatientNote
            {
                Note = patientNoteModel.Note,
                PatientId = patientNoteModel.PatientId,
                Created = patientNoteModel.Created,
                Edited = patientNoteModel.Edited
            };

            if (!string.IsNullOrEmpty(patientNoteModel.Id))
            {
                patientNote.Id = new MongoDB.Bson.ObjectId(patientNoteModel.Id ?? string.Empty);
            }

            return patientNote;
        }

        private PatientNoteModel MapPatientNoteToPatientNoteModel(PatientNote patientNote)
        {
            return new PatientNoteModel
            {
                Id = patientNote.Id.ToString(),
                Note = patientNote.Note,
                PatientId = patientNote.PatientId,
                Created = patientNote.Created,
                Edited = patientNote.Edited
            };
        }
    }
}
