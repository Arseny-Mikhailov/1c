using UnityEngine;
using UnityEngine.EventSystems;

namespace MyGame.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        protected Vector3 StartPos { get; private set; } 
        private Vector3 _offset;
    
        private Rigidbody2D _rb;
        protected Rigidbody2D Rb => _rb;
        private Camera _mainCam;
    
        public bool isDragging;
        public bool isMoving = true;

        protected virtual void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _mainCam = Camera.main;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            StartPos = transform.position;
            _offset = transform.position - _mainCam.ScreenToWorldPoint(eventData.position);
            isDragging = true;
            isMoving = false;
            _rb.simulated = false;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            if (!isDragging) return;
        
            var mousePos = _mainCam.ScreenToWorldPoint(eventData.position);
            mousePos.z = 0;
            transform.position = mousePos + _offset;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
            _rb.simulated = true;
        }
    }
}