using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// Основной скрипт, контролирующий игру
    /// </summary>
    public class GameController : MonoBehaviour
    {
        #region Variables
        #region Static
        #region Events
        /// <summary>
        /// Событие изменения состояния игры
        /// </summary>
        public static Action<GameState> ChangeGameState;
        #endregion
        public static GameDifficulty GameDifficulty
        {
            set
            {
                switch (value)
                {
                    case GameDifficulty.Easy:
                        PlayerSpeed = 4f;
                        ObstaclesDistance = 12f;
                        return;
                    case GameDifficulty.Normal:
                        PlayerSpeed = 8f;
                        ObstaclesDistance = 9f;
                        return;
                    case GameDifficulty.Hard:
                        PlayerSpeed = 10f;
                        ObstaclesDistance = 7f;
                        return;
                }

            }
        }

        public static Vector3 PlayerPose = Vector3.zero;
        public static float PlayerSpeed = 10f;
        public static float WorldGravity = 5f;
        public static float ObstaclesDistance = 7f;
        public static bool isPause = true;

        private static GameObject PlayerObject;
        #endregion
        #region Prefabs
        [Header("Prefabs")]
        public GameObject PlayerPrefab;
        #endregion
        #endregion
        #region StandartFunctions
        private void Awake() => ChangeGameState = OnChangeGameState;
        #endregion
        #region Custom functions
        /// <summary>
        /// Проверка изменения состояния игры
        /// </summary>
        /// <param name="new_state"></param>
        private void OnChangeGameState(GameState new_state)
        {
            switch (new_state)
            {
                case GameState.Start:
                    PlayerPose = Vector3.zero;
                    StartPlayer();
                    return;
                case GameState.End:
                    EndPlayer();
                    isPause = true;
                    return;
                default: return;
            }
        }

        /// <summary>
        /// Заспавнить игрока
        /// </summary>
        private void StartPlayer() => PlayerObject = Instantiate(PlayerPrefab);

        /// <summary>
        /// Задеспавнить игрока
        /// </summary>
        private void EndPlayer() => Destroy(PlayerObject);
        #endregion
    }

    /// <summary>
    /// Тип состояния игры
    /// </summary>
    public enum GameState : byte
    {
        Start = 0,
        Pause = 1,
        End = 2,
        MainMenu = 3
    }

    /// <summary>
    /// Сложность игры
    /// </summary>
    public enum GameDifficulty : byte
    {
        Easy = 0,
        Normal = 1,
        Hard = 2
    }
}