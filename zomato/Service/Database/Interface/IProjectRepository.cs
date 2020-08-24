
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;

public interface IProjectRepository
{
    // Implement all methods required to complete the assignment
    Task<string> CreateProject(Project project);
    Task<Project> GetProject(int id);
    Task<List<Project>> GetAllProject();
    Task<string> DeleteProject(int id);
    Task<string> UpdateProject(Project project);
    Task<List<Project>> FilterProject(string projectDescription, string createrOfProject);
}
