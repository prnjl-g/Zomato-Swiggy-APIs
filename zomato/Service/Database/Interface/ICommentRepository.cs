
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;

public interface  ICommentRepository
{
    Task<string> CommentOnIssue(Comment commentDetails);
    Task<string> UpdateComment(EditComment editComment);
    Task<string> DeleteComment(int commentId, string userName);
    Task<List<Comment>> FilterComment(int issueId, string comment, string userName);
}
