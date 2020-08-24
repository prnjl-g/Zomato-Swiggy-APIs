using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.Database;
 
public class LabelRepository : ILabelRepository
{
      private StoreContext this_dataBaseContext;

      public LabelRepository(StoreContext context){
            this_dataBaseContext = context;
      }

    //method to add new label.
      public async Task<string> AddLabel(Label label)
      {
          var issue = this_dataBaseContext.IssueList.Find(label.issueId);
          if(issue == null)
          {
              return await Task.FromResult("Invalid issue Id");
          }
          var isLabelExist = this_dataBaseContext.Labels.Where(i => i.issueId == label.issueId && i.label == label.label).ToList();
          if(isLabelExist.Count != 0)
          {
              return await Task.FromResult("Issue is already tagged with the given label");
          }
          this_dataBaseContext.Labels.Add(label);
          this_dataBaseContext.SaveChanges();
          return await Task.FromResult($"New label is added to the issue with id  = {label.issueId}");
      }

      //method to delete a label.
      public async Task<string> DeleteLabel(int labelId, int issueId)
      {
          var issue = this_dataBaseContext.IssueList.Find(issueId);
          if(issue == null)
          {
              return await Task.FromResult("Invalid issue Id");
          }
          var label = this_dataBaseContext.Labels.Find(labelId);
          if(label == null)
          {
              return await Task.FromResult("Invalid label Id");
          }
          var isLabelExist = this_dataBaseContext.Labels.Where(i => i.labelId == labelId && i.issueId == issueId).ToList();
          if(isLabelExist.Count == 0)
          {
              return await Task.FromResult("Issue is not tagged with the given label");
          }
          this_dataBaseContext.Labels.Remove(label);
          this_dataBaseContext.SaveChanges();
          return await Task.FromResult($"Label with id = {labelId} is removed from issue with id = {issueId}");
      }

    //method to filter labels on the basis of any zero level entity.
      public async Task<List<Label>> FilterLabel(int issueId, string label)
      {
          var result = this_dataBaseContext.Labels.Where(i => i.issueId == (issueId == 0 ? i.issueId : issueId) && i.label == (label == null ? i.label : label)).ToList();
          return await Task.FromResult(result);
      }
      
}