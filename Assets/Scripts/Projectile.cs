using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float Damage = 50;

    private bool _damageDealt = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Joe" && !_damageDealt)
        {
            var stats = other.gameObject.GetComponent<HeroStats>();
            stats.Health -= Damage;
            _damageDealt = true;
        }
        Destroy(this.gameObject);

    }
}
