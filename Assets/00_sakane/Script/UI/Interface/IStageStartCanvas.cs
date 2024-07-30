using Cysharp.Threading.Tasks;

// ステージ開始のインターフェース
public interface IStageStartCanvas
{
	/// <summary>
	/// ステージ開始
	/// </summary>
	//UniTask<bool> StageStart();
	void StageStart();
}