using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRender : MonoBehaviour {

	public int PointNumber;
	public int Radius;

	private LineRenderer line;
	private float m_timer;

	void Start () {
		line = gameObject.GetComponent<LineRenderer>();

		line.useWorldSpace = false;
		CreatePoints (Radius);
		m_timer = 0;
	}

	void Update () {
		if (m_timer < 0.03) {
			m_timer += Time.deltaTime;
		} else {
			Radius += 1;
			CreatePoints (Radius);
			m_timer = 0;
		}
	}


	private void CreatePoints (int radius) {
		float x;
		float y;
		float z = 0f;

		float angle = 0f;
		line.SetVertexCount (PointNumber + 1);
		for (int i = 0; i < (PointNumber + 1); i++) {
			x = Mathf.Sin (Mathf.Deg2Rad * angle) * radius;
			y = Mathf.Cos (Mathf.Deg2Rad * angle) * radius;

			line.SetPosition (i, new Vector3(x,y,z) );

			angle += (360f / PointNumber);
		}
	}
}
