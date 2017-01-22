using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour {

	public Color[] ColorSetting;

	public WaveSource[] Sources;
	public float RoundTime = 6f;
	public float WaveSpeed = 1f;
	public Vector2 TargetBound;
	public GameObject LineParticleTimerPrefab;

	private float m_timer;
	private LineParticleTimer[] m_LineParticleTimers;

	public AudioSource bgm;
	private bool playBgm = false;

	public StageController stageCtrl;
	public PlayerInfo playerInfo = new PlayerInfo();
	[SerializeField] private int roundCnt;

	void Awake() {
		for (int i = 0; i < 6; i++) {
			Config.ColorPool [i] = ColorSetting [i];
		}
		this.enabled = false;
		EffectController.Instance.SetKillTargetCallback(OnKillCnt);
	}

	void Start () {
		m_timer = 0f;
		InitialLineParticleTimers ();

		TryNextStage();
	}

	[ContextMenu("Create")]
	private void Test()
	{
		GenerateTarget (3, 10);
	}

	public void StartGame () {
		this.enabled = true;
	}

	public void GameOver () {
		this.enabled = false;
		playBgm = false;
		bgm.Stop ();
	}
	
	void Update () {
		if(!playBgm)
		{
			playBgm = true;
			bgm.Play();
		}

		if (m_timer < RoundTime) {
			m_timer += Time.deltaTime;
		} else {
			PlayAllPulse ();
			PlayAllLineParticle ();
			CountTargetLife ();
			TryNextStage();
			m_timer = 0;
		}
	}

	private void TryNextStage()
	{
		roundCnt++;

		if(!stageCtrl.CanCreateNewTargetBy(roundCnt)) return;

		roundCnt = 0;
		var stage = stageCtrl.GetStage(playerInfo.killCnt);
		var life = Random.Range(stage.lifeMin, stage.lifeMax);
		Debug.Log("life " + life);
		GenerateTarget(stage.targetAmount, life);
	}

	private void OnKillCnt(int killCnt)
	{
		playerInfo.killCnt += killCnt;
	}

	private void InitialLineParticleTimers() {
		m_LineParticleTimers = new LineParticleTimer[3];
		for (int i = 0; i < 3; i++) {
			GameObject go_ = GameObject.Instantiate<GameObject> (LineParticleTimerPrefab, transform);
			m_LineParticleTimers [i] = go_.GetComponent<LineParticleTimer> ();
			m_LineParticleTimers [i].SetPlayerEndCallback (KillTargets);
		}
	}

	private void GenerateTarget(int number, int life) {
		for (int i = 0; i < number; i++) {
			float x_ = Random.Range (-TargetBound.x, TargetBound.x);
			float y_ = Random.Range (-TargetBound.y, TargetBound.y);
			int color_ = Random.Range (3, 6);
			EffectController.Instance.ShowTargetEffect (new Vector2 (x_, y_), color_, life);
		}
	}
	private void PlayAllPulse() {
		for (int i = 0; i < Sources.Length; i++) {
			Sources [i].Pulse (WaveSpeed);
		}
	}

	private void PlayAllLineParticle() {
		PlayLineParticle (0, Sources [0].transform.position, Sources [1].transform.position, Config.MixColor(Sources [0].CurrentColorIdx, Sources [1].CurrentColorIdx));
		PlayLineParticle (1, Sources [2].transform.position, Sources [1].transform.position, Config.MixColor(Sources [2].CurrentColorIdx, Sources [1].CurrentColorIdx));
		PlayLineParticle (2, Sources [2].transform.position, Sources [0].transform.position, Config.MixColor(Sources [2].CurrentColorIdx, Sources [0].CurrentColorIdx));
	}

	private void PlayLineParticle(int idx, Vector2 pos1, Vector2 pos2, int colorIdx) {
		Vector2 diff_ = pos1 - pos2;
		Vector2 normal_ = new Vector2 (-diff_.y, diff_.x);
		m_LineParticleTimers [idx].PlayLineParticle (WaveSpeed, (diff_.magnitude) / 2f, (pos1 + pos2) / 2f, normal_.normalized, colorIdx);
	}

	private void LinParticleEnd() {
		Debug.Log ("Particle End");
	}

	private void KillTargets() {
		TouchTargets (Sources [0].transform.position, Sources [1].transform.position, Config.MixColor(Sources [0].CurrentColorIdx, Sources [1].CurrentColorIdx));
		TouchTargets (Sources [2].transform.position, Sources [1].transform.position, Config.MixColor(Sources [2].CurrentColorIdx, Sources [1].CurrentColorIdx));
		TouchTargets (Sources [2].transform.position, Sources [0].transform.position, Config.MixColor(Sources [2].CurrentColorIdx, Sources [0].CurrentColorIdx));
	}

	private void TouchTargets(Vector2 pos1, Vector2 pos2, int colorIdx) {
		Vector2 diff_ = pos1 - pos2;
		Vector2 mid_ = (pos1 + pos2) / 2f;

		float a = diff_.x;
		float b = diff_.y;
		float c = -(a * mid_.x + b * mid_.y);
		EffectController.Instance.TryKillTargets (a, b, c, colorIdx);
	}

	private void CountTargetLife() {
		EffectController.Instance.SubTargetLifeTime ();
	}
}
