using ProjectWEB.Models;

namespace ProjectWEB.Interfaces
{
    public interface IFoodRepository
    {
        IEnumerable<Food> Foods { get; set; }
        IEnumerable<Food> PrefferedFoods { get; set; }
        Food GetFood(int foodId);
    }
}
