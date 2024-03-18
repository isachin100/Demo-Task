using ParseUsersApplication.Core.Interfaces;
using ParseUsersApplication.Model;
using System.IO;

namespace ParseUsersApplication.Core
{
    public class UserService : IApiClient<User>
    {
        private readonly HttpClient _client;

        public UserService()
        {
            _client = new HttpClient();
        }

        public async Task<List<User>> GetUsers(string apiUrl)
        {
            var apiResult = new UserResult();
            HttpResponseMessage response = await _client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                apiResult = await response.Content.ReadAsAsync<UserResult>();
            }
            return apiResult?.Users;
        }
    }
}
