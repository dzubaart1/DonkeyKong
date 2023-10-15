using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CapsuleCollider2D),typeof(Rigidbody2D))]
    public class PlayerCollisions : MonoBehaviour
    {
        public event Action<Collider2D> EnterIntoLadderColliderEvent;
        public event Action<Collider2D>  EnterIntoGroundColliderEvent;
        public event Action ExitFromLadderColliderEvent;
        public event Action  ExitFromGroundColliderEvent;
        
        private CapsuleCollider2D _capsuleCollider;
        
        private Collider2D[] _colliders;
        [CanBeNull] private Collider2D _prevLadderCollider;
        [CanBeNull] private Collider2D _prevGroundCollider;
        [CanBeNull] private Collider2D _currentLadderCollider;
        [CanBeNull] private Collider2D _currentGroundCollider;
        
        private int _colidersCount;
        private Vector2 _size;
        
        private const int MAX_COLIDER_COUNT_OVERLAP = 4;

        private void Awake()
        {
            _capsuleCollider = GetComponent<CapsuleCollider2D>();
            
            _colliders = new Collider2D[MAX_COLIDER_COUNT_OVERLAP];
            
            _size = _capsuleCollider.bounds.size;
            _size.y += 0.05f;
            _size.x /= 2f;
        }

        private void FixedUpdate()
        {
            CheckCollisions();
        }

        private void CheckCollisions()
        {
            _currentLadderCollider = null;
            _currentGroundCollider = null;
            _colidersCount = Physics2D.OverlapBoxNonAlloc(transform.position, _size, 0f, _colliders);
            
            for (var i = 0; i < _colidersCount; i++)
            {
                if (_colliders[i].gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    _currentGroundCollider = _colliders[i];
                }
                if (_colliders[i].gameObject.layer == LayerMask.NameToLayer("Ladder"))
                {
                    _currentLadderCollider = _colliders[i];
                }
            }

            SendEvents();
        }

        private void SendEvents()
        {
            if (_prevGroundCollider is null && _currentGroundCollider is not null)
            {
                EnterIntoGroundColliderEvent?.Invoke(_currentGroundCollider);
                _prevGroundCollider = _currentGroundCollider;
            }

            if (_prevLadderCollider is null && _currentLadderCollider is not null)
            {
                EnterIntoLadderColliderEvent?.Invoke(_currentLadderCollider);
                _prevLadderCollider = _currentLadderCollider;
            }
            
            if (_prevGroundCollider is not null && _currentGroundCollider is null)
            {
                ExitFromGroundColliderEvent?.Invoke();
                _prevGroundCollider = null;
            }

            if (_prevLadderCollider is not null && _currentLadderCollider is null)
            {
                ExitFromLadderColliderEvent?.Invoke();
                _prevLadderCollider = null;
            }
        }
    }
}