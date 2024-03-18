namespace ParseUsersApplication.Core.Interfaces
{
    public interface IApiClient<T> where T : class
    {
        Task<List<T>> GetUsers(string apiUrl);
    }
}
