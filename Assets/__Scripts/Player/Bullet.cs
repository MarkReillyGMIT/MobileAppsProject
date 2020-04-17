using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Bullet script used to control player bullet
 */
public class Bullet : MonoBehaviour
{
	// Public Variables
	[SerializeField] GameObject particleEmission;
	
	//Private Variables
	//Bullet damage
	private float damage = 10f;

	//Public Methods
	//Gets the damage of bullet
	public float GetDamage()
	{
		return damage;
	}

	//Particle explosion and destroys gameObject
	public void Hit()
	{
		if (particleEmission)
		{
			GameObject particleColor = Instantiate(particleEmission, gameObject.transform.position, Quaternion.identity) as GameObject;
			ParticleSystem ps = particleColor.GetComponent<ParticleSystem>();
			ParticleSystem.MainModule psmain = ps.main;
			psmain.startColor = gameObject.GetComponent<SpriteRenderer>().color;
		}

		Destroy(gameObject);
	}
}
