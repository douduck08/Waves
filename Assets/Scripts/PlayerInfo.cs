using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
	public int killCnt = 0;

	public int maxKill = 0;

	public int life = 3;

	public void ResetGameInfo()
	{
		killCnt = 0;
		life = 3;
	}

	public void ResetMaxKill()
	{
		if(maxKill < killCnt)
		{
			maxKill = killCnt;
		}
	}
}
