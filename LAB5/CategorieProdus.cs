
using System.ComponentModel.DataAnnotations;

namespace LAB5
{
    public class CategorieProdus
    {
        public int Id { get; set; }

        [Required]
        public string Denumire { get; set; } = string.Empty;
    }
}
