using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    // Public Variables
    #region
    public float projectileSpeed = 1f;
    public float firingRate = 1f;
    public float maxHealth = 100f;
    public AudioClip shootSound;
    public AudioClip destroySound;
    public GameObject dieParticleEmission;
    public GameObject hitParticleEmission;
    public GameObject addBombParticleEmission;
    public GameObject shootBombParticleEmission;
    public Image fill;
    public Color explosionColor;
    public Color maxHealthColor = Color.green;
    public Color minHealthColor = Color.red;
    [SerializeField] private float speed = 5.0f;
    #endregion
    private float sfxVolume;
    private static float health;
    private Slider healthSlider;
    private Vector3 sfxPosition;
    private Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;

        healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        healthSlider.wholeNumbers = true;
        healthSlider.maxValue = health;
        healthSlider.minValue = 0;

        sfxVolume = MusicPlayer.GetSFXVolume();
        sfxPosition = new Vector3(this.transform.position.x, this.transform.position.y, -50); //For 3-D sound
    }

    // Update is called once per frame
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
                // float xValue = Mathf.clamp(value, min, max)
                // rb.position.x 
                float yValue = Mathf.Clamp(rb.position.y, -4.36f, 4.30f);
                float xValue = Mathf.Clamp(rb.position.x, -10.33f, 10.33f);

                rb.position = new Vector2(xValue, yValue);


        //DisplayHealth();
    }

    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.gameObject.GetComponent<Enemy>();
        ExtraHealth extraHealth = collider.GetComponent<ExtraHealth>();

        if (enemy)
        {
            Hit(enemy.GetDamage());
            enemy.Hit();
        }

        if (extraHealth)
        {
            AddHealth(extraHealth.GetAddHealth());
        }
    }

    void Hit(float damage)
    {
        health -= damage;

        DisplayHealth();

        if (health <= 0)
        {
            Die();
        }
    }    
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

        Invoke("NextLevel", 1f);

        Destroy(this.gameObject, 2f);
    }

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

    public void DisplayHealth(float healthPoints)
    {
        AddHealth(healthPoints);
        DisplayHealth();
    }

    public static void AddHealth(float healthPoints)
    {
        health += healthPoints;
    }
}
