using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
	public List<StageData> stages = new List<StageData>();

	void Awake()
	{
		stages.Sort((t1, t2) =>
			{
				return -t1.killCnt.CompareTo(t2.killCnt);
			});
	}

	public StageData GetStage(int killCnt)
	{
		StageData data = null;
		for(int i = 0; i < stages.Count; i++)
		{
			if(stages[i].killCnt <= killCnt)
			{
				data = stages[i];

				Debug.Log("currentStage: " + data.killCnt);
				break;
			}
		}
		return data;
	}

	public bool CanCreateNewTargetBy(int rountCnt)
	{
		// TODO:
		return true;
	}

	[System.Serializable]
	public class StageData
	{
		public int killCnt;
		public int createRoundCnt;

		public int targetAmount;
		public int lifeMin;
		public int lifeMax;
	}
}
