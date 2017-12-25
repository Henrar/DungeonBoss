using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PaladinAttack : AEnemyAttack
{
    public Animator Animator;

    public float Damage = 50;

    public override void Attack(GameObject who)
    {
        Animator.SetBool("Attack", true);
        StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        Animator.SetBool("Attack", false);
    }
}
