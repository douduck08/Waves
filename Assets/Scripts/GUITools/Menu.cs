using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public MainController mainController;
	public Text Score;
	public Text TopScore;

	void OnEnable () {
		Score.text = string.Format ("LAST SCORE : {0}", mainController.playerInfo.killCnt);
		TopScore.text = string.Format ("TOP SCORE : {0}", mainController.playerInfo.maxKill);
	}

	public void StartGame() {
		this.gameObject.SetActive (false);
		mainController.StartGame ();;
	}
}
