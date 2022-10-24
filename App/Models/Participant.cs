namespace App.Models
{
    public class Participant
    {
        public Guid Id { get; set; }
        public Etudiant etudiant { get; set; }
        public Cour cour { get; set; }
        public DateTime datep { get; set; }
    }
}
