using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TresBrujas.Models.ViewModels
{
    public class SpellFormViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
       
        [Display(Name = "Spell Type")]
        [Required]
        public int SpellTypeId { get; set; }

        [NotMapped]
        public List<SelectListItem> SpellTypeOptions { get; set; }


    }
}
