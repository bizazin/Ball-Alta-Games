using System;
using Scripts.Enums;

namespace Scripts.Models
{
    [Serializable]
    public class GameResultVo
    {
        public string Text;
        public EGameResultType Type;
    }
}