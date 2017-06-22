using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	[SerializeField] private ScoreManager scoreManager;
	[SerializeField] private GunController gunController;

	[SerializeField] private Text scoreText;
	[SerializeField] private Text timerText;
	[SerializeField] private Text bulletNumText;
	[SerializeField] private Text extraBulletNumText;
	[SerializeField] private Image snipeImage;

	private float timer;

	private void Start ()
	{
		timer = 90f;
	}

	private void Update ()
	{
		timer -= Time.deltaTime;

		if (timer <= 0) {
			timer = 0.0f;
		}

		scoreText.text = "Pt:" + scoreManager.score;
		timerText.text = "Time" + timer.ToString ("0.0") + "s";
		bulletNumText.text = "Bullet:" + gunController.m_CurrentBulletNum;
		extraBulletNumText.text = "BulletBox:" + gunController.m_ExtraBulletNum;
	}

	public void snipeImageEnabled ()
	{
		snipeImage.enabled = true;
	}

	public void snipeImageNotEnabled ()
	{
		snipeImage.enabled = false;	
	}

}