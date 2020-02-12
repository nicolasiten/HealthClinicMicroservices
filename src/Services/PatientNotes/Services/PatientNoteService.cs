using PatientNotes.Common.Interfaces;
using PatientNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientNotes.Services
{
    public class PatientNoteService
    {
        private readonly INoSqlDbConnector<PatientNote> _dbConnector;

        public PatientNoteService(INoSqlDbConnector<PatientNote> dbConnector)
        {
            _dbConnector = dbConnector;
        }
    }
}
