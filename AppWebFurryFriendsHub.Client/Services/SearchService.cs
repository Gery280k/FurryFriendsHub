namespace AppWebFurryFriendsHub.Client.Services
{
    public class SearchService
    {
        public event Func<string, Task> OnSearchChanged;

        public async Task TriggerSearch(string searchQuery)
        {
            if (OnSearchChanged != null)
            {
                await OnSearchChanged.Invoke(searchQuery);
            }
        }
    }
}
