using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    /// <summary>
    /// Скрипт управления UI
    /// </summary>
    public class UIController : MonoBehaviour
    {
        private UIState CurrentState
        {
            set
            {
                if (value == UIState.MainMenu) MainMenu_CG.alpha = 1f;
                else MainMenu_CG.alpha = 0f;
                MainMenu_CG.blocksRaycasts = value == UIState.MainMenu;
                MainMenu_CG.interactable = value == UIState.MainMenu;

                if (value == UIState.Game) InGame_CG.alpha = 1f;
                else InGame_CG.alpha = 0f;
                InGame_CG.blocksRaycasts = value == UIState.Game;
                InGame_CG.interactable = value == UIState.Game;

                if (value == UIState.RestartMenu) Restart_CG.alpha = 1f;
                else Restart_CG.alpha = 0f;
                Restart_CG.interactable = value == UIState.RestartMenu;
                Restart_CG.blocksRaycasts = value == UIState.RestartMenu;
            }
        }
        #region UI elements
        [Header("Main menu")]
        public CanvasGroup MainMenu_CG;
        public Button MainMenu_EasyButton;
        public Button MainMenu_NormalButton;
        public Button MainMenu_HardButton;
        public Button MainMenu_StartButton;

        [Space(4), Header("In game")]
        public CanvasGroup InGame_CG;
        public Text InGame_Timer;
        public Button InGame_TouchToStart_Button;

        [Space(4), Header("Restart")]
        public CanvasGroup Restart_CG;
        public Text Restart_Time;
        public Button Restart_ChangeDif_Button;
        public Button Restart_Button;
        #endregion
        #region Standart functions
        void Start()
        {
            GameController.ChangeGameState += OnChangeGameState;

            MainMenu_EasyButton.onClick.AddListener(delegate { SetDif(GameDifficulty.Easy); });
            MainMenu_NormalButton.onClick.AddListener(delegate { SetDif(GameDifficulty.Normal); });
            MainMenu_HardButton.onClick.AddListener(delegate { SetDif(GameDifficulty.Hard); });
            MainMenu_StartButton.onClick.AddListener(delegate { GameController.ChangeGameState.Invoke(GameState.Start); });

            InGame_TouchToStart_Button.onClick.AddListener(delegate { GameController.isPause = false; InGame_TouchToStart_Button.gameObject.SetActive(false); });

            Restart_ChangeDif_Button.onClick.AddListener(delegate { GameController.ChangeGameState.Invoke(GameState.MainMenu); });
            Restart_Button.onClick.AddListener(delegate { GameController.ChangeGameState.Invoke(GameState.Start); });

            SetDif(GameDifficulty.Normal);
        }

        private void FixedUpdate()
        {
            InGame_Timer.text = PlayerController.PlayerTime.ToString("0.0s");
        }
        #endregion
        /// <summary>
        /// Изменить сложность игры
        /// </summary>
        /// <param name="new_dif"></param>
        private void SetDif(GameDifficulty new_dif)
        {
            MainMenu_EasyButton.interactable = new_dif != GameDifficulty.Easy;
            MainMenu_NormalButton.interactable = new_dif != GameDifficulty.Normal;
            MainMenu_HardButton.interactable = new_dif != GameDifficulty.Hard;
            GameController.GameDifficulty = new_dif;
        }

        /// <summary>
        /// Проверка изменения состояния игры
        /// </summary>
        /// <param name="new_state"></param>
        private void OnChangeGameState(GameState new_state)
        {
            switch(new_state)
            {
                case GameState.Start:
                    CurrentState = UIState.Game;
                    InGame_TouchToStart_Button.gameObject.SetActive(true);
                    break;
                case GameState.Pause:
                    CurrentState = UIState.Pause;
                    break;
                case GameState.End:
                    CurrentState = UIState.RestartMenu;
                    Restart_Time.text = "Ваше время - " + PlayerController.PlayerTime.ToString("0.0s");
                    break;
                case GameState.MainMenu:
                    CurrentState = UIState.MainMenu;
                    break;
            }
        }

        /// <summary>
        /// Состояние интерфейса
        /// </summary>
        enum UIState
        {
            MainMenu,
            Pause,
            RestartMenu,
            Game
        }
    }
}