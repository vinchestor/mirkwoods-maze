using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;

    private Rigidbody2D _rigidbody;
    private Vector2 _moveDir;
    private Knockback _knockback;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_knockback.gettingKnockedBack)
        {
            return;
        }

        _rigidbody.MovePosition(_rigidbody.position + _moveDir * (_moveSpeed * Time.fixedDeltaTime));
    }

    public void MoveTo(Vector2 targetPosition)
    {
        _moveDir = targetPosition;
    }
}
