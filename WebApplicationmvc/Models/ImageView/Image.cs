using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebApplicationmvc.Models.ImageView
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public string Path { get; set; }
        [NotMapped]
        [Display(Name = "Choose Image")]
        public IFormFile ImagePath { get; set; }
    }
}
