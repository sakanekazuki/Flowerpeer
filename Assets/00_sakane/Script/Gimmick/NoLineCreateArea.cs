using UnityEngine;

// ü‚ğ•`‚¯‚È‚¢ƒGƒŠƒA
public class NoLineCreateArea : MonoBehaviour
{
	public void Hovered()
	{
		Player.player.GetComponent<IPlayer>().DefaultCursor();
		Player.player.GetComponent<IPlayer>().LineCreateForcedTermination();
	}

	public void UnHovered()
	{
		Player.player.GetComponent<IPlayer>().MagicCursor();
	}
}