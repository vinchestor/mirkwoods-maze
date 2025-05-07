using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material _whiteFlashMaterial;
    [SerializeField] private float _restoreDefaultMaterialTime = 0.2f;

    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = _spriteRenderer.material;
    }

    public float GetRestoreMatTime()
    {
        return _restoreDefaultMaterialTime;
    }

    public IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = _whiteFlashMaterial;
        yield return new WaitForSeconds(_restoreDefaultMaterialTime);
        _spriteRenderer.material = _defaultMaterial;
    }
}
