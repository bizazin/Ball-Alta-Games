using Scripts.Core.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Views
{
    public class GameResultView : View
    {
        [SerializeField] private Text resultText;

        public void SetResultText(string text) => resultText.text = text;
    }
}