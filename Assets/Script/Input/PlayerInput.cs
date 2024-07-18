using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


[CreateAssetMenu(fileName = "PlayerInput")]
public class PlayerInput : ScriptableObject, InputActions.IPlayerActions
{
    private InputActions inputActions;

    public UnityAction<Vector2> onWalk;
    public UnityAction disWalk;
    public UnityAction onJump;

    public void OnWalk(InputAction.CallbackContext context)
    {
        if (context.performed)
            onWalk?.Invoke(context.ReadValue<Vector2>());
        if (context.canceled)
            disWalk?.Invoke();
    }

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Player.SetCallbacks(this);
    }

    public void enablePlayerinput()
    {
        inputActions.Player.Enable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            onJump?.Invoke();
        }
    }
}
