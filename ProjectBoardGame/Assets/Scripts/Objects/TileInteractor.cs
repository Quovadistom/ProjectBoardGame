using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using DG.Tweening;

public class TileInteractor : MonoBehaviour
{
    private XRSocketInteractor m_socketInteractor;

    public void Awake()
    {
        m_socketInteractor = GetComponent<XRSocketInteractor>();
        m_socketInteractor.selectEntered.AddListener(OnSelectEntered);
        m_socketInteractor.selectExited.AddListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs arg0)
    {
        GetComponentInParent<Tile>().CreateActivity();
        GameObject.FindObjectOfType<GameBoard>().gameObject.SetActive(false);
    }

    private void OnSelectExited(SelectExitEventArgs arg0)
    {

    }
}
