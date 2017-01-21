using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;
using TeamSignal.Utilities.ObjectPools;
using System;

public class EffectCreater : MonoBehaviour
{
	public ObjectPool objPool;

	public EffectBase ShowEffect(Vector2 pos, Color color)
	{
		var effect = objPool.GetPoolObject<EffectBase>();
		effect.gameObject.SetActive(true);

		effect.transform.position = new Vector3(pos.x, pos.y);
		var gradient = new ParticleSystem.MinMaxGradient(color);
		gradient.color = color;

		var colorOverLifetime = effect.particle.colorOverLifetime;
		colorOverLifetime.color = color;
		effect.particle.Play();

		return effect;
	}
}
