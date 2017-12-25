using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

    public float timeAlive = 5.0f;
    public float speed = 1.0f;
    public float damage;
    private Rigidbody2D _fireBallBody;

    void Start() {
        _fireBallBody = GetComponent<Rigidbody2D>();
    }

    void Update() {
        timeAlive = timeAlive - 0.05f;
        destroyBallIfTimeHasPassed();
    }

    private void destroyBallIfTimeHasPassed() {
        if (timeAlive <= 0.0f) {
            Destroy(this.gameObject);
        }
    }
   
    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name != "Joe") {
            Destroy(this.gameObject);
            if(hasHitEnemy(other)) {
                other.gameObject.GetComponent<EnemyStats>().takeDamage(this.damage);
            }
        }
    }

    private bool hasHitEnemy(Collider2D other) {
        return other.gameObject.name == "Wizzard" || other.gameObject.name == "Paladin";
    }
}
