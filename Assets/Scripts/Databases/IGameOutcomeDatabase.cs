using Scripts.Enums;

namespace Scripts.Databases
{
    public interface IGameOutcomeDatabase
    {
        string GetGameResult(EGameResultType gameResultType);
    }
}