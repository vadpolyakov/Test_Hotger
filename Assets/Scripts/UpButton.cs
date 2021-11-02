using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Test
{
    /// <summary>
    /// Скрипт события нажатия на кнопку "Вверх"
    /// </summary>
    public class UpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region OnPointerEvents
        public void OnPointerDown(PointerEventData eventData)
        {
            PlayerController.isUp = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PlayerController.isUp = false;
        }
        #endregion
    }
}