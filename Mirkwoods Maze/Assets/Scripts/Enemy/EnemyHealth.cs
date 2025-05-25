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
        damage = 1;
        _currentHealth -= damage;
        _knockback.GetKnockedBack(PlayerController.Instance.transform, 10f);
        StartCoroutine(_flash.FlashRoutine());
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDead = true;

        var collider = GetComponent<Collider2D>();
        if (collider != null) collider.enabled = false;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        if (_knockback != null)
        {
            StopAllCoroutines();
        }

        _animator.SetTrigger(DEATH);
        Destroy(gameObject, 0.7f);
    }
}