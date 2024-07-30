using UnityEngine;

// ���̃G�t�F�N�g
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