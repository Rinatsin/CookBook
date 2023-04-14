using CookBook.Core.Models;
using CookBook.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.Core.Pages.Recipes;

public class ViewModel : PageModel
{
    public RecipeDetailViewModel? Recipe { get; set; }
    private readonly RecipeService _recipeService;

    public ViewModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Recipe = await _recipeService.GetRecipeDetail(id);
        if (Recipe is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        await _recipeService.DeleteRecipe(id);
        return RedirectToPage("/Index");
    }
}