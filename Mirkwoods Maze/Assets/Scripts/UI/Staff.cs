using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo _weaponInfo;
    [SerializeField] private GameObject _magicLaser;
    [SerializeField] private Transform _magicLaserSpawnPoint;

    private Animator _animator;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    public void Attack()
    {
        _animator.SetTrigger(ATTACK_HASH);
    }

    public void SpawnStaffProjectileEvent()
    {
        GameObject newLaser = Instantiate(_magicLaser, _magicLaserSpawnPoint.position, Quaternion.identity);
        newLaser.GetComponent<MagicLaser>().UpdateLaserRange(_weaponInfo._weaponRange);
    }

    public WeaponInfo GetWeaponInfo()
    {
        return _weaponInfo;
    }



    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        if (mousePos.x > playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
