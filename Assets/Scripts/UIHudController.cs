using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHudController : MonoBehaviour
{
	public Text killText;

	public GameObject[] lifeObjs;

	public void SetLife(int life)
	{
		if(life > lifeObjs.Length)
		{
			life = lifeObjs.Length;
		}
		else if(life < 0)
		{
			life = 0;
		}

		for(int i = 0; i < lifeObjs.Length; i++)
		{
			if( i < life)
			{
				lifeObjs[i].SetActive(true);
			}
			else
			{
				lifeObjs[i].SetActive(false);
			}
		}
	}

	public void SetKill(int kill)
	{
		killText.text = kill.ToString();
	}
}
