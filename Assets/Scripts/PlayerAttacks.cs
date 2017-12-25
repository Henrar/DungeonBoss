using UnityEngine;
using System.Collections;

public class PlayerAttacks : MonoBehaviour {

    public GameObject fireballAttackPrefab;

    private Rigidbody2D _rigidBody;
    private Plane clickPlane;
    
    void Start () {
        _rigidBody = GetComponent<Rigidbody2D>();
    }
	
	void Update () {
        clickPlane = new Plane(-Vector3.forward, Vector3.zero);
        if (Input.GetMouseButtonDown(0)) {
            castFireball();
        }
    }

    private void castFireball() {
		var target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
        var direction = (target - transform.position).normalized;
		GameObject newFireball = (GameObject)Instantiate(fireballAttackPrefab, this.transform.position, Quaternion.identity);
		newFireball.GetComponent<Rigidbody2D>().AddForce(direction * 7, ForceMode2D.Impulse);
        Fireball fb = (Fireball)newFireball.GetComponent(typeof(Fireball));
        fb.damage = this.gameObject.GetComponent<HeroStats>().FireballDamage;
    }
}
