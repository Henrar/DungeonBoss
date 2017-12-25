using UnityEngine;
using System.Collections;

public class HeroStats : MonoBehaviour {
    public float Health = 1000.0f;
    public float FireballDamage = 50.0f;

    private float MaxHealth;

    private GUIStyle guiStyle = new GUIStyle();
    void Start () {
        this.MaxHealth = this.Health;
    }

	void Update () {
        if (Health >= 0.0f) {
            this.gameObject.GetComponent<HealthBar>().updateHealthBar(this.Health, this.MaxHealth);
        }
        if (Health <= 0.0f) {
            GameObject go = GameObject.Find("GameMode");
            GameMode gm = (GameMode)go.GetComponent(typeof(GameMode));
            gm.playerDied();
            Destroy(this.gameObject); //DEATH
        }
	}
}
