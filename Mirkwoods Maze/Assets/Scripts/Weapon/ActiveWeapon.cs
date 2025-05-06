using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour _currentActiveWeapon;

    private PlayerControls _playerControls;

    private bool _attackButtondown = false;
    private bool _isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
    }

    public void ToggleIsAttacking(bool value)
    {
        _isAttacking = value;
    }

    private void StartAttacking()
    {
        _attackButtondown = true;
    }

    private void StopAttacking()
    {
        _attackButtondown = false;
    }

    private void Attack()
    {
        if (_attackButtondown && !_isAttacking)
        {
            _isAttacking = true;
            (_currentActiveWeapon as IWeapon).Attack();
        }
    }
}
