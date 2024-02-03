using eTickets.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace eTickets.Data.ViewModels
{
    public class NewMovieVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Movie Image Is Required")]
        [Display(Name = "Movie Poster Url")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Description Is Required")]
        [Display(Name = "Movie Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Movie Price Is Required")]
        [Display(Name = "Movie Price")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Movie StartDate Is Required")]
        [Display(Name = "Movie Start Date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Movie EndDate Is Required")]
        [Display(Name = "Movie End Date")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Movie Category Is Required")]
        [Display(Name = "Movie Category")]
        public MovieCategory MovieCategory { get; set; }

        [Required(ErrorMessage = "Movie Actor(s) Is Required")]
        [Display(Name = "Select Actor(s)")]
        public List<int> ActorIds { get; set; }
        [Required(ErrorMessage = "Movie Cinema Is Required")]
        [Display(Name = "Select a Cinema")]
        public int CinemaId { get; set; }
        [Required(ErrorMessage = "Movie Producer Is Required")]
        [Display(Name = "Select a Producer")]
        public int ProducerId { get; set; }


    }
}
