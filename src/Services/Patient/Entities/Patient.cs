using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Patient.Entities
{
    public class Patient
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Sex { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }
    }
}
