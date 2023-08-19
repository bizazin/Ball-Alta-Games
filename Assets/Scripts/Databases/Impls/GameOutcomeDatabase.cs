using System;
using System.Collections.Generic;
using Scripts.Enums;
using Scripts.Models;
using UnityEngine;

namespace Scripts.Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/GameOutcomeDatabase", fileName = "GameOutcomeDatabase")]
    public class GameOutcomeDatabase : ScriptableObject, IGameOutcomeDatabase
    {
        [SerializeField] private GameResultVo[] gameResults;
        private Dictionary<EGameResultType, string> _gameResultsDictionary;
        
        private void OnEnable()
        {
            _gameResultsDictionary = new Dictionary<EGameResultType, string>();

            foreach (var gameResult in gameResults) 
                _gameResultsDictionary.Add(gameResult.Type, gameResult.Text);
        }
        
        public string GetGameResult(EGameResultType gameResultType)
        {
            try
            {
                return _gameResultsDictionary[gameResultType];
            }
            catch (Exception e)
            {
                throw new Exception(
                    $"[{nameof(GameOutcomeDatabase)}] Game result" +
                    $" with type {gameResultType.ToString()} was not present in the dictionary. {e.StackTrace}");
            }
        }
    }
}