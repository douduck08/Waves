using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
	public int lifeTime;
	public TextMesh lifeText;
	public ParticleSystem particle;

	public void Init(int life)
	{
		lifeTime = life;
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
}
