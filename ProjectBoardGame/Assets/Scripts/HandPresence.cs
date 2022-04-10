using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;
    public ActionBasedController controller;
    public InputActionReference TriggerTouchedReference;

    private UnityEngine.XR.InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject m_spawnedHandModel;
    public Animator HandAnimator;
    private bool triggerTouched;
    private float triggerLerp, triggerValue, gripValue, thumbRestLerp;

    void Start()
    {
        TryInitialize();

        TriggerTouchedReference.action.performed += OnTriggerTouched;
        TriggerTouchedReference.action.canceled += OnTriggerTouchedCanceled;
    }

    private void OnDestroy()
    {
        TriggerTouchedReference.action.performed -= OnTriggerTouched;
        TriggerTouchedReference.action.canceled -= OnTriggerTouchedCanceled;
    }

    void TryInitialize()
    {
        List<UnityEngine.XR.InputDevice> devices = new List<UnityEngine.XR.InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);


        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
            }

            m_spawnedHandModel = Instantiate(handModelPrefab, transform);
            HandAnimator = m_spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out triggerValue))
        {
            triggerValue = controller.activateAction.action.ReadValue<float>();
            HandAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            HandAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out gripValue))
        {
            gripValue = controller.selectAction.action.ReadValue<float>();
            HandAnimator.SetFloat("Grip", gripValue);

            if (gripValue > triggerValue && triggerTouched)
            {
                triggerLerp = Mathf.Lerp(triggerLerp, gripValue, 30 * Time.deltaTime);
                HandAnimator.SetFloat("Trigger", triggerLerp);
            }
            else if (gripValue > 0.001f && !triggerTouched)
            {
                triggerLerp = Mathf.Lerp(triggerLerp, 0, 30 * Time.deltaTime);
                HandAnimator.SetFloat("Trigger", triggerLerp);
            }
        }
        else
        {
            HandAnimator.SetFloat("Grip", 0);
        }

        if (targetDevice.TryGetFeatureValue(OculusUsages.thumbTouch, out bool test))
        {
            float gripFloat = HandAnimator.GetFloat("Grip");
            float returnValue = 0.5f + gripFloat * 0.5f;

            float value = test ? returnValue : 0;
            thumbRestLerp = Mathf.Lerp(HandAnimator.GetFloat("ThumbRest"), value, 30 * Time.deltaTime);
            HandAnimator.SetFloat("ThumbRest", thumbRestLerp);
        }
    }

    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                if (m_spawnedHandModel)
                    m_spawnedHandModel.SetActive(false);
                if (spawnedController)
                    spawnedController.SetActive(true);
            }
            else
            {
                if (m_spawnedHandModel)
                    m_spawnedHandModel.SetActive(true);
                if (spawnedController)
                    spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }

    private void OnTriggerTouchedCanceled(InputAction.CallbackContext obj)
    {
        triggerTouched = false;
        triggerLerp = gripValue;
    }

    private void OnTriggerTouched(InputAction.CallbackContext obj)
    {
        triggerTouched = true;
    }
}
