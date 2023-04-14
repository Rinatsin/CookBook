using CookBook.Core.Data;

namespace CookBook.Core.Models;

public class CreateRecipeCommand: EditRecipeBase
{
    public IList<CreateIngredientCommand> Ingredients { get; set; } 
        = new List<CreateIngredientCommand>();

    public Recipe ToRecipe()
    {
        return new Recipe
        {
            Name = Name,
            TimeCook = new TimeSpan(TimeToCookHrs, TimeToCookMins, 0),
            Method = Method,
            IsVegetarian = IsVegetarian,
            IsVegan = IsVegan,
            Ingredients = Ingredients?
                .Select(createIngredientCommand => createIngredientCommand.ToIngredient())
                .ToList()!
        };
    }
}