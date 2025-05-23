using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }


    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Transform _weaponCollider;

    private PlayerControls _playerControls;


    private Vector2 _movement;
    private Rigidbody2D _rigidbody;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Knockback _knockback;

    private bool facingLeft = false;

    //
    protected override void Awake()
    {
        base.Awake();
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        ActiveInventory.Instance.EquipStartingWeapon();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        if (_playerControls != null)
        {
            _playerControls.Disable();
        }
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


    //public 
    public Transform GetWeaponCollider()
    {
        return _weaponCollider;
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
        if (_knockback.GettingKnockedBack || PlayerHealth.Instance.isDead)
        {
            return;
        }

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