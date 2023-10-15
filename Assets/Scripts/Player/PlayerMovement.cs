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

            _inputManager.RunEvent += (direction) =>
            {
                _direction = direction;
            };
            _inputManager.JumpEvent += () =>
            {
                _isJump = true;
            };
            _playerCollisions.EnterIntoGroundColliderEvent += (colider) =>
            {
                _isGrounded = colider.transform.position.y < (transform.position.y - 0.5f + 0.1f);
                Physics2D.IgnoreCollision(_capsuleCollider2D, colider, !_isGrounded);
            };
            _playerCollisions.EnterIntoLadderColliderEvent += _ =>
            {
                _isInLadderColider = true;
            };
            _playerCollisions.ExitFromGroundColliderEvent += () =>
            {
                _isGrounded = false;
            };
            _playerCollisions.ExitFromLadderColliderEvent += () =>
            {
                _isInLadderColider = false;
            };
        }
        private void FixedUpdate()
        {
            var resVec = Vector2.zero;

            if (_direction.x != 0)
            {
                resVec.x += _runSpeed * Mathf.Sign(_direction.x);
            }
        
            if (_isInLadderColider && _direction.y > 0)
            {
                resVec.x = 0;
                resVec.y += _climbingSpeed * _direction.y;
            }
        
            if (_isJump && _isGrounded)
            {
                resVec += Vector2.up * _jumpStrenght;
                _isJump = false;
            }
            else
            {
                resVec += Physics2D.gravity * Time.fixedDeltaTime * 13;
            }
        
            _rigidbody.MovePosition(_rigidbody.position + resVec * Time.fixedDeltaTime);
            _direction = Vector2.zero;
        }
    }
}
