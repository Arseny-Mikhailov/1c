using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyGame.Scripts.Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class ShapeItem : Draggable
    {
        [SerializeField] private new SpriteRenderer renderer;
        [SerializeField] private ParticleSystem destroyParticles;
        [SerializeField] private ParticleSystem successParticles;
        [SerializeField] private float screenBoundX = 10f;

        private float _speed;

        public ShapeType Type { get; private set; }

        protected override void Start()
        {
            base.Start();
            GameManager.Instance.RegisterShape(this);
        }

        private void Update()
        {
            CheckScreenBounds();

            if (isMoving && !isDragging) Move();
        }

        private void OnDestroy()
        {
            if (GameManager.Instance != null) GameManager.Instance.UnregisterShape(this);
        }

        public void Initialize(ShapeType shapeType, float moveSpeed)
        {
            Type = shapeType;
            _speed = moveSpeed;
        }

        private void Move()
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime);
        }

        private void CheckScreenBounds()
        {
            if (transform.position.x > screenBoundX) DestroyWithEffect();
        }

        public void DestroyWithEffect()
        {
            isMoving = false;

            if (destroyParticles != null)
            {
                renderer.enabled = false;
                GetComponent<Collider2D>().enabled = false;

                destroyParticles.Play();
                Destroy(gameObject, destroyParticles.main.duration);
            }
            else
            {
                Destroy(gameObject);
            }

            EventBus.Publish(new LifeLostEvent());
        }

        public void HandleCorrectDrop(Vector3 slotPosition)
        {
            isMoving = false;
            GetComponent<Collider2D>().enabled = false;

            if (successParticles != null) successParticles.Play();

            transform.DOMove(slotPosition, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
            {
                transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
                {
                    EventBus.Publish(new ShapeSortedEvent());
                    Destroy(gameObject);
                });
            });
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            isDragging = false;
            Rb.simulated = true;

            CheckForSlotDrop();
        }

        private void CheckForSlotDrop()
        {
            var hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);

            foreach (var hit in hits)
            {
                var slot = hit.GetComponent<Slot>();

                if (slot == null) continue;

                if (slot.correctShapeType == Type)
                    HandleCorrectDrop(slot.transform.position);
                else
                    DestroyWithEffect();

                return;
            }

            transform.DOMove(StartPos, 0.5f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() => isMoving = true);
        }
    }
}