using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public WaveSource[] Sources;
	public float ClickSensitivity;

	private bool m_OnDrag = false;
	private int m_iMovingSourceIdx = -1;
	private Vector2 m_BeginPos;

	void Start() {
		EventTriggerListener.Get(transform.gameObject).onClick = OnMouseClick;
		EventTriggerListener.Get(transform.gameObject).onBeginDrag = OnMouseBeginDrag;
		EventTriggerListener.Get(transform.gameObject).onDrag = OnMouseDrag;
		EventTriggerListener.Get(transform.gameObject).onEndDrag = OnMouseEndDrag;
	}

	private void OnMouseClick (GameObject go) {
		if (m_OnDrag) {
			return;
		}

		Vector2 clickPos_;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_OSX
		clickPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		#else	
		clickPos_ = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
		#endif

		int idx_ = GrabAvaliableSourceIdx(clickPos_);
		if (idx_ != -1) {
			Sources [idx_].ChangeColor ();
		}
	}

	private void OnMouseBeginDrag (GameObject go) {
		m_OnDrag = true;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_OSX
		m_BeginPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		#else	
		m_BeginPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
		#endif

		m_iMovingSourceIdx = GrabAvaliableSourceIdx (m_BeginPos);
	}

	private void OnMouseDrag (GameObject go) {
		Vector2 clickPos_;

		#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_STANDALONE_OSX
		clickPos_ = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		#else	
		clickPos_ = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y));
		#endif

		if (m_iMovingSourceIdx != -1) {
			Sources [m_iMovingSourceIdx].transform.position = clickPos_;
		}
	}

	private void OnMouseEndDrag (GameObject go) {
		m_OnDrag = false;
		m_iMovingSourceIdx = -1;
	}

	private int GrabAvaliableSourceIdx (Vector2 pos) {
		int idx_ = -1;
		float minDis_ = 10000f;
		for (int i = 0; i < Sources.Length; i++) {
			Vector2 diff_ = Sources [i].transform.position;
			diff_ -= pos;
			float dis_ = diff_.magnitude;
			if (dis_ < minDis_) {
				idx_ = i;
				minDis_ = dis_;
			}
		}

		if (minDis_ < ClickSensitivity) {
			return idx_;
		} else {
			return -1;
		}
	}
}
