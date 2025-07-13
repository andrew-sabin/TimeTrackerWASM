﻿using TimeTracker.Shared.Models.Project;

namespace TimeTracker.Client.Services
{
    public interface IProjectService
    {
        event Action? OnChange;
        public List<ProjectResponse> Projects { get; set; }
        Task LoadAllProjects();
        Task<ProjectResponse?> GetProjectById(int id);
        Task CreateProject(ProjectRequest project);
        Task UpdateProject(int id, ProjectRequest project);
        Task DeleteProject(int id);
    }
}
