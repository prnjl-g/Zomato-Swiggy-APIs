using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Database;

public class CommentRepository : ICommentRepository
{
    private StoreContext this_dataBaseContext;

    public CommentRepository(StoreContext context){
        this_dataBaseContext = context;
    }
    

    //method to comment on a issue.
    public async Task<string> CommentOnIssue(Comment commentDetails)
    {
        var issue = this_dataBaseContext.IssueList.Find(commentDetails.issueId);
        if(issue == null)
        {
            return await Task.FromResult("Issue with given id does not exist!");
        }
        var user = this_dataBaseContext.Users.Where(i => i.UserName == commentDetails.userName);
        if(user == null)
        {
            return await Task.FromResult("User not registered");
        }
        this_dataBaseContext.CommentsOnIssues.Add(commentDetails);
        this_dataBaseContext.SaveChanges();
        return await Task.FromResult($"You have commented on issue with id = {commentDetails.issueId} and your comment is {commentDetails.comment}.");
    }

    //method to update a comment.
    public async Task<string> UpdateComment(EditComment editComment)
    {
        var comment = this_dataBaseContext.CommentsOnIssues.Where(i => i.commentId == editComment.commentId).ToList();
        if(comment.Count == 0)
        {
            return await Task.FromResult("Invalid Comment Id");
        }
        var user = this_dataBaseContext.Users.Where(i => i.UserName == editComment.userName).ToList();
        if(user.Count == 0)
        {
            return await Task.FromResult("Invalid UserName");
        }
        if(comment[0].userName != editComment.userName)
        {
            return await Task.FromResult("You can not edit this comment");
        }
        comment[0].comment = editComment.updatedComment;
        this_dataBaseContext.SaveChanges();
        return await Task.FromResult($"Your comment on issue with id = {comment[0].issueId} is updated from {comment[0].comment} to {editComment.updatedComment}.");
    }

    //Method to delete a comment.
    public async Task<string> DeleteComment(int commentId, string userName)
    {
        var comment = this_dataBaseContext.CommentsOnIssues.Where(i => i.commentId == commentId).ToList();
        if(comment.Count == 0)
        {
            return await Task.FromResult("Invalid Comment Id");
        }
        var user = this_dataBaseContext.Users.Where(i => i.UserName == userName).ToList();
        if(user.Count == 0)
        {
            return await Task.FromResult("Invalid UserName");
        }
        if(comment[0].userName != userName)
        {
            return await Task.FromResult("You can not delete this comment");
        }
        this_dataBaseContext.CommentsOnIssues.Remove(comment[0]);
        this_dataBaseContext.SaveChanges();
        return await Task.FromResult($"Your comment on issue with id = {comment[0].issueId} is Deleted.");
    }

    //method to filter a comment.
    public async Task<List<Comment> > FilterComment(int issueId, string comment, string userName)
    {
        var result = this_dataBaseContext.CommentsOnIssues.Where(i => i.issueId == (issueId == 0 ? i.issueId : issueId) && i.comment == (comment == null ? i.comment : comment) && i.userName == (userName == null ? i.userName : userName)).ToList();
        return await Task.FromResult( result);
    }
      
}