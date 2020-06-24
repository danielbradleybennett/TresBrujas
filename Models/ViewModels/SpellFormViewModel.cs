using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TresBrujas.Models.ViewModels
{
    public class SpellFormViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        [Display(Name = "Spell Type")]
        [Required]
        public int SpellTypeId { get; set; }
        public List<SelectListItem> SpellTypeOptions { get; set; }


    }
}
