using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.Project;

namespace TimeTrackerAPI.Services
{
    public interface IProjectService
    {
        Task<ProjectResponse?> GetProjectById(int id);
        Task<List<ProjectResponse>> GetAllProjects();
        Task<List<ProjectResponse>> CreateProject(ProjectCreateRequest project);
        Task<List<ProjectResponse>?> UpdateProject(int id, ProjectUpdateRequest project);
        Task<List<ProjectResponse>?> DeleteProject(int id);
    }
}
