using System;

namespace APICall.Model
{
    public class UpdatePatientNextOfKinAPI
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
    }
}
