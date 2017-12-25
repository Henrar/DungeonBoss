using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

    public float Damage = 50;

    public bool _lockDamage = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Joe" && !_lockDamage)
        {
            var stats = other.gameObject.GetComponent<HeroStats>();
            stats.Health -= Damage;
            _lockDamage = true;
            StartCoroutine(UnlockAttack());
        }
    }

    IEnumerator UnlockAttack()
    {
        yield return new WaitForSeconds(1);
        _lockDamage = false;
    }
}
