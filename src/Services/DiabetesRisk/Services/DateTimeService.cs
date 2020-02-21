using DiabetesRisk.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiabetesRisk.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime DateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
