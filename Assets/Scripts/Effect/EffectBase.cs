using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TeamSignal.Utilities;
using DG.Tweening;

public class EffectBase : MonoBehaviour
{
	public float radius;
	public int lifeTime;
	public TextMesh lifeText;
	public ParticleSystem particle;
	private bool isKilling = false;
	private bool isTimeouting = false;
	private int colorIdx;
	public LineRenderer lineRen;
	public CircleCollider2D circle;

	public AnimationCurve killCurve;
	public AnimationCurve timeoutCurve;

	public void Init(int life, int colorIdx)
	{
		transform.localScale = Vector3.one;
		lifeTime = life;
		isKilling = false;
		isTimeouting = false;
		this.colorIdx = colorIdx;
		var color = Config.ColorPool[colorIdx];
		this.lifeText.color = color;
		lineRen.SetColors(color, Color.white);
		SetLife(life);

		circle.radius = radius;
	}

	public void SetLife(int life)
	{
		if(null == this.lifeText) return;
		this.lifeText.text = life.ToString();
	}

	public bool CheckDieAndSubTime()
	{
		lifeTime -= 1;
		SetLife(lifeTime);
		return (lifeTime <= 0);
	}

	public bool IsKilling()
	{
		return isKilling;
	}

	public void Kill(bool isTimeout, Action callback)
	{
		if(isTimeout)
		{
			if(isTimeouting) return;
			isTimeouting = true;

			transform.DOScale(Vector3.zero, 3f)
				.SetEase(timeoutCurve)
				.OnComplete(() =>
				{
					gameObject.SetActive(false);
					callback();
				});
	
		}
		else
		{
			if(isKilling) return;
			isKilling = true;

			DOTween.Kill(transform);
			transform.DOScale(Vector3.zero, 0.5f)
				.SetEase(killCurve)
				.OnComplete(() =>
				{
					gameObject.SetActive(false);
					callback();
				});
		}
	}

	public bool CheckColorIdx(int idx)
	{
		return colorIdx == idx;
	}

	[ContextMenu("ResetTextSortingLaye")]
	public void ResetTextSortingLaye()
	{
		var ren = lifeText.GetComponent<Renderer>();
		ren.sortingLayerName = "TextMesh";
		ren.sortingOrder = 0;
	}

	[ContextMenu("ResetLineRenSortingLaye")]
	public void ResetLineRenSortingLaye()
	{
		lineRen.sortingLayerName = "LineRen";
	}
}
