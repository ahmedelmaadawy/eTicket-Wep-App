using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture Is Required")]
        public string ProfilePictureUrl { get; set; }
        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name Must Be Between 3 and 50 Characters")]
        [Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography Is Required")]
        public string Bio { get; set; }

        //RelationShips
        public List<Movie>? Movies { get; set; }
    }
}
