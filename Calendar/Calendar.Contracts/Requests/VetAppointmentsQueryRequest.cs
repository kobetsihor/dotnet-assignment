using System;

namespace Calendar.Contracts.Requests
{
    public class VetAppointmentsQueryRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}