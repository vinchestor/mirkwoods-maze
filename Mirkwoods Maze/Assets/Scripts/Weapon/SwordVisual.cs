using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVisual : MonoBehaviour
{
    [SerializeField] private GameObject _swordSlash;
    [SerializeField] private Transform _slashSpawnPoint;
    [SerializeField] private Transform _weaponCollider;

    private PlayerControls _playerControls;
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;

    private const string ATTACK = "Attack";

    private GameObject _slash;

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
        _playerControls.Combat.Attack.started += _ => Attack();
    }

    private void Update()
    {
        MouseFollowWithOffset();
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
        _animator.SetTrigger(ATTACK);
        _weaponCollider.gameObject.SetActive(false); 
        _weaponCollider.gameObject.SetActive(true);

        _slash = Instantiate(_swordSlash, _slashSpawnPoint.position, Quaternion.identity);
        _slash.transform.parent = this.transform.parent;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(_playerController.transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
;        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}