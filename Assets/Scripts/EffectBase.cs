using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
	public TextMesh lifeText;
	public ParticleSystem particle;

	void Awake()
	{
		
	}

	public void SetLife(int life)
	{
		if(null == this.lifeText) return;
		this.lifeText.text = life.ToString();
	}
}
