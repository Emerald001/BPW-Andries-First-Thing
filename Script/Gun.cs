using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
	public float damage = 10f;
	public float range = 100f;
	public float impactForce = 30;
	public float fireRate = 15f;
	public float scopedFOV = 15f;
	private float normalFOV;

	public Camera fpsCam;
	public GameObject weaponCam;
	public GameObject impactEffect;
	public Animator shotAnimate;
	public Animator camAnimate;
	private bool isScoped = false;
	public GameObject overlay;
	
	public bool deadDeer = false;

	public float nextTimeToFire = 0f;

	void Update()
	{
		if (!PauseMenu.GameIsPaused)
		{
			if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
			{
				nextTimeToFire = Time.time + 1 / fireRate;
				Shoot();
				shotAnimate.SetTrigger("Shot");
				camAnimate.SetTrigger("Shot");
			}

			if (Input.GetButtonUp("Fire2"))
			{
				isScoped = !isScoped;
				shotAnimate.SetBool("Aiming", isScoped);

				if (isScoped) StartCoroutine(OnScoped());
				else OnUnscoped();
			}
		}

		void OnUnscoped()
		{
			overlay.SetActive(false);
			weaponCam.SetActive(true);

			fpsCam.fieldOfView = normalFOV;
		}

		IEnumerator OnScoped()
		{
			yield return new WaitForSeconds(.15f);

			overlay.SetActive(true);
			weaponCam.SetActive(false);

			normalFOV = fpsCam.fieldOfView;
			fpsCam.fieldOfView = scopedFOV;
		}

		void Shoot()
		{
			RaycastHit hit;

			if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
			{
				Target target = hit.transform.GetComponent<Target>();
				if (target != null)
				{
					target.TakeDamage(damage);
					Debug.Log("Hit!");
				}

				if (hit.rigidbody != null)
				{
					hit.rigidbody.AddForce(-hit.normal * impactForce);
					Debug.Log("And FLY!");
				}

				if (hit.transform.CompareTag("Deer"))
				{
					deadDeer = true;
				}

				GameObject ImpactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(ImpactGO, 2f);
			}
		}
	}
}
