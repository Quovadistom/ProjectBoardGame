using Fusion;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace ProjectTM.Networkables
{
    public class NetworkPlayer : SimulationBehaviour
    {
        public Transform Head;
        public Transform LeftHand;
        public Transform RightHand;
        public Animator LeftHandAnimator, RightHandAnimator;

        private NetworkObject networkObject;
        private Transform headRig;
        private Transform leftHandRig;
        private Transform rightHandRig;
        private HandPresence m_leftHandPresence, m_rightHandPresence;

        void Start()
        {
            networkObject = GetComponent<NetworkObject>();
            networkObject.AssignInputAuthority(Runner.LocalPlayer);
            XROrigin rig = FindObjectOfType<XROrigin>();
            headRig = rig.transform.Find("Camera Offset/Main Camera");
            leftHandRig = rig.transform.Find("Camera Offset/LeftHand Controller");
            rightHandRig = rig.transform.Find("Camera Offset/RightHand Controller");

            if (networkObject.HasInputAuthority)
            {
                foreach (var renderer in GetComponentsInChildren<Renderer>())
                {
                    renderer.enabled = false;
                }
            }
        }

        public void Init(HandPresence leftHandPresence, HandPresence rightHandPresence)
        {
            m_leftHandPresence = leftHandPresence;
            m_rightHandPresence = rightHandPresence;
        }

        void Update()
        {            
            if (networkObject.HasStateAuthority)
            {
                MapPosition(Head, headRig);
                MapPosition(RightHand, rightHandRig);
                MapPosition(LeftHand, leftHandRig);

                UpdateHandAnimation(LeftHandAnimator, m_leftHandPresence);
                UpdateHandAnimation(RightHandAnimator, m_rightHandPresence);
            }
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
