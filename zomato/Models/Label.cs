using System.ComponentModel.DataAnnotations;

namespace Service.Database
{
    public class Label
    {
        public int labelId{get; set;}
        public string label{get; set;}
        [Required]
        public int issueId{get; set;}
    }
}
