using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
 
[Table(Name = "Tasks")]
public class task
{
    [Column(IsPrimaryKey = true, IsDbGenerated = true)]
    public int id { get; set; }
    [Column(Name = "Project")]
    public string project { get; set; }
    [Column(Name = "Theme")]
    public string theme { get; set; }
    [Column(Name = "Priority")]
    public int priority { get; set; }
    [Column(Name = "User")]
    public string user { get; set; }
    [Column(Name = "Description")]
    public string description { get; set; }
}

[Table(Name = "Projects")]
public class project
{
    [Column(IsPrimaryKey = true, IsDbGenerated = true)]
    public int id { get; set; }
    [Column(Name = "Project")]
    public string projectName { get; set; }
}

[Table(Name = "Users")]
public class user
{
    [Column(IsPrimaryKey = true, IsDbGenerated = true)]
    public int id { get; set; }
    [Column(Name = "User")]
    public string userName { get; set; }
}

class Program
{
    static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=usersdb;Integrated Security=True";
    static void Main(string[] args) 
    {		
		DataContext db = new DataContext(connectionString);
		Table<Task> tasks = db.GetTable<Task>();
		Table<Project> projects = db.GetTable<Project>();
		Table<User> users = db.GetTable<User>();
        int choose = 1;
		while (choose >= 1 && choose <= 7)
		{
			Console.WriteLine("Enter the number 1-6 or other to exit:");
			Console.WriteLine("1 - to change task in the DB, 2 - to add task to the DB");
			Console.WriteLine("3 - to delete task from the DB, 4 - to view the all tasks in the DB");
			Console.WriteLine("5 - to view all users in the DB, 4 - to view all project in the DB");
			Console.WriteLine("7 - to view all tasks specific user in the DB");
			int choose = Convert.ToInt32(Console.ReadLine());
			if (choose == 1)
			{
				Console.WriteLine("Enter the user ID for change:");
				int chooseID = Convert.ToInt32(Console.ReadLine());
				var task = db.GetTable<task>().Where(task => task.id == chooseID).SingleOrDefault();
				Console.WriteLine("Enter project name:");
				task1.project1 = Console.ReadLine();
				Console.WriteLine("Enter theme name:");
				task1.theme1 = Console.ReadLine();
				Console.WriteLine("Enter priority:");
				task1.priority1 = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Enter user name:");
				task1.user1 = Console.ReadLine();
				Console.WriteLine("Enter description:");
				task1.description1 = Console.ReadLine();
				db.SubmitChanges(); // сохраним изменения
				Console.WriteLine("Object has been changed");
			}
			else if (choose == 2)
			{
				// создаем новую задачу
				Console.WriteLine("Enter project name:");
				string project1 = Console.ReadLine();
		        foreach (var project in projects) // проверяем есть ли такой проект
		        {
		            if (project.projectName==project1)
					{
						int existsPr = 1;
						break;
					}
		        }
				if (existsPr != 1)
				{
					Console.WriteLine("Project has been not found");
				}
				else
				{
					Console.WriteLine("Enter theme name:");
					string theme1 = Console.ReadLine();
					Console.WriteLine("Enter priority:");			
					int priority1 = Convert.ToInt32(Console.ReadLine());
					Console.WriteLine("Enter user name:");			
					string user1 = Console.ReadLine();
			        foreach (var user in users) // проверяем есть ли такой пользователь
			        {
			            if (user.userName==user1)
						{
							int existsUs = 1;
							break;
						}
					}
					if (existsPr != 1)
					{
						Console.WriteLine("User has been not found");
					}
					else
					{
						Console.WriteLine("Enter description:");			
						string description1 = Console.ReadLine();
						task task1 = new task {project = project1, theme = theme1, priority = priority1, user = user1, description = description1};
						// добавляем его в таблицу
						db.GetTable<task>().InsertOnSubmit(task1);
						db.SubmitChanges();
						Console.WriteLine("Object has been added");
					}
				}
			}
			else if (choose == 3)
			{
				DataContext db = new DataContext(connectionString);
				Console.WriteLine("Enter the user ID for delete:");
				int chooseID = Convert.ToInt32(Console.ReadLine());
				var task1 = db.GetTable<task>().Where(task => task.id == chooseID).SingleOrDefault();
				db.GetTable<task>().DeleteOnSubmit(task1);
				db.SubmitChanges();
				Console.WriteLine("Object has been deleted");
			}
			else if (choose == 4)
			{
				// Получаем таблицу задач
		        Table<task> tasks = db.GetTable<task>();  
		        foreach (var task in tasks)
		        {
		            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5}", task.id, task.project, task.theme, task.priority, task.user, task.description);
		        }
			}
			else if (choose == 5)
			{
				// Получаем таблицу пользователей
		        Table<user> users = db.GetTable<user>();  
		        foreach (var user in users)
		        {
		            Console.WriteLine("{0} \t{1}", user.id, user.userName);
		        }
			}
			else if (choose == 6)
			{
				// Получаем таблицу проектов
		        Table<project> projects = db.GetTable<project>();  
		        foreach (var user in users)
		        {
		            Console.WriteLine("{0} \t{1}", project.id, project.projectName);
		        }
			}
			else if (choose == 7)
			{
				Console.WriteLine("Enter the user name:");
				string user2 = Console.ReadLine();
				// Получаем таблицу задач конкретного пользователя
		        Table<task> tasks = db.GetTable<task>();  
		        foreach (var task in tasks)
		        {
					if (task.user==user2)
					{
						Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}", task.id, task.project, task.theme, task.priority, task.description);
					}
		        }
			}
			else
			{
				Console.WriteLine("Press any key to exit");
				Console.Read();
			}
		}
    }
	Environment.Exit(0);
}