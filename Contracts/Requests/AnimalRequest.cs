namespace Contracts.Requests
{
    public class AnimalRequest
    {
        public string Name { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerName { get; set; } = string.Empty;

        public string OwnerEmail { get; set; } = string.Empty;
    }
}