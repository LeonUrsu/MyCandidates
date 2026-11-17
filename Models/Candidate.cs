namespace MyCandidates.Models
{
    public class Candidate
    {
        public int CandidateId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Citta { get; set; }
        public string? Disponibilita { get; set; }
        public string? Note { get; set; }
    }
}