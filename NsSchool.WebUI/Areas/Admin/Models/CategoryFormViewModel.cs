using System.ComponentModel.DataAnnotations;

namespace NsSchool.WebUI.Areas.Admin.Models
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Bu alanı doldurmak zorunludur.")]
        [Display(Name ="Ad")]
        public string Name { get; set; }
    }
}
