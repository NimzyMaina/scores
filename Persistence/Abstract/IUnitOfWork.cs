using System.Threading.Tasks;

namespace Scores.Persistence.Abstract
{
    public interface IUnitOfWork
    {
         Task Complete();
    }
}