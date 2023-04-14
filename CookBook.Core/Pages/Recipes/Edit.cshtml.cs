using CookBook.Core.Models;
using CookBook.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.Core.Pages.Recipes;

public class EditModel : PageModel
{
    [BindProperty] public UpdateRecipeCommand? Input { get; set; }

    private readonly RecipeService _recipeService;

    public EditModel(RecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Input = await _recipeService.GetRecipeForUpdate(id);
        if (Input is null)
        {
            return NotFound();
        }

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _recipeService.UpdateRecipe(Input);
                return RedirectToPage("View", new { id = Input!.Id });
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError(
                string.Empty,
                "Ошибка при сохранении рецепта"
            );
        }

        return Page();
    }
}