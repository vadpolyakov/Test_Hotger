using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    /// <summary>
    /// Скрипт управляющий генерацией мира
    /// </summary>
    public class WorldController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private SpriteRenderer[] SpriteRenderers;
        [SerializeField] private BoxCollider2D[] BoxCollider2Ds;
        [SerializeField] private GameObject ObstaclePrefab;
        [SerializeField] private Transform ObstaclesParent;

        private float Player_XPose => GameController.PlayerPose.x;
        private float ObstaclesDistance => GameController.ObstaclesDistance;
        private List<GameObject> ObstaclesBuffer = new List<GameObject>();
        private Vector3 LastObstacle = Vector3.zero;
        #endregion
        #region Standart functions
        private void Start()
        {
            GameController.ChangeGameState += OnChangeGameState;
        }

        void Update()
        {
            foreach (SpriteRenderer renderer in SpriteRenderers)
                renderer.size = new Vector2(20 + Player_XPose, renderer.size.y);
            foreach (BoxCollider2D collider2D in BoxCollider2Ds)
                collider2D.offset = new Vector2(10 + Player_XPose, collider2D.offset.y);

            if (LastObstacle.x - Player_XPose <= ObstaclesDistance || LastObstacle == Vector3.zero)
            {
                if (ObstaclesBuffer.Count == 10)
                {
                    Destroy(ObstaclesBuffer[0]);
                    ObstaclesBuffer.RemoveAt(0);
                }
                ObstaclesBuffer.Add(GenerateObstacle());
            }
        }
        #endregion

        /// <summary>
        /// Проверка изменения состояния игры
        /// </summary>
        /// <param name="new_state"></param>
        private void OnChangeGameState(GameState new_state)
        {
            switch(new_state)
            {
                case GameState.Start:
                    Clear();
                    return;
                default: return;
            }
        }

        /// <summary>
        /// Генерация препятствия
        /// </summary>
        /// <returns></returns>
        private GameObject GenerateObstacle()
        {
            Vector2 pos = new Vector2(LastObstacle.x + ObstaclesDistance, 0);
            float rnd = Random.Range(0f, 1f);
            bool is_double = false;
            if (rnd >= .65f)
            {
                if (rnd < .80f)
                    pos += Vector2.up * 1.5f;
                else if (rnd > .84f)
                    pos += Vector2.down * 1.5f;
                is_double = true;
            }
            else
            {
                pos += Vector2.up * Random.Range(-2, 2);
            }

            GameObject obs = Instantiate(ObstaclePrefab, pos, Quaternion.identity, ObstaclesParent);
            if (is_double)
            {
                obs.GetComponent<BoxCollider2D>().size += Vector2.up;
                obs.GetComponent<SpriteRenderer>().size += Vector2.up;
            }
            LastObstacle = pos;
            return obs;
        }

        /// <summary>
        /// Удаление всех препятствий
        /// </summary>
        private void Clear()
        {
            for (int i = 0; i < ObstaclesBuffer.Count; i++)
                Destroy(ObstaclesBuffer[i]);
            ObstaclesBuffer.Clear();
            LastObstacle = Vector3.zero;
            foreach (SpriteRenderer renderer in SpriteRenderers)
                renderer.size = new Vector2(20, renderer.size.y);
            foreach (BoxCollider2D collider2D in BoxCollider2Ds)
                collider2D.offset = new Vector2(10, collider2D.offset.y);
        }
    }
}