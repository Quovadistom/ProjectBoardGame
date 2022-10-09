public class TransitionState : IGameBoardState
{
    public bool CanTeleport => false;

    public void SetTableTop(GameBoardContext gameBoardContext)
    {
        gameBoardContext.GameBoardState = new TableTopState();
    }

    public void SetTile(GameBoardContext gameBoardContext)
    {
        gameBoardContext.GameBoardState = new TileState();
    }

    public void SetTransitioning(GameBoardContext gameBoardContext)
    {
        return;
    }
}
