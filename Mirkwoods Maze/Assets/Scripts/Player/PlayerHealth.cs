using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 3;
    [SerializeField] private float _knockBackTrustAmount = 10f;
    [SerializeField] private float _damageRecoveryTime = 1f;

    private int _currentHealth;
    private bool _canTakeDamage = true;
    private Knockback _knockback;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }

    public  void TakeDamage(int damageAmoint, Transform hitTransform) 
    {
        if (!_canTakeDamage) { return; }

        ScreenShakeManager.Instance.ShakeScreen();

        _knockback.GetKnockedBack(hitTransform, _knockBackTrustAmount);
        StartCoroutine(_flash.FlashRoutine());
        _canTakeDamage = false;
        _currentHealth -= damageAmoint;

        StartCoroutine(DamageRecoveryRoutine());
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);

        _canTakeDamage = true;
    }
}
