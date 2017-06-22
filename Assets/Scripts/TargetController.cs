using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	private int m_life;
	private int m_MaxLife = 5;
	private Animator m_anim;
	[SerializeField] public Transform m_HeadMarker;

	private void Start()
	{
		m_life = m_MaxLife;
		m_anim = GetComponent<Animator> ();
	}

	private void Update () {
		if (m_life <= 0) {
			m_anim.SetBool ("broken", true);
			Invoke ("StandUp", 10f);
			m_life = m_MaxLife;
		}
	}

	private void StandUp() 
	{
		m_anim.SetBool ("broken", false);
	}

	public void GetDamaged()
	{
		m_life--;
	}


}
