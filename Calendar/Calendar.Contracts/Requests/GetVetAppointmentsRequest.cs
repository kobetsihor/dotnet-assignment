namespace Calendar.Contracts.Requests
{
    public class GetVetAppointmentsRequest
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}