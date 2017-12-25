using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

    public Texture2D HpBarTexture;
    private float lengthOfHpBar;
    private float percentOfHP;
    void Start () {
        
    }

    void OnGUI() {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x - 50, screenPosition.y - 100, lengthOfHpBar, 10), HpBarTexture);
    }

    public void updateHealthBar (float currentHealth, float maxHealth) {
        percentOfHP = currentHealth / maxHealth;
        lengthOfHpBar = percentOfHP * 100;
    }
}
