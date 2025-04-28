using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startHealth = 3;

    private int _currentHealth;
    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(_flash.FlashRoutine());
    }

    public void DetectDeath()
    {
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
