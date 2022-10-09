public class GameBoardContext
{
    public IGameBoardState GameBoardState { get; set; }

    public GameBoardContext()
    {
        GameBoardState = new TableTopState();
    }

    public void SetTransitioning()
    {
        GameBoardState.SetTransitioning(this);
    }

    public void SetTableTop()
    {
        GameBoardState.SetTableTop(this);
    }

    public void SetTile()
    {
        GameBoardState.SetTile(this);
    }
}