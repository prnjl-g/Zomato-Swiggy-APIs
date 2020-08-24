using System.Collections.Generic;

namespace Service.Database
{
    public class Project
    {
        public int projectId{get; set;}
        public string projectDescription{get; set;}
        public string creatorOfProject{get; set;}
        public List<Issue> issueUnderProject{get; set;}
    }
}