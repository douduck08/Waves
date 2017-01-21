using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCaller : MonoBehaviour {

	public WaveSource[] Waves;
	public Transform WaveArchor;
	public GameObject LinePrefab;

	public void CallNewLine(float speed) {
		GameObject go_ = GameObject.Instantiate<GameObject> (LinePrefab, WaveArchor);
		go_.transform.position = this.transform.position;

		int mixColor_ = Config.MixColor (Waves [0].CurrentColorIdx, Waves [1].CurrentColorIdx);
		go_.GetComponent<LineRender> ().PlayLineEffect (mixColor_, speed, Waves [0].transform.position, Waves [1].transform.position);
	}
}
