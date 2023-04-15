using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace AdvancedUGUI
{
    [RequireComponent(typeof(Button))]
    public class AdvancedButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private bool isExit;

        private float timer = 0f;
        private float delay = 1f;
        private Coroutine current;

        public UnityEvent onClick;
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
            if (timer >= delay && !isExit) onLongClick?.Invoke();
            else if (timer < delay && !isExit) onClick?.Invoke();
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