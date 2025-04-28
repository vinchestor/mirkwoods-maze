using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 1;
    [SerializeField] private float _damageCooldown = 0.5f;

    private float _lastDamageTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TryDealDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TryDealDamage(collision);
    }

    private void TryDealDamage(Collider2D collision)
    {
        if (Time.time - _lastDamageTime < _damageCooldown) return;

        if (collision.gameObject.GetComponent<EnemyHealth>())
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(_damageAmount);
            _lastDamageTime = Time.time;
        }
    }
}
