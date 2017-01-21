using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LineParticleTimer : MonoBehaviour {

	public Vector2 UpperBound;
	public Vector2 LowerBound;

	private bool m_Playing;
	private float m_Timer;
	private Action m_EndingCallback;

	private float m_tZero;
	private float m_speed;
	private float m_speedFactor;
	private float m_disFactor;
	private Vector2 m_MidPoint;
	private Vector2 m_Direct;
	private int m_colorIdx;

	void Start () {
		m_Playing = false;
	}
	
	void Update () {
		if (m_Playing) {
			if (m_Timer >= m_tZero) {
				Vector2 pos1_ = m_MidPoint + m_Direct * m_speed * Mathf.Sqrt (m_Timer * m_Timer - m_tZero * m_tZero);
				EffectController.Instance.ShowLineEffect(pos1_, m_colorIdx);
				Vector2 pos2_ = m_MidPoint - m_Direct * Mathf.Sqrt (m_speedFactor * m_Timer * m_Timer - m_disFactor);
				EffectController.Instance.ShowLineEffect(pos2_, m_colorIdx);

				if (CheckBound (pos1_, pos2_)) {
					m_Playing = false;
					m_EndingCallback ();
				}
			}
			m_Timer += Time.deltaTime;
		}
	}

	public void SetPlayerEndCallback (Action callback) {
		m_EndingCallback = callback;
	}

	public void PlayLineParticle(float speed, float distant, Vector2 midPoint, Vector2 direct, int colorIdx) {
		m_tZero = distant / speed;
		m_speed = speed;
		m_speedFactor = speed * speed;
		m_disFactor = distant * distant;
		m_MidPoint = midPoint;
		m_Direct = direct / 2;
		m_colorIdx = colorIdx;

		m_Playing = true;
		m_Timer = 0;
	}

	private bool CheckBound(Vector2 pos1, Vector2 pos2) {
		if (pos1.x > LowerBound.x && pos1.x < UpperBound.x && pos1.y > LowerBound.y && pos1.y < UpperBound.y) {
			return false;
		} else if (pos2.x > LowerBound.x && pos2.x < UpperBound.x && pos2.y > LowerBound.y && pos2.y < UpperBound.y) {
			return false;
		} else {
			return true;
		}
	}
}
