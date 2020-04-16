//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;


//public class PlayerHealth : MonoBehaviour
//{
//    private Slider healthSlider;
//    private static float health;
//    public Color maxHealthColor = Color.green;
//    public Color minHealthColor = Color.red;
//    public Image fill;
//    public GameObject hitParticleEmission;
//    public float maxHealth = 100f;


//    private void Start()
//    {
//        health = maxHealth;

//        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
//        healthSlider.wholeNumbers = true;
//        healthSlider.maxValue = health;
//        healthSlider.minValue = 0;
//    }

//    void OnTriggerEnter2D(Collider2D collider)
//    {
//        Bullet missile = collider.gameObject.GetComponent<Bullet>();
//        Enemy enemy = collider.gameObject.GetComponent<Enemy>();

//        if (missile)
//        {
//            Hit(missile.GetDamage());
//            missile.Hit();
//            print("Damage Taken, Instance ID: " + GetInstanceID());
//        }

//        if (enemy)
//        {
//            Hit(enemy.GetDamage());
//            enemy.Hit();

//        }
//    }

//    void Hit(float damage)
//    {
//        health -= damage;

//        DisplayHealth();

//        if (health <= 0)
//        {
//            Die();
//        }
//    }

//    void Die()
//    {
//        Destroy(this.gameObject, 2f);
//    }

//    void DisplayHealth()
//    {
//        healthSlider.value = health;
//        fill.color = Color.Lerp(minHealthColor, maxHealthColor, (float)healthSlider.value / maxHealth);

//        if (hitParticleEmission)
//        {
//            GameObject hitParticleColor = Instantiate(hitParticleEmission) as GameObject;
//            hitParticleColor.GetComponent<ParticleSystem>().startColor = fill.color;
//        }
//    }

//    public void DisplayHealth(float healthPoints)
//    {
//        AddHealth(healthPoints);
//        DisplayHealth();
//    }

//    public static void AddHealth(float healthPoints)
//    {
//        health += healthPoints;
//    }
//}
