using CookBook.Core.Models;
using CookBook.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CookBook.Core.Pages.Recipes;

public class CreateModel : PageModel
{
    [BindProperty]
    public CreateRecipeCommand? Input { get; set; }
    private readonly RecipeService _service;

    public CreateModel(RecipeService service)
    {
        _service = service;
    }
        
    public void OnGet()
    {
        Input = new CreateRecipeCommand();
    }

    public async Task<IActionResult> OnPost()
    {
        try
        {
            if (ModelState.IsValid)
            {
                var id = await _service.CreateRecipe(Input);
                return RedirectToPage("View", new { id = id });
            }
        }
        catch (Exception)
        {
            // TODO: Log error
            // Add a model-level error by using an empty string key
            ModelState.AddModelError(
                string.Empty,
                "An error occured saving the recipe"
            );
        }

        return Page();
    }
}