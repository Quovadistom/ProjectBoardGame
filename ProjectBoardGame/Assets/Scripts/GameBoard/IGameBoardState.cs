public interface IGameBoardState
{
    public bool CanTeleport { get; }

    public void SetTransitioning(GameBoardContext gameBoardContext);

    public void SetTableTop(GameBoardContext gameBoardContext);

    public void SetTile(GameBoardContext gameBoardContext);
}