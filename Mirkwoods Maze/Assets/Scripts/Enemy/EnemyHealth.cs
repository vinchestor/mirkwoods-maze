using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _startHealth = 3;
    [SerializeField] private Animator _animator;

    private int _currentHealth;
    private Knockback _knockback;
    private Flash _flash;
    private bool _isDead = false;

    private const string DEATH = "Death";

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_isDead) return;

        _currentHealth -= damage;
        _knockback.GetKnockedBack(PlayerController.Instance.transform, 10f);
        StartCoroutine(_flash.FlashRoutine());
        DetectDeath();
    }

    public void DetectDeath()
    {
        if (_currentHealth <= 0 && !_isDead)
        {
            _isDead = true;
            StartCoroutine(Die());
        }
    }
    
    private IEnumerator Die()
    {
        _animator.SetTrigger(DEATH);

        var collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        yield return new WaitForSeconds(0.7f);

        Destroy(gameObject);
    }
}
