namespace BlazorEcommerce.Client.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _http;

        public CategoryService(HttpClient http)
        {
            _http = http;
        }

        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Edition> Editions { get; set ; }

        public async Task GetCategories()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/category");

            if (response != null && response.Data != null)
                Categories = response.Data;
        }

        public async Task GetEditions()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Edition>>>("api/category/getEdition");

            if (response != null && response.Data != null)
                Editions = response.Data;
        }
    }
}
