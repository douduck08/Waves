using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public Color[] ColorSetting;

	public WaveSource[] Sources;
	public float RoundTime = 6f;
	public float WaveSpeed = 1f;
	public GameObject LineParticleTimerPrefab;

	private float m_timer;
	private LineParticleTimer[] m_LineParticleTimers;

	void Awake() {
		for (int i = 0; i < 6; i++) {
			Config.ColorPool [i] = ColorSetting [i];
		}
	}

	void Start () {
		m_timer = 0f;
		InitialLineParticleTimers ();
	}
	
	void Update () {
		if (m_timer < RoundTime) {
			m_timer += Time.deltaTime;
		} else {
			PlayAllPulse ();
			PlayAllLineParticle ();
			m_timer = 0;
		}
	}

	private void InitialLineParticleTimers() {
		m_LineParticleTimers = new LineParticleTimer[3];
		for (int i = 0; i < 3; i++) {
			GameObject go_ = GameObject.Instantiate<GameObject> (LineParticleTimerPrefab, transform);
			m_LineParticleTimers [i] = go_.GetComponent<LineParticleTimer> ();
			m_LineParticleTimers [i].SetPlayerEndCallback (LinParticleEnd);
		}
	}

	private void PlayAllPulse() {
		for (int i = 0; i < Sources.Length; i++) {
			Sources [i].Pulse (WaveSpeed);
		}
	}

	private void PlayAllLineParticle() {
		PlayLineParticle (0, Sources [0].transform.position, Sources [1].transform.position);
		PlayLineParticle (1, Sources [2].transform.position, Sources [1].transform.position);
		PlayLineParticle (2, Sources [2].transform.position, Sources [0].transform.position);
	}

	private void PlayLineParticle(int idx, Vector2 pos1, Vector2 pos2) {
		Vector2 diff_ = pos1 - pos2;
		Vector2 normal_ = new Vector2 (-diff_.x, diff_.y);
		m_LineParticleTimers [idx].PlayLineParticle (WaveSpeed, diff_.magnitude, (pos1 + pos2) / 2f, normal_ / normal_.magnitude);
	}

	private void LinParticleEnd() {
		// Debug.Log ("Particle End");
	}

	private void KillTargets() {
		TouchTargets (Sources [0].transform.position, Sources [1].transform.position);
		TouchTargets (Sources [2].transform.position, Sources [1].transform.position);
		TouchTargets (Sources [2].transform.position, Sources [0].transform.position);
	}

	private void TouchTargets(Vector2 pos1, Vector2 pos2) {
		Vector2 diff_ = pos1 - pos2;
		Vector2 mid_ = (pos1 + pos2) / 2f;

		float a = diff_.x;
		float b = diff_.y;
		float c = -(a * mid_.x + b * mid_.y);
		EffectController.Instance.TryKillTargets (a, b, c);
	}
}
