namespace App.Models
{
    public class Etudiant
    {
        public Guid Id { get; set; }
        public String Nom { get; set; }
        public ICollection<Cour> cours { get; set; }
    }
}
