using UnityEngine;
using System.Collections;

public class WizardAttack : AEnemyAttack
{
    public Animator Animator;
    public GameObject WandTip;
    public GameObject WizardAttackPrefab;

    public override void Attack(GameObject who)
    {
        Animator.SetBool("Attack", true);
        var attack = (GameObject) Instantiate(WizardAttackPrefab, WandTip.transform.position, Quaternion.identity);

        float angle = 0;

        Vector3 relative = attack.transform.InverseTransformPoint(who.transform.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        attack.transform.Rotate(0, 0, -angle);
        attack.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 7), ForceMode2D.Impulse);

        StartCoroutine(EndAnimation());
    }

    private IEnumerator EndAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        Animator.SetBool("Attack", false);
    }
}
