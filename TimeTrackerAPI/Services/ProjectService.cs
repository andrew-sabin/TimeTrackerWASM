using Mapster;
using TimeTracker.Shared.Entities;
using TimeTracker.Shared.Models.Project;
using TimeTrackerAPI.Repositories;


namespace TimeTrackerAPI.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<List<ProjectResponse>> CreateProject(ProjectCreateRequest request)
        {
            var newEntry = request.Adapt<Project>();
            newEntry.ProjectDetails = request.Adapt<ProjectDetails>();
            var result = await _projectRepo.CreateProject(newEntry);
            return result.Adapt<List<ProjectResponse>>();
        }

        public async Task<List<ProjectResponse>?> DeleteProject(int id)
        {
            var result = await _projectRepo.DeleteProject(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<List<ProjectResponse>>();
        }

        public async Task<List<ProjectResponse>> GetAllProjects()
        {
            var result =await _projectRepo.GetAllProjects();
            return result.Adapt<List<ProjectResponse>>();
        }

        public async Task<ProjectResponse?> GetProjectById(int id)
        {
            var result = await _projectRepo.GetProjectById(id);
            if (result == null)
            {
                return null;
            }
            return result.Adapt<ProjectResponse>();
        }

        public async Task<List<ProjectResponse>?> UpdateProject(int id, ProjectUpdateRequest request)
        {
            try {
                var updatedEntry = request.Adapt<Project>();
                updatedEntry.ProjectDetails = request.Adapt<ProjectDetails>();
                var result = await _projectRepo.UpdateProject(id, updatedEntry);
                return result.Adapt<List<ProjectResponse>>();
            }
            catch (EntityNotFoundException) 
            {
                return null;
            }
            
        }
    }
}
