using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AdvancedUGUI
{
    [AddComponentMenu("UI/AdvancedUGUI/Click Extension")]
    public class AdvancedClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private float longPressDelay = 0.5f;

        private float timer = 0f;
        private Coroutine current;

        private bool isExit;

        public UnityEvent onClick;
        public UnityEvent onRightClick;
        public UnityEvent onMiddleClick;
        public UnityEvent onLongClick;

        private IEnumerator Action()
        {
            timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            current = StartCoroutine(Action());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (timer >= longPressDelay && !isExit) onLongClick?.Invoke();
                else if (timer < longPressDelay && !isExit) onClick?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                if (!isExit) onRightClick?.Invoke();
            }
            else
            {
                if (!isExit) onMiddleClick?.Invoke();
            }
            StopCoroutine(current);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isExit = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isExit = true;
        }
    }
}