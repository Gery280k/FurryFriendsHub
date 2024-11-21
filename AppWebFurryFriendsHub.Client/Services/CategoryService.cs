namespace AppWebFurryFriendsHub.Client.Services
{
    public class CategoryService
    {
        public event Action<int, string> OnCategoryChanged;

        public void SetCategory(int categoryId, string categoryName)
        {
            OnCategoryChanged?.Invoke(categoryId, categoryName);
        }
    }
}
