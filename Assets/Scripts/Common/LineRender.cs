using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRender : MonoBehaviour {

	private bool m_playing;
	private float m_timer;
	private float m_totalTime;
	private float m_waveSpeed;

	private Vector2[] m_sourcePos = new Vector2[2];
	private int m_colorIdx;

	private Vector2 m_MidPositon;
	private Vector2 m_lineAB;
	private Vector2 m_normal;
	private float m_halfDis;
	private float m_tZero;

	void Update () {
		if (m_playing) {
			if (m_timer < 0.03) {
				m_timer += Time.deltaTime;
				m_totalTime += Time.deltaTime;
			} else {
				CallLineEffect ();
				m_timer = 0;
			}
		}

		if (m_totalTime > 100f / m_waveSpeed) {
			DestroyObject (this.gameObject);
		}
	}

	private void SetProperty() {
		m_MidPositon = (m_sourcePos [0] + m_sourcePos [1]) / 2f;
		m_lineAB = m_sourcePos [0] - m_sourcePos [1];
		m_halfDis = m_lineAB.magnitude;
		m_tZero = m_halfDis / m_waveSpeed;

		m_normal = new Vector2 (-m_lineAB.y, m_lineAB.x);
		m_normal = m_normal.normalized;
	}

	private void CallLineEffect() {
		if (m_totalTime < m_tZero) {
			return;
		}
		float factor_ = m_waveSpeed * Mathf.Sqrt (m_totalTime * m_totalTime - m_tZero * m_tZero);

		Vector2 pos1_ = m_MidPositon + m_normal * factor_;
		EffectController.Instance.ShowLineEffect(pos1_, m_colorIdx);
		Vector2 pos2_ = m_MidPositon - m_normal * factor_;
		EffectController.Instance.ShowLineEffect(pos2_, m_colorIdx);
	}

	public void PlayLineEffect(int colorIdx, float speed, Vector2 pos1, Vector2 pos2) {
		m_colorIdx = colorIdx;
		m_waveSpeed = speed;
		m_sourcePos [0] = pos1;
		m_sourcePos [1] = pos2;
		SetProperty ();

		m_playing = true;
		m_totalTime = 0f;
		m_timer = 0f;
	}
}
