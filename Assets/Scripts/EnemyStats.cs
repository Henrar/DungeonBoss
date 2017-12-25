using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnemyStats : MonoBehaviour {
    public float Strength;
    public float Health;
    public float SpellResistance;
    private float MaxHealth;

    void Start() {
        this.MaxHealth = this.Health;
    }

    public void takeDamage(float amount) {
        this.Health = this.Health - calculateSpellDamageAfterFactoringInSpellResistance(amount);
        checkIfShouldDie();    
    }

    private void checkIfShouldDie() {
        if (this.Health <= 0) {
            GameObject go = GameObject.Find("GameMode");
            GameMode gm = (GameMode)go.GetComponent(typeof(GameMode));
            if (gm != null) {
                if (this.gameObject.name == "Paladin") {
                    gm.addScoreForKilledPaladin();
                }
                else if (this.gameObject.name == "Wizzard") {
                    gm.addScoreForKilledMage();
                }
            }
            Destroy(this.gameObject);
        }
    }

    void Update() {
        if (Health >= 0.0f) {
            this.gameObject.GetComponent<HealthBar>().updateHealthBar(this.Health, this.MaxHealth);
        }
    }

    private float calculateSpellDamageAfterFactoringInSpellResistance(float amount) {
        if (amount - this.SpellResistance < 0) {
            return 0.0f;
        } else {
            return (amount - this.SpellResistance);
        }
    }
}