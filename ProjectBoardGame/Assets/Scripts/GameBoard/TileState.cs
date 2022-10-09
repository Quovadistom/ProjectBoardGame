public class TileState : IGameBoardState
{
    public bool CanTeleport => true;

    public void SetTableTop(GameBoardContext gameBoardContext)
    {
        gameBoardContext.GameBoardState = new TableTopState();
    }

    public void SetTile(GameBoardContext gameBoardContext)
    {
        return;
    }

    public void SetTransitioning(GameBoardContext gameBoardContext)
    {
        gameBoardContext.GameBoardState = new TransitionState();
    }
}