using Scripts.Models;

namespace Scripts.Databases
{
    public interface IDoorAnimationSettingsDatabase
    {
        DoorAnimationSettingsVo Settings { get; }
    }
}