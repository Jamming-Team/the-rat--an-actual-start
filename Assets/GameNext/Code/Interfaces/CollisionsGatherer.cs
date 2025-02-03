using UnityEngine;

namespace GameNext
{
    public class CollisionsGatherer : MonoBehaviour
    {
        [SerializeField] private float _detectionDistance = 0.1f;
        [SerializeField] private LayerMask _detectionMask;
        
        
        public void Gather(Collider2D col, MovementController.FrameCollisions frameCollisions)
        {
            switch (col.GetType().Name)
            {
                case nameof(CircleCollider2D):
                    Gather(col as CircleCollider2D, frameCollisions);
                    return;
            }
        }
        
        public void Gather(CircleCollider2D col, MovementController.FrameCollisions frameCollisions)
        {
            frameCollisions.groundHit = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.down, _detectionDistance,
                _detectionMask);
            frameCollisions.cellingHit = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.up,
                _detectionDistance, _detectionMask);
            frameCollisions.leftHit = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.left, _detectionDistance,
                _detectionMask);
            frameCollisions.rightHit = Physics2D.CircleCast(col.bounds.center, col.radius, Vector2.right, _detectionDistance,
                _detectionMask);
        }
        
        public void Gather(CapsuleCollider2D col, MovementController.FrameCollisions frameCollisions)
        {

        }
        
    }
}