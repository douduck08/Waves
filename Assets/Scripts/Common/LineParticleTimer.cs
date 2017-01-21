using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LineParticleTimer : MonoBehaviour {

	public Vector2 UpperBond;
	public Vector2 LowerBond;

	private bool m_Playing;
	private float m_Timer;
	private Action m_EndingCallback;

	private float m_tZero;
	private float m_speedFactor;
	private float m_disFactor;
	private Vector2 m_MidPoint;
	private Vector2 m_Direct;

	void Start () {
		m_Playing = false;
	}
	
	void Update () {
		if (m_Playing) {
			m_Timer += Time.deltaTime;
			if (m_Timer >= m_tZero) {
				Vector2 pos1_ = m_MidPoint + m_Direct * Mathf.Sqrt (m_speedFactor * m_Timer * m_Timer);
				// PlayParticleAt(pos1_);
				Vector2 pos2_ = m_MidPoint - m_Direct * Mathf.Sqrt (m_speedFactor * m_Timer * m_Timer);
				// PlayParticleAt(pos2_);

				if (CheckVound (pos1_, pos2_)) {
					m_Playing = false;
					m_EndingCallback ();
				}
			}
		}
	}

	public void SetPlayerEndCallback (Action callback) {
		m_EndingCallback = callback;
	}

	public void PlayLineParticle(float speed, float distant, Vector2 midPoint, Vector2 direct) {
		m_tZero = distant / speed;
		m_speedFactor = speed * speed;
		m_disFactor = distant * distant / 4f;
		m_MidPoint = midPoint;
		m_Direct = direct;

		m_Playing = true;
		m_Timer = 0;
	}

	private bool CheckVound(Vector2 pos1, Vector2 pos2) {
		return false;
	}
}
