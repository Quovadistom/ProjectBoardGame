using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Realtime;

namespace ProjectTM.Networkables
{
    public class XRGrabNetworkable : XRGrabInteractable
    {
        //private PhotonView photonView;
        private XRBaseInteractor currentInteractor;
        

        void Start()
        {
            //photonView = PhotonView.Get(this);
        }

        protected override void OnSelectEntering(SelectEnterEventArgs args)
        {
            base.OnSelectEntering(args);

            currentInteractor = args.interactor;

            //if (!photonView.IsMine)
            //{
            //    photonView.RequestOwnership();                
            //}

            if (gameObject.layer != 8)
                gameObject.layer = 6;
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            if (gameObject.layer != 8)
                gameObject.layer = 0;
        }

        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            if (args.interactor == currentInteractor && isSelected)
            {
                interactionManager.CancelInteractableSelection(this);
            }   
        }
    }
}
