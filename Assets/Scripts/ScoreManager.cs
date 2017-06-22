using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public int score;

	private void Start () {
		score = 0;
	}

	public void GetScore(float distance)
	{
		if (distance < 0.2f) {
			score += 100;
		} else if (distance < 0.4f) {
			score += 80;
		} else if (distance < 0.6f) {
			score += 60;
		} else if (distance < 0.8f) {
			score += 40;
		} else if (distance < 1.0f) {
			score += 20;
		} else if (distance < 1.6f) {
			score += 10;
		}
	}
}
