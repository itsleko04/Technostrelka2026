using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class XRActionEvent : MonoBehaviour
{
    [Header("Event Actions")]
    [SerializeField] private UnityEvent _event;

    [Header("Input Action")]
    [SerializeField] private InputActionProperty _inputAction;

    private void OnEnable()
    {
        _inputAction.action.Enable();
        _inputAction.action.performed += InvokeEvent;
    }

    private void OnDisable()
    {
        _inputAction.action.performed -= InvokeEvent;
        _inputAction.action.Disable();
    }

    private void InvokeEvent(InputAction.CallbackContext context)
    {
        _event.Invoke();
    }
}
