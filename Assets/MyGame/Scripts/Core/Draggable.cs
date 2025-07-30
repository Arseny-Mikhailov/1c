using UnityEngine;
using UnityEngine.EventSystems;

namespace MyGame.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public bool isDragging;
        public bool isMoving = true;
        private Camera _mainCam;
        private Vector3 _offset;

        protected Vector3 StartPos { get; private set; }
        protected Rigidbody2D Rb { get; private set; }

        protected virtual void Start()
        {
            Rb = GetComponent<Rigidbody2D>();
            _mainCam = Camera.main;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;

            var mousePos = _mainCam.ScreenToWorldPoint(eventData.position);
            mousePos.z = 0;
            transform.position = mousePos + _offset;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            StartPos = transform.position;
            _offset = transform.position - _mainCam.ScreenToWorldPoint(eventData.position);
            isDragging = true;
            isMoving = false;
            Rb.simulated = false;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
            Rb.simulated = true;
        }
    }
}