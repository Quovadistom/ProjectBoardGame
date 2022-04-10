using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectTM.Networkables
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(XRGrabNetworkable))]

    public class RigidbodyView : PhotonTransformViewAdapted
    {
        private Rigidbody body;
        public XRGrabNetworkable GrabInteractable { get; private set; }
        public PhotonView PhotonViewComponent;
        private Collider objectCollider;


        public override void Awake()
        {
            base.Awake();

            if (PhotonViewComponent == null)
                PhotonViewComponent = GetComponent<PhotonView>();

            this.body = GetComponent<Rigidbody>();
            GrabInteractable = GetComponent<XRGrabNetworkable>();
            objectCollider = GetComponent<Collider>();
            PhotonViewComponent.OwnershipTransfer = OwnershipOption.Request;
        }

        public override void Update()
        {
            var tr = transform;

            if (!PhotonViewComponent.IsMine)
            {
                body.isKinematic = true;

                if (m_UseLocal)
                {
                    tr.localPosition = Vector3.MoveTowards(tr.localPosition, this.m_NetworkPosition, this.m_Distance * (1.0f / PhotonNetwork.SerializationRate));
                    tr.localRotation = Quaternion.RotateTowards(tr.localRotation, this.m_NetworkRotation, this.m_Angle * (1.0f / PhotonNetwork.SerializationRate));
                }
                else
                {
                    tr.position = Vector3.MoveTowards(tr.position, this.m_NetworkPosition, this.m_Distance * (1.0f / PhotonNetwork.SerializationRate));
                    tr.rotation = Quaternion.RotateTowards(tr.rotation, this.m_NetworkRotation, this.m_Angle * (1.0f / PhotonNetwork.SerializationRate));
                }
            }
            else if (PhotonViewComponent.IsMine)
            {
                body.isKinematic = false;

                if (GrabInteractable.isSelected)
                {
                    //objectCollider.enabled = false;
                }
                else if (!GrabInteractable.isSelected)
                {
                    //objectCollider.enabled = true;
                }
            }
        }
    }
}

