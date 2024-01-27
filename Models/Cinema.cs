using eTickets.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema Logo is Required")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema Name is Required")]
        public string Name { get; set; }
        [Display(Name = "Cinema Description")]
        [Required(ErrorMessage = "Cinema Discription is Required")]
        public string Description { get; set; }
        //RelationShips
        public List<Movie>? Movies { get; set; }
    }
}
