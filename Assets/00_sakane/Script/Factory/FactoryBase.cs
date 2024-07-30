using UnityEngine;

// 生成するクラスのベース
public abstract class FactoryBase<T> : Singleton<T> where T : Singleton<T>
{
	// 生成するオブジェクト
	[SerializeField]
	GameObject prefab;

	/// <summary>
	/// オブジェクト生成
	/// </summary>
	/// <param name="position">生成位置</param>
	/// <returns>生成したオブジェクト</returns>
	public GameObject Create(Vector3 position)
	{
		return GameObject.Instantiate(prefab, position, Quaternion.identity);
	}
}