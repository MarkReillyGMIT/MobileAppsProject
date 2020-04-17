using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
/*
 * Controls players movement and health
 */

public class Player : MonoBehaviour
{
    // Public Variables
    //Player Health
    [SerializeField] float maxHealth = 100f;
    //Sound of player being destroyed
    [SerializeField] AudioClip destroySound;
    //Particle explosions
    [SerializeField] GameObject dieParticleEmission;
    [SerializeField] GameObject hitParticleEmission;
    [SerializeField] GameObject particleEmission;
    [SerializeField] Color explosionColor;
    [SerializeField] Image fill;
    //Color of max and min Health bar
    [SerializeField] Color maxHealthColor = Color.green;
    [SerializeField] Color minHealthColor = Color.red;

    //Private Variables
    //Speed of player
    [SerializeField] private float speed = 5.0f;
    private float sfxVolume;
    private static float health;
    private Slider healthSlider;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        //rb equal to Rigidbody2D
        rb = GetComponent<Rigidbody2D>();
        //Set health equal to maxHealth
        health = maxHealth;
        //Find health slider
        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthSlider.wholeNumbers = true;
        healthSlider.maxValue = health;
        healthSlider.minValue = 0;
        sfxVolume = MusicPlayer.GetSFXVolume();
    }
    void Update()
    {
        //add hMovement
        // if the player presses the up arrow, then move
        float vMovement = Input.GetAxis("Vertical");
        float hMovement = Input.GetAxis("Horizontal");
        // apply a force, get the player moving.
        rb.velocity = new Vector2(hMovement * speed, 
                                    vMovement * speed);
        // keep the player on the screen
        //Reference: https://gamedev.stackexchange.com/questions/146903/how-to-restrict-the-players-movement-win-relation-to-screen-bounds-in-unity-wi
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
        //Check if health if less than or equal to zero
        if (health <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // if the player collides with any of the below gameObjects 
        // Get the gameObjects damage value and take away from health bar
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        EnemyAI enemyAI = collider.gameObject.GetComponent<EnemyAI>();
        BulletAI bulletAi = collider.GetComponent<BulletAI>();

        if (enemy)
        {
            Hit(enemy.GetDamage());
            enemy.Hit();
        }

        if (enemyAI)
        {
            Hit(enemyAI.GetDamage());
            enemyAI.Hit();
        }

        if (bulletAi)
        {
            Hit(bulletAi.GetDamage());
            bulletAi.Hit();
        }

    }
    //Takes in damage value and subtracts it from the current health value
    //Checks if health is greater than or equal to zero , if not call Die()
    void Hit(float damage)
    {
        health -= damage;

        DisplayHealth();

        if (health <= 0)
        {
            Die();
        }
    }

    public void Hit()
    {
        if (particleEmission)
        {
            GameObject particleColor = Instantiate(particleEmission, gameObject.transform.position, Quaternion.identity) as GameObject;
            ParticleSystem ps = particleColor.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            psmain.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        }

    }
    //Activates particle explosions,  destroysound 
    //Destroys gameobject
    //Changes scene to Failed scene
    void Die()
    {
        if (dieParticleEmission)
        {
            GameObject dieParticleColor = Instantiate(dieParticleEmission, gameObject.transform.position, Quaternion.identity) as GameObject;
            ParticleSystem ps = dieParticleColor.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            psmain.startColor = explosionColor;            
        }

        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100);

        if (destroySound)
        {
            AudioSource.PlayClipAtPoint(destroySound, Camera.main.transform.position, Mathf.Clamp(sfxVolume + 0.25f, 0.0f, 1.0f));
        }

        Destroy(this.gameObject, 2f);
        SceneManager.LoadSceneAsync("Failed");

    }
    //Displays health bar, particle explosion everytime health is lost
    void DisplayHealth()
    {
        healthSlider.value = health;
        Debug.Log("Health " + health);
        fill.color = Color.Lerp(minHealthColor, maxHealthColor, (float)healthSlider.value / maxHealth);

        if (hitParticleEmission)
        {
            GameObject hitParticleColor = Instantiate(hitParticleEmission) as GameObject;
            ParticleSystem ps = hitParticleColor.GetComponent<ParticleSystem>();
            ParticleSystem.MainModule psmain = ps.main;
            psmain.startColor = fill.color;
        }
    }

    //Method to call when adding health
    public void DisplayHealth(float healthPoints)
    {
        AddHealth(healthPoints);
        DisplayHealth();
    }
    //Method to be passed into DisplayHealth to add health
    public static void AddHealth(float healthPoints)
    {
        health += healthPoints;
    }
}
