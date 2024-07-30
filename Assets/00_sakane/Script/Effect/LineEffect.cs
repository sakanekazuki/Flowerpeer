using UnityEngine;

// 線のエフェクト
public class LineEffect : MonoBehaviour
{
	ParticleSystem particle;

	private void Awake()
	{
		particle = GetComponent<ParticleSystem>();
	}

	private void Update()
	{
		particle.Simulate(t: Time.unscaledDeltaTime, withChildren: true, restart: false);
	}
}