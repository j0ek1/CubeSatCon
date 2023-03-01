using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class RayToggleR : MonoBehaviour
{
    public InputActionReference rayToggleRef;
    public XRRayInteractor rayInteractor;
    private bool isEnabled = false;
    public bool editMode = false;

    void Start()
    {
        //rayInteractor = GetComponent<XRRayInteractor>();
        rayToggleRef.action.started += RayToggle;
        
    }

    void OnDestroy()
    {
        rayToggleRef.action.started -= RayToggle;
    }

    private void RayToggle(InputAction.CallbackContext context) // When primary button pressed enable ray
    {
        isEnabled = !isEnabled;
        rayInteractor.enabled = isEnabled;
        editMode = !editMode;
    }
}
