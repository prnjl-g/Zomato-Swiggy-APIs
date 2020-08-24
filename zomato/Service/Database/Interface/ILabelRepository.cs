
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;

public interface  ILabelRepository
{
    Task<string> AddLabel(Label label);
    Task<string> DeleteLabel(int labelId, int issueId);
    Task<List<Label>> FilterLabel(int issueId, string label);
}
 