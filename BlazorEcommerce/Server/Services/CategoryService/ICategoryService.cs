namespace BlazorEcommerce.Server.Services.CategoryService
{
    public interface ICategoryService
    {

        Task<ServiceResponse<List<Category>>> GetCategories();

        Task<ServiceResponse<List<Edition>>> GetEditions();
    }
}
