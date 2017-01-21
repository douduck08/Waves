using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSource : MonoBehaviour {

	public Transform WaveArchor;
	public GameObject WavePrefab;
	public int CurrentColorIdx;

	private SpriteRenderer img;

	void Start() {
		img = this.GetComponent<SpriteRenderer> ();
		img.color = Config.ColorPool [CurrentColorIdx];
	}

	public void ChangeColor() {
		CurrentColorIdx = (CurrentColorIdx + 1) % 3;
		img.color = Config.ColorPool [CurrentColorIdx];
	}

	public void Pulse(float speed) {
		GameObject go_ = GameObject.Instantiate<GameObject> (WavePrefab, WaveArchor);
		go_.transform.position = this.transform.position;
		go_.GetComponent<CircleRender> ().PlayPulse (Config.ColorPool [CurrentColorIdx], speed);
	}
}
