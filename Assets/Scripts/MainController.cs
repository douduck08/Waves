using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public WaveSource[] Sources;
	public float RoundTime = 6f;
	public float WaveSpeed = 1f;

	private float m_timer;

	void Start () {
		m_timer = 0f;	
	}
	
	void Update () {
		if (m_timer < RoundTime) {
			m_timer += Time.deltaTime;
		} else {
			PlayAllPulse ();
			m_timer = 0;
		}
	}

	private void PlayAllPulse() {
		for (int i = 0; i < Sources.Length; i++) {
			Sources [i].Pulse (WaveSpeed);
		}
	}

	private void PlayLineParticle() {

	}
}
