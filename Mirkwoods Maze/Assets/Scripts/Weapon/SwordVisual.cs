using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    [SerializeField] private GameObject _swordSlash; //slashAnimPrefab
    [SerializeField] private Transform _slashSpawnPoint;
    [SerializeField] private Transform _weaponCollider;
    [SerializeField] private float _swordAttack = 0.1f;

    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;
    private bool _attackButtonDown = false;
    private bool _isAttacking = false;

    private const string ATTACK = "Attack";

    private GameObject _slash; //slashAnim

    //
    private void Awake()
    {
        _playerController = GetComponentInParent<PlayerController>();
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _animator = GetComponent<Animator>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        MouseFollowWithOffset();
        Attack();
    }



    //public

    public void DoneAttackEvent()
    {
        _weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlip()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlip()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    //priavte

    private void Attack()
    {
        if (_attackButtonDown && !_isAttacking)
        {
            _isAttacking = true;
            _animator.SetTrigger(ATTACK);
            _weaponCollider.gameObject.SetActive(true);
            _slash = Instantiate(_swordSlash, _slashSpawnPoint.position, Quaternion.identity);
            _slash.transform.parent = this.transform.parent;

            StartCoroutine(AttackCDCollider());
        }
    }

    private IEnumerator AttackCDCollider()
    {
        yield return new WaitForSeconds(_swordAttack);
        _isAttacking = false;
    }

    private void StartAttacking()
    {
        _attackButtonDown = true;
    }

    private void StopAttacking()
    {
        _attackButtonDown = false;
    }


    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
            ;
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}