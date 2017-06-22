using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

	private RaycastHit hit;
	[SerializeField] private GameObject m_HitObjectSparkle;
	[SerializeField] private GameObject m_MuzzleSparkle;
	private AudioSource m_GunAudioSource;
	[SerializeField] private AudioClip m_fire;
	[SerializeField] private AudioClip m_reload;
	private bool m_canShot = true;
	private bool m_onReloading = false;
	private float m_coolTime;
	[SerializeField] public int m_CurrentBulletNum;
	private int m_BulletLimit = 30;
	[SerializeField] public int m_ExtraBulletNum = 150;
	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private UIController uiController;
	private bool onSniping = false;
	[SerializeField] private Camera mainCamera;

	private void Start()
	{
		m_GunAudioSource = GetComponent<AudioSource> ();
		m_coolTime = 0.5f;
		m_CurrentBulletNum = m_BulletLimit;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown (0) && m_canShot && m_CurrentBulletNum > 0) Shot ();

		if (Input.GetKeyDown ("r") && !m_onReloading) Reload ();

		if (Input.GetMouseButtonDown (1)) ChangeSnipeMode ();
	}

	private void Shot()
	{
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
		m_GunAudioSource.PlayOneShot (m_fire);
		m_canShot = false;
		Invoke ("EnableToShot", m_coolTime);
		m_CurrentBulletNum--;
		GameObject muzzleSparkle = Instantiate (
			m_MuzzleSparkle,
			transform
		);
		Destroy (muzzleSparkle, 0.1f);

		if (Physics.Raycast (ray, out hit)) {
			if (hit.collider) {
				GameObject hitObjectSparkle = Instantiate (
					m_HitObjectSparkle,
					hit.point - ray.direction * 0.25f,
					Camera.main.transform.rotation
				);
				Destroy (hitObjectSparkle, 0.5f);

				if (hit.collider.transform.parent && hit.collider.transform.parent.tag == "Target") {
					TargetController target = hit.collider.transform.parent.GetComponent<TargetController> ();
					target.GetDamaged ();

					Vector3 centerPosition = target.m_HeadMarker.position;
					float distance = Vector3.Distance (centerPosition, hit.point);
					scoreManager.GetScore (distance);
				}
			}
		}
	}

	private void EnableToShot() {
		m_canShot = true;
	}

	private void Reload()
	{
		m_GunAudioSource.PlayOneShot (m_reload);
		m_onReloading = true;
		m_canShot = false;
		Invoke ("EnableToShot", 2.1f);
		Invoke ("EndOfReload", 2.1f);
		while (m_CurrentBulletNum < m_BulletLimit && m_ExtraBulletNum > 0) {
			m_CurrentBulletNum++;
			m_ExtraBulletNum--;
		}
	}

	private void EndOfReload ()
	{
		m_onReloading = false;
	}

	private void ChangeSnipeMode()
	{
		if (!onSniping) {
			mainCamera.fieldOfView = 25;
			uiController.snipeImageEnabled ();
			onSniping = true;
		} else {
			mainCamera.fieldOfView = 60;
			uiController.snipeImageNotEnabled ();
			onSniping = false;
		}
	}
}
