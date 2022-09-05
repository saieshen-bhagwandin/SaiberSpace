namespace BlazorEcommerce.Client.Services.CategoryService
{
    public interface ICategoryService
    {

        public List<Category> Categories { get; set; }

        Task GetCategories();


        public List<Edition> Editions { get; set; }

        Task GetEditions();

    }
}
