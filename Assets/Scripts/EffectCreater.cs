using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamSignal.Utilities;
using TeamSignal.Utilities.ObjectPools;
using DG.Tweening;

public class EffectCreater : MonoBehaviour
{
	public ObjectPool objPool;

//	ParticleSystem myParticleSystem;
	ParticleSystem.ColorOverLifetimeModule colorModule;

	public void ShowLineEffect(Vector2 pos, Color color)
	{
		var effect = objPool.GetPoolObject<ParticleSystem>();
		effect.gameObject.SetActive(true);

		effect.transform.position = new Vector3(pos.x, pos.y);
		var gradient = new ParticleSystem.MinMaxGradient(color);
		gradient.color = color;

		var colorOverLifetime = effect.colorOverLifetime;
		colorOverLifetime.color = color;
		effect.Play();

		StartCoroutine(
			TSUtil.WaitForSeconds(effect.main.duration + 0.5f, () =>
				{
					effect.Stop();
					effect.gameObject.SetActive(false);
				}));
	}
}
