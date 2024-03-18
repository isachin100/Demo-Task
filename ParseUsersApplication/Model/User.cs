using Newtonsoft.Json;
using System.Text.Json;

namespace ParseUsersApplication.Model
{
    public class User
    {
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonProperty("firstname")]
        private string FirstName2 { set { FirstName = value; } }

        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int Source { get; set; }
    }

    public class UserResult
    {

        [JsonProperty(PropertyName = "data")]
        public List<User>? Users { get; set; }

        [JsonProperty("users")]
        private List<User>? Users2 { set { Users = value; } }
    }
}
