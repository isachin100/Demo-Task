using Newtonsoft.Json;
using ParseUsersApplication.Core;
using ParseUsersApplication.Core.Interfaces;
using ParseUsersApplication.Model;
using SuperConvert.Extensions;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ParseUsersApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Welcome to User Parser");
            Console.WriteLine("Enter the path to save: ");

            string? path = Console.ReadLine();
            Console.WriteLine("Enter the format (JSON/CSV) to save:");

            string? format = Console.ReadLine();

            if (!string.IsNullOrEmpty(format) && (format == "JSON" || format == "CSV"))
            {
                if(string.IsNullOrEmpty(path))
                {
                    Console.WriteLine("You have not entered the path");
                    return;
                }
                var users = Task.Run(() => UserParser.ParseUsers()).Result;
                UserParser.Save(format, path, users);
                Console.WriteLine("Total number of users parsed: " + users.Count()+ " and saved to the provided "+ path +" with format: " + format);

            } else 
            { 
                Console.WriteLine("You have entered an incorrect format."); 
                return; 
            }
        }       
    }

    public static class UserParser
    {
        private static readonly Dictionary<int, string> _sources;
        private static readonly UserService _service;
        static UserParser()
        {
            _service = new UserService();
            _sources = new Dictionary<int, string>();

            _sources.Add(1, "https://dummyjson.com/users");
            _sources.Add(2, "https://reqres.in/api/users");
        }

        public static async Task<List<User>>ParseUsers()
        {
            var parsedUsers = new List<User>();
            foreach(var source in _sources)
            {
                var users = await _service.GetUsers(source.Value);
                users?.ForEach(user => { user.Source = source.Key; });

                if(users?.Count > 0)
                parsedUsers.AddRange(users);
            }
            return parsedUsers;
        }

        public static void Save(string format, string path, List<User> users)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string jsonData = JsonConvert.SerializeObject(users.ToArray());
            if (format == "JSON")
            {       
                File.WriteAllText(@""+path+"/users.json", jsonData);
            }
            else
            {
                jsonData.ToCsv(path,"users");
            }
        }
    }
}
