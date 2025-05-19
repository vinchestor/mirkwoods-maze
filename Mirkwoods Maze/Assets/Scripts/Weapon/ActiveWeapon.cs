using System.Collections;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon {  get; private set; }

    private PlayerControls _playerControls;
    private float _timeBetweenAttacks;

    private bool _attackButtondown = false;
    private bool _isAttacking = false;

    protected override void Awake()
    {
        base.Awake();

        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        _playerControls.Combat.Attack.started += _ => StartAttacking();
        _playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    //public

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;

        _timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo()._weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }


    //private

    private void AttackCooldown()
    {
        _isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(_timeBetweenAttacks);
        _isAttacking = false;
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
        if (_attackButtondown && !_isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
