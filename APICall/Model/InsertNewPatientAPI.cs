using System;

namespace APICall.Model
{
    public class InsertNewPatientAPI
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Suburb { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string HospitalName { get; set; }
        public DateTime DateOfTest { get; set; }
        public string HealthCarerName { get; set; }
        public bool Results { get; set; }
        public string Notes { get; set; }
    }
}

