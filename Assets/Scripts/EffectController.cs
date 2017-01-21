using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;

public class EffectController : Singleton<EffectController>
{
	private EffectCreater lineEffCreater = null;
	private EffectCreater targetEffCreater = null;

	public List<EffectBase> targetEffBases = new List<EffectBase>();

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

	public void ShowTargetEffect(Vector2 pos, Color color, int life)
	{
		var effect = targetEffCreater.ShowEffect(pos, color);

		effect.Init(5);

		targetEffBases.Add(effect);
	}

	public void SubTargetLifeTime()
	{
		List<EffectBase> removes = new List<EffectBase>();

		for(int i = 0; i < targetEffBases.Count; i++)
		{
			if(targetEffBases[i].CheckDieAndSubTime())
			{
				removes.Add(targetEffBases[i]);
			}
		}

		for(int i = 0; i < removes.Count; i++)
		{
			EndTarget(removes[i]);
		}
	}

	public void TryKillTargets(float a, float b, float c)
	{
		for(int i = 0; i < targetEffBases.Count; i++)
		{
			if(	!targetEffBases[i].IsKilling() &&
				CanKillTarget(a,b,c,targetEffBases[i]))
			{
				targetEffBases[i].Kill();
			}
		}
	}

	private bool CanKillTarget(float a, float b, float c, EffectBase target)
	{
		var x = target.transform.position.x;
		var y = target.transform.position.y;
		var child = Mathf.Abs((a * x) + (b * y) + c);
		var parent = Mathf.Sqrt((a*a) + (b*b));
		var d = child / parent;
		return d <= target.radius;
	}

	private void EndTarget(EffectBase effect)
	{
		if(targetEffBases.Contains(effect))
		{
			effect.particle.gameObject.SetActive(false);
			targetEffBases.Remove(effect);
		}
	}
}
