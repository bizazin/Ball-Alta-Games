using Scripts.Core.Abstracts;
using Scripts.Databases;
using Scripts.Enums;
using Scripts.Views;

namespace Scripts.Controllers.Impls
{
    public class GameResultController : Controller<GameResultView>, IGameResultController
    {
        private readonly IGameOutcomeDatabase _gameOutcomeDatabase;

        public GameResultController
        (
            IGameOutcomeDatabase gameOutcomeDatabase
        )
        {
            _gameOutcomeDatabase = gameOutcomeDatabase;
        }
        
        public void SetResult(EGameResultType gameResultType)
        {
            var resultText = _gameOutcomeDatabase.GetGameResult(gameResultType);
            
            View.gameObject.SetActive(true);
            View.SetResultText(resultText);
        }
    }
}