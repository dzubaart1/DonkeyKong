using System.Collections.Generic;
using Input;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(PlayerCollisions), typeof(InputManager))]
    public class PlayerSpriteAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite _climbingSprite;
        [SerializeField] private List<Sprite> _runSprites;
        
        private SpriteRenderer _spriteRenderer;
        private InputManager _inputManager;
        private PlayerCollisions _playerCollisions;

        private int _runSpriteIndex;
        private bool _isInLadderColider;
        private Vector2 _direction;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _playerCollisions = GetComponent<PlayerCollisions>();
            _inputManager = GetComponent<InputManager>();

            _inputManager.RunEvent += OnRun;
            _playerCollisions.EnterIntoLadderColliderEvent += _ =>
            {
                _isInLadderColider = true;
            };

            _playerCollisions.ExitFromLadderColliderEvent += () =>
            {
                _isInLadderColider = false;
            };
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(AnimateSprite), 1f/12f,1f/12f);
        }
        
        private void OnDisable()
        {
            CancelInvoke();
        }

        private void OnRun(Vector2 direction)
        {
            _direction = direction;
            if (direction.x > 0f)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else if (direction.x < 0f)
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
        
        private void AnimateSprite()
        {
            if (_isInLadderColider)
            {
                _spriteRenderer.sprite = _climbingSprite;
            }
            else if(_direction.x != 0)
            {
                _runSpriteIndex = (_runSpriteIndex + 1) % _runSprites.Count;
                _spriteRenderer.sprite = _runSprites[_runSpriteIndex];
            }
        }
    }
}