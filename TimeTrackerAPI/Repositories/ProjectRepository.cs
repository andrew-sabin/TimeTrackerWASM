namespace TimeTrackerAPI.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>?> DeleteProject(int id)
        {
            var dbProject = await _context.Projects.FindAsync(id);

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
            return await _context.Projects
                .Where(te => !te.IsDeleted)
                .Include(te=> te.ProjectDetails)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectById(int id)
        {
            var project = await _context.Projects
                .Where(te => !te.IsDeleted)
                .Include(te => te.ProjectDetails)
                .FirstOrDefaultAsync(te => te.Id == id);
            if (project == null)
            {
                return null;
            }

            return project;
        }

        public async Task<List<Project>?> UpdateProject(int id, Project project)
        {
            var dbProject = await _context.Projects.FindAsync(id);

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
