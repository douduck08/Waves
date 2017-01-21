using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;

public class EffectController : Singleton<EffectController>
{
	private EffectCreater lineEffCreater = null;
	private EffectCreater targetEffCreater = null;

	public List<EffectBase> targetInfos = new List<EffectBase>();

	void Awake()
	{
		var lineEff = Resources.Load("LineEffectCreater") as GameObject;
		lineEffCreater = TSUtil.Instantiate(lineEff, transform).GetComponent<EffectCreater>();

		var targetEff = Resources.Load("TargetEffectCreater") as GameObject;
		targetEffCreater = TSUtil.Instantiate(targetEff, transform).GetComponent<EffectCreater>();
	}

	public void ShowLineEffect(Vector2 pos, Color color)
	{
		var effect = lineEffCreater.ShowEffect(pos, color);

		StartCoroutine(
			TSUtil.WaitForSeconds(effect.particle.main.duration + 0.5f, () =>
				{
					effect.particle.Stop();
					effect.gameObject.SetActive(false);
				}));
	}

	public void ShowTargetEffect(Vector2 pos, Color color)
	{
		var effect = targetEffCreater.ShowEffect(pos, color);

		effect.Init(5);

		targetInfos.Add(effect);
	}

	public void SubTargetLifeTime()
	{
		List<EffectBase> removes = new List<EffectBase>();

		for(int i = 0; i < targetInfos.Count; i++)
		{
			if(targetInfos[i].CheckDieAndSubTime())
			{
				removes.Add(targetInfos[i]);
			}
		}

		for(int i = 0; i < removes.Count; i++)
		{
			EndTarget(removes[i]);
		}
	}

	public void EndTarget(EffectBase effect)
	{
		if(targetInfos.Contains(effect))
		{
			// TODO: call main system
			effect.particle.gameObject.SetActive(false);
			targetInfos.Remove(effect);
		}
	}
}
