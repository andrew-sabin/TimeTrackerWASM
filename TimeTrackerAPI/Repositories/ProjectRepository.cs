using TimeTracker.Shared.Entities;

namespace TimeTrackerAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;
        private readonly IUserContextService _userContextService;

        public ProjectRepository(DataContext context, IUserContextService userContextService)
        {
            _context = context;
            _userContextService = userContextService;
        }

        public async Task<List<Project>> CreateProject(Project project)
        {
            var user = await _userContextService.GetUserAsync();
            if (user == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            project.Users.Add(user);

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>?> DeleteProject(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return null;
            }


            var dbProject = await _context.Projects
                .FirstOrDefaultAsync(p => p.Id == id && p.Users.Any(u => u.Id == userId));

            if (dbProject == null)
            {
                return null;
            }
            dbProject.IsDeleted = true;
            dbProject.DateDeleted = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>> GetAllProjects()
        {
            var UserId = _userContextService.GetUserId();
            if (UserId == null)
            {
                return new List<Project>();
            }

            return await _context.Projects
                .Where(p => !p.IsDeleted && p.Users.Any(u => u.Id == UserId))
                .Include(p=> p.ProjectDetails)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                return null;
            }

            var project = await _context.Projects
                .Where(p => !p.IsDeleted && p.Id == id && p.Users.Any(u => u.Id == userId))
                .Include(p => p.ProjectDetails)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (project == null)
            {
                return null;
            }

            return project;
        }

        public async Task<List<Project>?> UpdateProject(int id, Project project)
        {
            
            var userId = _userContextService.GetUserId();
            if (userId == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            var dbProject = await _context.Projects
                 .Include(p => p.ProjectDetails)
                 .FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id && p.Users.Any(u => u.Id == userId));


            if (dbProject == null)
            {
                throw new EntityNotFoundException($"Time entry with ID {id} not found.");
            }

            if(project.ProjectDetails != null && dbProject.ProjectDetails != null)
            {
                dbProject.ProjectDetails.Description = project.ProjectDetails.Description;
                dbProject.ProjectDetails.Start = project.ProjectDetails.Start;
                dbProject.ProjectDetails.EndDate = project.ProjectDetails.EndDate;
            }
            else if (project.ProjectDetails != null && dbProject.ProjectDetails == null)
            {
                dbProject.ProjectDetails = new ProjectDetails
                {
                    Description = project.ProjectDetails.Description,
                    Start = project.ProjectDetails.Start,
                    EndDate = project.ProjectDetails.EndDate,
                    Project = project
                };
            }

            dbProject.Name = project.Name;
            dbProject.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }
    }
}
