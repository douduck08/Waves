using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;
using System;

public class EffectController : Singleton<EffectController>
{
	private EffectCreater _lineEffCreater = null;

	private EffectCreater lineEffCreater
	{
		get
		{
			if(null == _lineEffCreater)
			{
				var lineEff = Resources.Load("Prefabs/LineEffectCreater") as GameObject;
				_lineEffCreater = TSUtil.Instantiate(lineEff, transform).GetComponent<EffectCreater>();
			}
			return _lineEffCreater;
		}
	}

	private EffectCreater _targetEffCreater = null;

	private EffectCreater targetEffCreater
	{
		get
		{
			if(null == _targetEffCreater)
			{
				var targetEff = Resources.Load("Prefabs/TargetEffectCreater") as GameObject;
				_targetEffCreater = TSUtil.Instantiate(targetEff, transform).GetComponent<EffectCreater>();
			}
			return _targetEffCreater;
		}
	}

	public List<EffectBase> targetEffBases = new List<EffectBase>();
	public Dictionary<LineParticleTimer,EffectBase[]> lineEffBases = new Dictionary<LineParticleTimer,EffectBase[]>();

	private Action<int> onKillTarget;

	public void SetKillTargetCallback(Action<int> callback)
	{
		onKillTarget = callback;
	}

	public int GetCurrentTargetCnt()
	{
		return targetEffBases.Count;
	}

	public void ShowLineEffect(Vector2 pos, int colorIdx, LineParticleTimer lineTimer, int lineIdx)
	{
		onKillTarget(TryKillTargets(pos, colorIdx));

		var color = Config.ColorPool[colorIdx];

		EffectBase effect = null;
		EffectBase[] lineEffs = null;
		if(!lineEffBases.TryGetValue(lineTimer, out lineEffs))
		{
			effect = lineEffCreater.ShowEffect(pos, color);
			lineEffs = new EffectBase[2];
			lineEffs[lineIdx] = effect;
			lineEffBases.Add(lineTimer, lineEffs);
		}

		effect = lineEffs[lineIdx];
		if(null == effect)
		{
			effect = lineEffCreater.ShowEffect(pos, color);
			lineEffs[lineIdx] = effect;
		}

		effect.SetTrailColor(color);
		effect.transform.position = pos;

//		var effect = lineEffCreater.ShowEffect(pos, color);
//		StartCoroutine(
//			TSUtil.WaitForSeconds(effect.particle.main.duration + 0.5f, () =>
//				{
//					effect.particle.Stop();
//					effect.gameObject.SetActive(false);
//				}));
	}

	public void ShowTargetEffect(Vector2 pos, int colorIdx, int life)
	{
		var color = Config.ColorPool[colorIdx];
		var effect = targetEffCreater.ShowEffect(pos, color);

		effect.Init(life, colorIdx);

		targetEffBases.Add(effect);
	}

	public void SubTargetLifeTime(Action<int> callback = null)
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
			EndTarget(removes[i], true);
		}

		if(null != callback)
		{
			callback(removes.Count);
		}
	}

	public void TryKillTargets(float a, float b, float c, int colorIdx, Action<int> callback = null)
	{
		List<EffectBase> removes = new List<EffectBase>();

		for(int i = 0; i < targetEffBases.Count; i++)
		{
			if(targetEffBases[i].CheckColorIdx(colorIdx) &&
			   !targetEffBases[i].IsKilling() &&
			   CanKillTarget(a, b, c, targetEffBases[i]))
			{
				Debug.Log("Kill Target: " + targetEffBases[i].name, targetEffBases[i].gameObject);
				removes.Add(targetEffBases[i]);
			}
		}

		for(int i = 0; i < removes.Count; i++)
		{
			EndTarget(removes[i], false);
		}

		if(null != callback)
		{
			callback(removes.Count);
		}
	}

	private bool CanKillTarget(float a, float b, float c, EffectBase target)
	{
		var x = target.transform.position.x;
		var y = target.transform.position.y;
		var child = Mathf.Abs((a * x) + (b * y) + c);
		var parent = Mathf.Sqrt((a * a) + (b * b));
		var d = child / parent;
		return d <= target.radius;
	}

	private int TryKillTargets(Vector2 pos, int colorIdx)
	{
		List<EffectBase> removes = new List<EffectBase>();

		for(int i = 0; i < targetEffBases.Count; i++)
		{
			if(targetEffBases[i].CheckColorIdx(colorIdx) &&
			   !targetEffBases[i].IsKilling() &&
			   CanKillTarget(pos, targetEffBases[i]))
			{
				Debug.Log("Kill Target By Pos: " + targetEffBases[i].name, targetEffBases[i].gameObject);
				removes.Add(targetEffBases[i]);
			}
		}

		for(int i = 0; i < removes.Count; i++)
		{
			EndTarget(removes[i], false);
		}

		return removes.Count;
	}

	private bool CanKillTarget(Vector2 pos, EffectBase target)
	{
		var collders = Physics2D.OverlapCircleAll(pos, target.radius);
		for(int i = 0; i < collders.Length; i++)
		{
			if(collders[i].gameObject.Equals(target.gameObject))
			{
				return true;
			}
		}
		return false;
	}

	private bool EndTarget(EffectBase effect, bool isTimeout)
	{
		if(targetEffBases.Contains(effect))
		{
			effect.Kill(isTimeout, () =>
				{
					targetEffBases.Remove(effect);
				});
			return true;
		}
		return false;
	}
}
