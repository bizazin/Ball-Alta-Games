using Scripts.Models;

namespace Scripts.Databases
{
    public interface IPlayerSettingsDatabase
    {
        PlayerSettingsVo Settings { get; }
    }
}