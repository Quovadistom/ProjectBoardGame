using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

namespace ProjectTM.Networkables
{
    public class TeleportManager : MonoBehaviour
    {
        public ActionBasedController ActionBasedControllerBase;
        public XRDirectInteractor XRDirectInteractorBase;
        public ActionBasedController ActionBasedControllerRay;
        public XRRayInteractor XRRayInteractorRay;
        public GameObject Reticle, XRRig;

        public InputActionReference TeleportActivationReference;

        private XRInteractorLineVisual lineRenderer;
        private static bool isTeleporting = false;

        private void Start()
        {
            lineRenderer = XRRayInteractorRay.gameObject.GetComponent<XRInteractorLineVisual>();
            TeleportActivationReference.action.performed += TeleportModeActivate;
            TeleportActivationReference.action.canceled += TeleportModeCancel;
            Reticle.SetActive(false);
        }

        private void OnDestroy()
        {
            TeleportActivationReference.action.performed -= TeleportModeActivate;
            TeleportActivationReference.action.canceled -= TeleportModeCancel;
        }

        private void TeleportModeCancel(InputAction.CallbackContext obj)
        {
            lineRenderer.enabled = false;
            if (XRRayInteractorRay.enabled)
            {
                Invoke("DeactivateTeleporter", 0.00001f);
            }
            Reticle.SetActive(false);
        }

        void DeactivateTeleporter() => ChangeTeleportRay(true);

        private void TeleportModeActivate(InputAction.CallbackContext obj)
        {
            if (!isTeleporting)
            {
                ChangeTeleportRay(false);
                lineRenderer.enabled = true;
            }
        }

        private void ChangeTeleportRay(bool teleporting)
        {
            isTeleporting = !teleporting;
            ActionBasedControllerBase.enableInputActions = teleporting;
            //XRDirectInteractorBase.enabled = teleporting;
            ActionBasedControllerRay.enableInputActions = !teleporting;
            XRRayInteractorRay.enabled = !teleporting;
            Reticle.SetActive(!teleporting);
        }
    }
}
