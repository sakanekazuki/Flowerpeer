// 早送りボタンのインターフェース
public interface IFastForwardButton
{
    /// <summary>
    /// UVの状態を設定
    /// </summary>
    /// <param name="isMove">true = 移動する</param>
    void SetUVMove(bool isMove);
}