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

	public void Init(int life)
	{
		lifeTime = life;
		isKilling = false;
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
}
