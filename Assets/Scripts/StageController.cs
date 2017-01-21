using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
	public List<StageData> stages = new List<StageData>();

	[HideInInspector] public StageData currentStage;
	[HideInInspector] public StageData nextStage;

	public StageData GetStage(int level)
	{
		int currentLv = level;
		for(int i = 0; i < stages.Count; i++)
		{
			if(null != currentStage && currentStage == stages[i]) break;

			if(stages[i].needLevel >= currentLv)
			{
				currentLv = stages[i].needLevel;
				currentStage = stages[i];
				if(i == stages.Count - 1)
				{
					nextStage = stages[i];
				}
				else
				{
					nextStage = stages[i + 1];
				}

				Debug.Log("currentStage: " + currentStage.needLevel);
				Debug.Log("nextStage: " + nextStage.needLevel);
				break;
			}
		}
		return currentStage;
	}

	public bool CanCreateNewStage(int level)
	{
		if(null == nextStage)
		{	
			return true;
		}
		return (level >= nextStage.needLevel);
	}

	public bool CanCreateNewTargetBy(int rountCnt)
	{
		if(null == currentStage)
		{
			return true;
		}

		return rountCnt >= currentStage.createRoundCnt;
	}

	[System.Serializable]
	public class StageData
	{
		public int needLevel;
		public int createRoundCnt;

		public int targetAmount;
		public int lifeMin;
		public int lifeMax;
	}
}
