using UnityEngine;
using UnityEngine.Serialization;

namespace MeatAndSoap
{
    public class CollisionsGatherer : MonoBehaviour
    {
        [SerializeField] private float _detectionDistance = 0.1f;
        [SerializeField] private LayerMask _detectionMask;

        [FormerlySerializedAs("col")]
        [SerializeField]
        private Collider2D _col;
        
        
        public void Gather(MovementController.FrameCollisions frameCollisions)
        {
            switch (_col.GetType().Name)
            {
                case nameof(CircleCollider2D):
                    Gather(_col as CircleCollider2D, frameCollisions);
                    return;
                case nameof(CapsuleCollider2D):
                    Gather(_col as CapsuleCollider2D, frameCollisions);
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
            frameCollisions.groundHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.down, _detectionDistance,
                _detectionMask);
            frameCollisions.cellingHit = Physics2D.CapsuleCast(col.bounds.center,col.size,  col.direction, 0, Vector2.up,
                _detectionDistance, _detectionMask);
            frameCollisions.leftHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.left, _detectionDistance,
                _detectionMask);
            frameCollisions.rightHit = Physics2D.CapsuleCast(col.bounds.center, col.size, col.direction, 0, Vector2.right, _detectionDistance,
                _detectionMask);
        }
        
    }
}