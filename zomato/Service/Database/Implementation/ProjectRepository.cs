
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Database;
using System;

public class ProjectRepository : IProjectRepository
{
    private StoreContext this_dataBaseContext;

    public ProjectRepository(StoreContext context){
       this_dataBaseContext = context;
    }

    //method to craete new project.
    public async Task<string> CreateProject(Project project)
    {
          try{
                var create = new Project{projectDescription = project.projectDescription, creatorOfProject = project.creatorOfProject};
                this_dataBaseContext.ProjectDetails.Add(create);
                this_dataBaseContext.SaveChanges();
                return await Task.FromResult("New Project Created Successfuly");
          }
          catch(Exception e){
                return await Task.FromResult(e.Message);
          }
    }

    //method to get the details of aproject.
    public async Task<Project> GetProject(int id)
    {
        var requiredProject = this_dataBaseContext.ProjectDetails.Find(id);
         return await Task.FromResult(requiredProject);
    }

    //method to get details of all the projects.
    public async Task<List<Project> > GetAllProject()
    {
          var allProject = this_dataBaseContext.ProjectDetails.ToList();
          return await Task.FromResult(allProject);
    }

    //method to update a project.
    public async Task<string> UpdateProject(Project project)
    {
          var requiredProject = this_dataBaseContext.ProjectDetails.Find(project.projectId);
           if(requiredProject == null)
            {
                  return await Task.FromResult("Project does not exist!");
            }
            if(project.projectDescription != null)
            {
                  requiredProject.projectDescription = project.projectDescription;
            }
            if(project.creatorOfProject != null)
            {
                  requiredProject.creatorOfProject = project.creatorOfProject;
            }
            this_dataBaseContext.SaveChanges();
          return await Task.FromResult($"Projec with id = {project.projectId} is updated.");
    }

      //method to delete a project.
    public async Task<string > DeleteProject(int id)
    {
          var requiredProject = this_dataBaseContext.ProjectDetails.Find(id);
          if(requiredProject == null)
          {
                return await Task.FromResult("Project does not exist");
          }
          this_dataBaseContext.ProjectDetails.Remove(requiredProject);
          this_dataBaseContext.SaveChanges();
          return await Task.FromResult($"Project with id = {id} is deleted");
    }


      // method to filter projects on the basis of any zero level entity.
    public async Task<List<Project> > FilterProject(string projectDescription, string creatorOfProject)
    {
          var result = this_dataBaseContext.ProjectDetails.Where(i => i.projectDescription == (projectDescription == null ? i.projectDescription : projectDescription) && i.creatorOfProject == (creatorOfProject == null ? i.creatorOfProject : creatorOfProject)).ToList();
          return await Task.FromResult(result);
    }
}