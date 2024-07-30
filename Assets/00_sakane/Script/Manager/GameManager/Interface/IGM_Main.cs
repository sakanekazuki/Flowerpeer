using System.Collections.Generic;
using UnityEngine;

// ゲームマネージャーのインターフェース
public interface IGM_Main
{
	/// <summary>
	/// 初期化
	/// </summary>
	void Init();

	/// <summary>
	/// 終了
	/// </summary>
	void Final();

	/// <summary>
	/// ゴール
	/// </summary>
	/// <param name="goalCharacter">ゴールしたキャラクターオブジェクト</param>
	void CharacterGoal(GameObject goalCharacter);

	/// <summary>
	/// ゲームオーバー
	/// </summary>
	void GameOver();

	/// <summary>
	/// 次のステージに移動
	/// </summary>
	void NextStage();

	/// <summary>
	/// オプション画面
	/// </summary>
	void Option();

	/// <summary>
	/// メインゲームに戻る
	/// </summary>
	void ToGame();

	/// <summary>
	/// メインメニュー
	/// </summary>
	void MainMenu();

	/// <summary>
	/// ゲームクリア
	/// </summary>
	void GameClear();

	/// <summary>
	/// インスタンス化したキャラクター取得
	/// </summary>
	/// <returns>インスタンス化したキャラクター</returns>
	List<GameObject> GetInstanceCharacterObjects();

	/// <summary>
	/// 線の長さ設定
	/// </summary>
	/// <param name="lineMagnitude">変更後の線の長さ</param>
	void SetLineMagnitude(float lineMagnitude);

	/// <summary>
	/// ゲーム停止
	/// </summary>
	void GameStop();

	/// <summary>
	/// ゲームリスタート
	/// </summary>
	void GameReStart();

	/// <summary>
	/// ゲーム早送り
	/// </summary>
	void FastForward();

	/// <summary>
	/// 早送り状態取得
	/// </summary>
	/// <returns>早送り状態</returns>
	bool GetIsFastForward();

	/// <summary>
	/// キャラクターが鍵を取得
	/// </summary>
	/// <param name="number">個数</param>
	/// <param name="keyType">種類</param>
	void CharacterGetKey(int number, GameInstance.KeyType keyType);
}