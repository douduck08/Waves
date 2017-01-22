using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
	public int killCnt = 0;

	private int m_maxKill = 0;
	public int maxKill {
		get {
			if (m_maxKill < killCnt) {
				m_maxKill = killCnt;
			}
			return m_maxKill;
		}
	}

	public int life = 3;
}
