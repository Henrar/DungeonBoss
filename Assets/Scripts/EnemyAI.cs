﻿using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public EnemyMovement EnemyMovement;

    private IEnumerator _movement;

    public void Start()
    {
        EnemyMovement.OnSideWallHitAction += OnSideWallHitAction;
        bool initialDirLeft = (Random.Range(0, 2)%2) == 0;

        StartCoroutine(RandomJump());
        _movement = RandomChangeDirection(initialDirLeft);
        StartCoroutine(_movement);
    }

    private void OnSideWallHitAction(EnemyMovement.MoveDirection moveDirection)
    {
        var initialDirLeft = moveDirection == EnemyMovement.MoveDirection.Right; // we want to chage the direction so set the initial one to opposite
        StopCoroutine(_movement);
        _movement = RandomChangeDirection(initialDirLeft);
        StartCoroutine(_movement);
    }

    IEnumerator RandomJump()
    {
        while (true)
        {
            var wait = Random.Range(0.5f, 2);
            yield return new WaitForSeconds(wait);
            EnemyMovement.Jump();
        }
    }

    IEnumerator RandomChangeDirection(bool isdirLeft)
    {
        EnemyMovement.Move(isdirLeft);
        while (true)
        {
            var wait = Random.Range(1, 3);
            yield return new WaitForSeconds(wait);
            isdirLeft = !isdirLeft;
            EnemyMovement.Move(isdirLeft);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            EnemyMovement.Jump();
        }
    }
}
