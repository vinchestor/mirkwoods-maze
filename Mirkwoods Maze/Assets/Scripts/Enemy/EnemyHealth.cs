using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startHealth = 3;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
