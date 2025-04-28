using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{


    //

    //машина состояний
    private enum State
    {
        Roaming
    }

    private State _state;
    private EnemyPathfinding _enemyPathfinding;

    private void Awake()
    {
        _enemyPathfinding = GetComponent<EnemyPathfinding>();
        _state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }


    //public




    //private

    private IEnumerator RoamingRoutine()
    {
        while (_state == State.Roaming)
        {
            Vector2 roamingPosition = GetRoamingPosition();
            _enemyPathfinding.MoveTo(roamingPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
