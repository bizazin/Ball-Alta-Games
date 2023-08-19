using Scripts.Enums;

namespace Scripts.Controllers
{
    public interface IGameResultController
    {
        void SetResult(EGameResultType gameResultType);
    }
}