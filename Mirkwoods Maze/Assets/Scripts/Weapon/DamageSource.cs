using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int _damageAmount;

    private void Start()
    {
        MonoBehaviour currenActiveWeapon = ActiveWeapon.Instance.CurrentActiveWeapon;
        _damageAmount = (currenActiveWeapon as IWeapon).GetWeaponInfo()._weaponDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(_damageAmount);
    }

}
