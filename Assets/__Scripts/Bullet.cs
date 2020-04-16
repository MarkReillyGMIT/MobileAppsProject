using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	// Public Variables
	#region
	public Color explosionColor;
	public GameObject particleEmission;
	#endregion

	private float damage = 10f;

	public float GetDamage()
	{
		return damage;
	}

	public void Hit()
	{
		if (particleEmission)
		{
			GameObject particleColor = Instantiate(particleEmission, gameObject.transform.position, Quaternion.identity) as GameObject;
			ParticleSystem ps = particleColor.GetComponent<ParticleSystem>();
			ParticleSystem.MainModule psmain = ps.main;
			psmain.startColor = gameObject.GetComponent<SpriteRenderer>().color;
			particleColor.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
		}

		Destroy(gameObject);
	}
}
