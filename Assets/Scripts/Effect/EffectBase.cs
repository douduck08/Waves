using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
	public float radius;
	public int lifeTime;
	public TextMesh lifeText;
	public ParticleSystem particle;
	private bool isKilling = false;
	private int colorIdx;
	public LineRenderer lineRen;

	public void Init(int life, int colorIdx)
	{
		lifeTime = life;
		isKilling = false;
		this.colorIdx = colorIdx;
		SetLife(life);
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

	public void Kill()
	{
		if(isKilling) return;
		isKilling = true;
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
	}

	[ContextMenu("ResetLineRenSortingLaye")]
	public void ResetLineRenSortingLaye()
	{
		lineRen.sortingLayerName = "LineRen";
	}
}
