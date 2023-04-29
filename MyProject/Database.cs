using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject;
using MyProject.Controllers;
using System.Net;

namespace MyProject
{
	
       

        public class Project
		{
			public int Id { get; set; }
			public string Login { get; set; }
			public string Title { get; private set; }
			public string Content { get; private set; }
			public Project(string login,string title,string content)
			{
				Login = login;
				Title = title;
				Content = content;
			}
			public void SetData(string title,string content)
			{
				Title = title;
				Content = content;
			}
		}
		public class User
		{
			public int Id { get; set; }
			public string Login { get; set; }
			public string Password { get; set; }

			public User(string login, string password) {
				Login = login;
				Password = password;
			
			}
}
	}


public class Database : DbContext
{
    
    private const string DatabaseName = "projects";
	private const string DatabaseFilePath = $"{DatabaseName}.db";
	public static readonly Database Instance = new Database();
	public List<Project> Projects => ProjectsTable.ToList();
	public List<User> Users => UsersTable.ToList();
	private DbSet<Project> ProjectsTable => Set<Project>();
	private DbSet<User> UsersTable => Set<User>();
	public Database()
	{
		File.Delete(DatabaseFilePath);
		Database.EnsureCreated();
	}
	public void AddIn(Project newProject)
	{
		var projects = ProjectsTable.ToList();
		var doesExist = projects.Any(projects => projects.Title == newProject.Title);
		if (doesExist)
			return;
		Projects.Add(newProject);
		SaveChanges();
	}
	public void AddUsers(User user)
	{
		UsersTable.Add(user);
	   SaveChanges();
	}

	
	public void CheckUser(User user)
	{
		if (user == null)
			return;
	}
	
	public void RemoveTable(string title)
	{
		var projectIndex = Projects.FindIndex(project => project.Title == title);
		if (projectIndex < 0)
			return;
		var project = Projects[projectIndex];
		ProjectsTable.Remove(project);
	}
	public void UpdateTable(int id,string title,string content)
	{
		var projects = Projects;
		var projectIndex = projects.FindIndex(project => project.Id == id);
		if (projectIndex < 0)
			return;
		var project = projects[projectIndex];
		project.SetData(title, content);
		ProjectsTable.Update(project);
		SaveChanges();


	}
    public bool DoesExistUser(string login)
    {
        return Users.Any(l => l.Login== login);
    }

    public bool IsCorrectUserData(string login, string password)
    {
        var user = Users.Find(l => l.Login == login);

        return user.Password == password;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite($"Data Source={DatabaseName}.db");
    }

    internal bool DoesExistLogin(string login)
    {
        throw new NotImplementedException();
    }
}
