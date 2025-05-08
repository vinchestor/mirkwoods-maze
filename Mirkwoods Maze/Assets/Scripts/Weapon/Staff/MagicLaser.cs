using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    [SerializeField] private float _laserGrowTime = 2f;

    private bool _isGrowing = true;
    private float laserRange;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider2D;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructible>() && !other.isTrigger) {
            _isGrowing = false;
        }
    }

    public void UpdateLaserRange(float laserRange)
    {
        this.laserRange = laserRange;
        StartCoroutine(IncreaseLaserLenghtRoutine());
    }

    private IEnumerator IncreaseLaserLenghtRoutine()
    {
        float timePassed = 0f;

        while (_spriteRenderer.size.x < laserRange && _isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / _laserGrowTime;

            _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            _capsuleCollider2D.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), _capsuleCollider2D.size.y);
            _capsuleCollider2D.offset = new Vector2((Mathf.Lerp(1f, laserRange, linearT)) / 2, _capsuleCollider2D.offset.y);

            yield return null;

        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = transform.position - mousePosition;
        transform.right = -direction;
    }
}
