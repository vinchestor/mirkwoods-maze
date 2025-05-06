using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class SwordVisual : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject _swordSlash; //slashAnimPrefab
    [SerializeField] private Transform _slashSpawnPoint;
    [SerializeField] private float _swordAttack = 0.1f;

    private Transform _weaponCollider;

    private Animator _animator;

    private const string ATTACK = "Attack";

    private GameObject _slash; //slashAnim

    //
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _weaponCollider = PlayerController.Instance.GetWeaponCollider();
        _slashSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
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

        if (PlayerController.Instance.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlip()
    {
        _slash.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            _slash.GetComponent<SpriteRenderer>().flipX = true;
        }
    }


    //priavte

    public void Attack()
    {

        _animator.SetTrigger(ATTACK);
        _weaponCollider.gameObject.SetActive(true);
        _slash = Instantiate(_swordSlash, _slashSpawnPoint.position, Quaternion.identity);
        _slash.transform.parent = this.transform.parent;

        StartCoroutine(AttackCDCollider());
    }

    private IEnumerator AttackCDCollider()
    {
        yield return new WaitForSeconds(_swordAttack);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }


    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
            ;
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            _weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}