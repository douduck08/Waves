using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSource : MonoBehaviour {

	public Transform WaveArchor;
	public GameObject WavePrefab;
	public Color[] Colors;
	public int CurrentColorIdx;

	private SpriteRenderer img;

	void Awake() {
		img = this.GetComponent<SpriteRenderer> ();
		img.color = Colors [CurrentColorIdx];
	}

	public void ChangeColor() {
		CurrentColorIdx = (CurrentColorIdx + 1) % Colors.Length;
		img.color = Colors [CurrentColorIdx];
	}

	public void Pulse(float speed) {
		GameObject go_ = GameObject.Instantiate<GameObject> (WavePrefab, WaveArchor);
		go_.transform.position = this.transform.position;
		go_.GetComponent<CircleRender> ().PlayPulse (Colors [CurrentColorIdx], speed);
	}
}
