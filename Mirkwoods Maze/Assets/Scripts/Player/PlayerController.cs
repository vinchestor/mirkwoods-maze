
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    public static PlayerController Instance;

    [SerializeField] private float _moveSpeed = 1f;

    private PlayerControls _playerControls;


    private Vector2 _movement;
    private Rigidbody2D _rigidbody;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool facingLeft = false;

    //
    private void Awake()
    {
        Instance = this;
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }



    //private 

    private void PlayerInput()
    {
        _movement = _playerControls.Movement.Move.ReadValue<Vector2>();

        _animator.SetFloat("moveX", _movement.x);
        _animator.SetFloat("moveY", _movement.y);
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * (_moveSpeed * Time.fixedDeltaTime));
    }

    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            _spriteRenderer.flipX = true;
            FacingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            FacingLeft = false;
        }
    }


}