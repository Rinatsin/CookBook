using CookBook.Core.Data;
using CookBook.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CookBook.Core.Services;

public class RecipeService
{
    private readonly AppDbContext _dbContext;
    private ILogger _logger;

    public RecipeService(AppDbContext dbContext, ILoggerFactory factory)
    {
        _dbContext = dbContext;
        _logger = factory.CreateLogger<RecipeService>();
    }

    public async Task<List<RecipeSummaryViewModel>?> GetRecipes()
    {
        return await _dbContext.Recipes
            .Where(recipe => !recipe.IsDeleted)
            .Select(x => new RecipeSummaryViewModel
            {
                Id = x.RecipeId,
                Name = x.Name,
                TimeToCook = $"{x.TimeCook.Hours}hrs {x.TimeCook.Minutes}mins",
            })
            .ToListAsync();
    }
    
    public async Task<RecipeDetailViewModel?> GetRecipeDetail(int id)
    {
        return await _dbContext.Recipes
            .Where(x => x.RecipeId == id)
            .Where(x => !x.IsDeleted)
            .Select(x => new RecipeDetailViewModel
            {
                Id = x.RecipeId,
                Name = x.Name,
                Method = x.Method,
                Ingredients = (x.Ingredients!)
                    .Select(item => new RecipeDetailViewModel.Item
                    {
                        Name = item.Name,
                        Quantity = $"{item.Quantity} {item.Unit}"
                    })
            })
            .SingleOrDefaultAsync();
    }
    
    public async Task<int> CreateRecipe(CreateRecipeCommand? cmd)
    {
        var recipe = cmd!.ToRecipe();
        _dbContext.Add(recipe);
        await _dbContext.SaveChangesAsync();
        return recipe.RecipeId;
    }

    public async Task UpdateRecipe(UpdateRecipeCommand? cmd)
    {
        var recipe = await _dbContext.Recipes.FindAsync(cmd!.Id);
        if (recipe == null)
        {
            throw new Exception("Unable to find the recipe");
        }

        if (recipe.IsDeleted)
        {
            throw new Exception("Unable to update a deleted recipe");
        }

        cmd.UpdateRecipe(recipe);
        await _dbContext.SaveChangesAsync();
        }

    public async Task<UpdateRecipeCommand?> GetRecipeForUpdate(int recipeId)
    {
        return (await _dbContext.Recipes
            .Where(x => x.RecipeId == recipeId)
            .Where(x => !x.IsDeleted)
            .Select(x => new UpdateRecipeCommand
            {
                Name = x.Name,
                Method = x.Method,
                TimeToCookHrs = x.TimeCook.Hours,
                TimeToCookMins = x.TimeCook.Minutes,
                IsVegan = x.IsVegan,
                IsVegetarian = x.IsVegetarian
            })
            .SingleOrDefaultAsync());
    }

    public async Task DeleteRecipe(int recipeId)
    {
        var recipe = await _dbContext.Recipes.FindAsync(recipeId);
        if (recipe is null)
        {
            throw new Exception("Unable to find the recipe");
        }

        recipe.IsDeleted = true;
        await _dbContext.SaveChangesAsync();
    }
}