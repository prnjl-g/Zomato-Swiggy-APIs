using System.ComponentModel.DataAnnotations;

namespace Service.Database
{
    public class Comment
    {
        public int commentId{get; set;}
        [Required]
        public int issueId{get; set;}
        public string comment{get; set;}
        public string userName{get; set;}

    }

    public class EditComment
    {
        public int commentId{get; set;}
        public string updatedComment{get; set;}
        public string userName{get; set;}
    }
}