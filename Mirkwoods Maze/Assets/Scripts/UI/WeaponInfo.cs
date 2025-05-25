using UnityEngine;


[CreateAssetMenu(menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject _weaponPrefab;
    public float _weaponCooldown;
    public int _weaponDamage;
    public float _weaponRange;
}
  