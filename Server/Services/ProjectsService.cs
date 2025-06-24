using Shared.Models;

namespace Server.Services
{
	public class ProjectsService
	{
		private readonly Dictionary<string, Project> _projects = new();
		public Project Create()
		{
			Project project = new Project();

			_projects[project.Token] = project;

			return project;
		}

		public Project? Get(string token)
		{
			return _projects.TryGetValue(token, out var project) ? project : null;
		}

		public bool UpdateCode(string token, string newCode)
		{

			if (!_projects.TryGetValue(token, out var project))
			{
				return false;
			}

			project.Code = newCode;
			project.Version++;
			project.UpdateRequested = true;

			return true;
		}

		public void MarkAsUpdated(string token)
		{
			if (_projects.TryGetValue(token, out var project))
			{
				project.UpdateRequested = false;
			}
		}
	}
}