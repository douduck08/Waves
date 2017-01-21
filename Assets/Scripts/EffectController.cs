using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;

public class EffectController : Singleton<EffectController>
{
	private EffectCreater effCreater = null;

	void Awake()
	{
		var creater = Resources.Load("EffectCreater") as GameObject;

		effCreater = TSUtil.Instantiate(creater, transform).GetComponent<EffectCreater>();
	}

	public void ShowLineEffect(Vector2 pos, Color color)
	{
		effCreater.ShowLineEffect(pos, color);
	}
}
