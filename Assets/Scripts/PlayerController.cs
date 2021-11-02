using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class PlayerController : MonoBehaviour
    {
        #region Static variables
        public static float PlayerTime = 0f;
        public static bool isUp = false;
        #endregion
        #region Variables
        private float PlayerSpeed => GameController.PlayerSpeed;
        private float Gravity => GameController.WorldGravity;
        private Transform CameraTransform;
        #endregion
        #region Standart funcions
        void Start()
        {
            CameraTransform = Camera.main.transform;
            CameraTransform.position = new Vector3(transform.position.x, transform.position.y, CameraTransform.position.z);
            PlayerTime = 0f;
        }

        void Update()
        {
            if (!GameController.isPause)
            {
                PlayerTime += Time.deltaTime;
                transform.position += Vector3.right * Time.deltaTime * PlayerSpeed;
                if(isUp)
                    transform.position += Vector3.up * Time.deltaTime * (PlayerSpeed * .5f);
                else
                    transform.position += Vector3.down * Time.deltaTime * (PlayerSpeed * .5f);

                CameraTransform.position = Vector3.Lerp(CameraTransform.position, new Vector3(transform.position.x, transform.position.y, CameraTransform.position.z), Time.deltaTime * PlayerSpeed);

                GameController.PlayerPose = transform.position;
            }
        }

        /// <summary>
        /// Проверка коллизии
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision) => GameController.ChangeGameState.Invoke(GameState.End);
        #endregion
    }
}