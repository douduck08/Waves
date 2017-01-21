using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRender : MonoBehaviour {

	public LineRenderer Render;
	public int PointNumber;
	public float Radius;

	private bool m_playing;
	private float m_timer;
	private float m_speed;

	void Start () {
		Render.useWorldSpace = true;
		Render.material = new Material(Shader.Find("Particles/Additive"));
	}

	void Update () {
		if (m_playing) {
			if (m_timer < 0.03) {
				m_timer += Time.deltaTime;
			} else {
				Radius += m_speed * Time.deltaTime;
				PrintCircle (Radius);
				m_timer = 0;
			}
		}

		if (Radius > 50) {
			DestroyObject (this.gameObject);
		}
	}


	private void PrintCircle (float radius) {
		float x;
		float y;
		float z = 0f;

		float angle = 0f;
		Render.SetVertexCount (PointNumber + 1);
		for (int i = 0; i < (PointNumber + 1); i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

			Vector3 pos_ = new Vector3 (x, y, z) + transform.position;
			Render.SetPosition (i,  pos_);

			angle += (360f / PointNumber);
		}
	}

	public void PlayPulse(Color color, float speed) {
		Radius = 0;
		m_speed = speed;
		m_playing = true;

		Render.SetColors(color, color);
	}
}
