using ProjectWEB.Models;

namespace ProjectWEB.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
