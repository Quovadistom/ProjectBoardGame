using UnityEngine;

public class TableTopState : IGameBoardState
{
    public bool CanTeleport => false;

    public void SetTableTop(GameBoardContext gameBoardContext)
    {
        return;
    }

    public void SetTile(GameBoardContext gameBoardContext)
    {
        gameBoardContext.GameBoardState = new TileState();
    }

    public void SetTransitioning(GameBoardContext gameBoardContext)
    {
        gameBoardContext.GameBoardState = new TransitionState();
    }
}
