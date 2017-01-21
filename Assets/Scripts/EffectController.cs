﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;

public class EffectController : Singleton<EffectController>
{
	private EffectCreater lineEffCreater = null;
	private EffectCreater targetEffCreater = null;

	public List<TargetInfo> targetInfos = new List<TargetInfo>();

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

		var info = new TargetInfo(5, effect);

		targetInfos.Add(info);
	}

	public void SubTargetLifeTime()
	{
		List<TargetInfo> removes = new List<TargetInfo>();

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

	public void EndTarget(TargetInfo info)
	{
		if(targetInfos.Contains(info))
		{
			// TODO: call main system
			info.effectBase.GetComponent<ParticleSystem>().gameObject.SetActive(false);
			targetInfos.Remove(info);
		}
	}

	public class TargetInfo
	{
		public int lifeTime;
		public EffectBase effectBase;

		public TargetInfo(int lifeTime, EffectBase targetBase)
		{
			this.lifeTime = lifeTime;
			this.effectBase = targetBase;
		}

		public bool CheckDieAndSubTime()
		{
			lifeTime -= 1;
			effectBase.SetLife(lifeTime);
			return (lifeTime <= 0);
		}
	}
}
