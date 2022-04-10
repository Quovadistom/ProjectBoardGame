using Photon.Voice.Unity;
using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace ProjectTM.Managers
{
    public class VoiceManager : MonoBehaviour
    {
        public NetworkManager NetworkManagerScript;
        public static VoiceManager Instance { get; private set; }
        public Recorder recorder;
        bool isRequesting;

        public bool HasPermission { get; private set; }

        private void Awake()
        {
            HasPermission = false;
            Instance = this;
            InitVoice();

        }

        private void Update()
        {
            if (HasPermission && !recorder.TransmitEnabled)
            {
                StartRecording();
            }
        }

        public void InitVoice()
        {
#if UNITY_ANDROID

            if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
            {
                HasPermission = true;
            }
            else
            {
                Permission.RequestUserPermission(Permission.Microphone);
                isRequesting = true;
            }
#else
        HasPermission = true;
#endif
        }

        private void OnApplicationFocus(bool focus)
        {
#if UNITY_ANDROID
            if (focus && isRequesting)
            {
                if (Permission.HasUserAuthorizedPermission(Permission.Microphone))
                {
                    HasPermission = true;

                }
                else
                {
                    HasPermission = false;
                }
                isRequesting = false;
            }
#endif
        }

        public void StartRecording()
        {
            if (HasPermission)
            {
                recorder.TransmitEnabled = true;
                recorder.StartRecording();
            }
        }

        public void StopRecording()
        {
            if (HasPermission)
            {
                recorder.StopRecording();
                recorder.TransmitEnabled = false;
            }
        }
    }
}