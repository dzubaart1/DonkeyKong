using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(InputManager), typeof(PlayerCollisions))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _runSpeed = 3f;
        [SerializeField] private float _climbingSpeed = 10f;
        [SerializeField] private float _jumpStrenght = 30f;
    
        private Rigidbody2D _rigidbody;
        private InputManager _inputManager;
        private PlayerCollisions _playerCollisions;
        private CapsuleCollider2D _capsuleCollider2D;
    
        private Vector2 _direction;
        private bool _isJump, _isGrounded, _isInLadderColider;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputManager = GetComponent<InputManager>();
            _playerCollisions = GetComponent<PlayerCollisions>();
            _capsuleCollider2D = GetComponent<CapsuleCollider2D>();

            _inputManager.RunEvent += OnRun;
            _inputManager.JumpEvent += OnJump;
            _playerCollisions.EnterIntoGroundColliderEvent += OnEnterIntoGroundCollider;
            _playerCollisions.EnterIntoLadderColliderEvent += OnEnterIntoLadderCollider;
            _playerCollisions.ExitFromGroundColliderEvent += OnExitFromGroundCollider;
            _playerCollisions.ExitFromLadderColliderEvent += OnExitFromLadderCollider;
        }

        private void OnDestroy()
        {
            _inputManager.RunEvent -= OnRun;
            _inputManager.JumpEvent -= OnJump;
            _playerCollisions.EnterIntoGroundColliderEvent -= OnEnterIntoGroundCollider;
            _playerCollisions.EnterIntoLadderColliderEvent -= OnEnterIntoLadderCollider;
            _playerCollisions.ExitFromGroundColliderEvent -= OnExitFromGroundCollider;
            _playerCollisions.ExitFromLadderColliderEvent -= OnExitFromLadderCollider;
        }

        private void FixedUpdate()
        {
            var resVec = Vector2.zero;

            if (_direction.x != 0)
            {
                resVec.x += _runSpeed * Mathf.Sign(_direction.x);
            }
        
            if (_isInLadderColider && _direction.y != 0)
            {
                resVec.x = 0;
                resVec.y += _climbingSpeed * _direction.y;
            }
        
            if (_isJump && _isGrounded)
            {
                resVec += Vector2.up * _jumpStrenght;
                _isJump = false;
            }
            else if(!_isInLadderColider)
            {
                resVec += Physics2D.gravity * Time.fixedDeltaTime * 13;
            }
        
            _rigidbody.MovePosition(_rigidbody.position + resVec * Time.fixedDeltaTime);
            _direction = Vector2.zero;
        }

        private void OnRun(Vector2 direction)
        {
            _direction = direction;
        }

        private void OnJump()
        {
            _isJump = true;
        }

        private void OnEnterIntoGroundCollider(Collider2D collider2d)
        {
            _isGrounded = collider2d.transform.position.y < (transform.position.y - 0.5f + 0.1f);
            Physics2D.IgnoreCollision(_capsuleCollider2D, collider2d, !_isGrounded);
        }

        private void OnEnterIntoLadderCollider(Collider2D collider2d)
        {
            _isInLadderColider = true;
        }

        private void OnExitFromGroundCollider()
        {
            _isGrounded = false;
        }

        private void OnExitFromLadderCollider()
        {
            _isInLadderColider = false;
        }
    }
}
