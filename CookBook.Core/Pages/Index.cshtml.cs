using CookBook.Core.Models;
using CookBook.Core.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.Core.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly RecipeService _recipeService;
    public IEnumerable<RecipeSummaryViewModel>? Recipes { get; private set; }

    public IndexModel(ILogger<IndexModel> logger, RecipeService recipeService)
    {
        _logger = logger;
        _recipeService = recipeService;
    }

    public async Task OnGetAsync()
    {
        Recipes = await _recipeService.GetRecipes();
    }
}