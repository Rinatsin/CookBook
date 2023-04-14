using CookBook.Core.Data;

namespace CookBook.Core.Models;

public class RecipeSummaryViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? TimeToCook { get; set; }
    
    public int NumberOfIngrediens { get; set; }

    public static RecipeSummaryViewModel FromRecipe(Recipe recipe)
    {
        return new RecipeSummaryViewModel
        {
            Id = recipe.RecipeId,
            Name = recipe.Name,
            TimeToCook = $"{recipe.TimeCook.Hours} часов {recipe.TimeCook.Minutes} минут"
        };
    }
}