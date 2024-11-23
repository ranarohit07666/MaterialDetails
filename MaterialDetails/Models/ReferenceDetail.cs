using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MaterialDetails.Models
{
    public class ReferenceDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string ReferenceNumber { get; set; }
        [Required]
        public DateTime ReferenceDate { get; set; }

        public IList<Material>? Materials { get; set; }
    }
}
