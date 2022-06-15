using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class TileInteractor : MonoBehaviour
{
    public XRSocketInteractor SocketInteractor;
    private GameBoard m_gameBoard;
    private Tile m_tile;
    private bool m_isOccupied;
    private Tween m_tween;

    public void Awake()
    {
        SocketInteractor.selectEntered.AddListener(OnSelectEntered);
        SocketInteractor.selectExited.AddListener(OnSelectExited);
        m_gameBoard = GetComponentInParent<GameBoard>();
        m_gameBoard.DisableInteractors += ToggleSocketInteractor;
        m_tile = GetComponentInParent<Tile>();
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        m_isOccupied = true;
        m_gameBoard.DisableAllInteractors();
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {
        m_isOccupied = false;
        foreach (Tile tile in m_tile.NeighboringTiles)
        {
            TileInteractor helper = tile.GetComponentInChildren<TileInteractor>();
            helper.ToggleSocketInteractor(true);
        }
    }

    public void ToggleSocketInteractor(bool toggle)
    {
        m_tween.Kill();

        Vector3 scaleVector = toggle ? new Vector3(0.03f, 0.03f, 0.03f) : Vector3.zero;
        m_tween = SocketInteractor.gameObject.transform.DOScale(scaleVector, 0.2f);

        if (!toggle)
        {
            if (!m_isOccupied)
                m_tween.onComplete += () => SocketInteractor.gameObject.SetActive(false);
        }
        else
        {
            SocketInteractor.gameObject.SetActive(true);
        }
    }
}
