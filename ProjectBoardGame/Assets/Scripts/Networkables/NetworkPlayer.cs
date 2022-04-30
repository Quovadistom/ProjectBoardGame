using System.Collections;
using System.Collections.Generic;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ProjectTM.Networkables
{
    public class NetworkPlayer : MonoBehaviour
    {
        public Transform Head;
        public Transform LeftHand;
        public Transform RightHand;
        public Animator LeftHandAnimator, RightHandAnimator;
        public HandPresence LeftHandPresence, RightHandPresence;

        private ActionBasedController leftHandActionBased, rightHandActionBased;
        //private PhotonView photonView;
        private Transform headRig;
        private Transform leftHandRig;
        private Transform rightHandRig;

        void Start()
        {
            //photonView = GetComponent<PhotonView>();
            XRRig rig = FindObjectOfType<XRRig>();
            headRig = rig.transform.Find("Camera Offset/Main Camera");
            leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
            rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");
            leftHandActionBased = leftHandRig.gameObject.GetComponent<ActionBasedController>();
            rightHandActionBased = rightHandRig.gameObject.GetComponent<ActionBasedController>();

            //if (photonView.IsMine)
            {
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
            }
        }

        public void Init(HandPresence leftHandPresence, HandPresence rightHandPresence)
        {
            LeftHandPresence = leftHandPresence;
            RightHandPresence = rightHandPresence;
        }

        void Update()
        {
            /*
            if (photonView.IsMine)
            {
                MapPosition(Head, headRig);
                MapPosition(RightHand, rightHandRig);
                MapPosition(LeftHand, leftHandRig);

                UpdateHandAnimation(LeftHandAnimator, LeftHandPresence);
                UpdateHandAnimation(RightHandAnimator, RightHandPresence);
            }
            */
        }

        void UpdateHandAnimation(Animator handAnimator, HandPresence handPresence)
        {
            if (handPresence.HandAnimator != null)
            {
                handAnimator.SetFloat("Grip", handPresence.HandAnimator.GetFloat("Grip"));
                handAnimator.SetFloat("Trigger", handPresence.HandAnimator.GetFloat("Trigger"));
                handAnimator.SetFloat("ThumbRest", handPresence.HandAnimator.GetFloat("ThumbRest"));
            }
        }

        void MapPosition(Transform target, Transform rigTransform)
        {
            target.position = rigTransform.position;
            target.rotation = rigTransform.rotation;
        }
    }
}
