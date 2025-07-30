using UnityEngine;

namespace MyGame.Scripts.Core
{
    public class Slot : MonoBehaviour
    {
        public ShapeType correctShapeType;

        private void OnTriggerEnter2D(Collider2D other)
        {
            var shape = other.GetComponent<ShapeItem>();
            if (shape == null) return;

            if (shape.Type == correctShapeType)
                shape.HandleCorrectDrop(transform.position);
            else
                shape.DestroyWithEffect();
        }
    }
}