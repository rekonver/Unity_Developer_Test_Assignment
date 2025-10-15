using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private InputActionAsset actionsAsset = null;
    [SerializeField] private string actionMapName = "Gameplay";
    [SerializeField] private string moveActionName = "Move";
    [SerializeField] private string jumpActionName = "Jump";

    private InputAction moveAction;
    private InputAction jumpAction;

    private float moveValue;

    private bool jumpHeld;
    private float lastJumpPressedTime = float.NegativeInfinity;
    private float lastJumpReleasedTime = float.NegativeInfinity;

    public float LastJumpPressedTime => lastJumpPressedTime;
    public float LastJumpReleasedTime => lastJumpReleasedTime;
    public bool JumpHeld => jumpHeld;
    public float Move => moveValue;

    void OnEnable()
    {
        if (actionsAsset == null)
        {
            Debug.LogError("PlayerInput: actionsAsset not assigned.");
            return;
        }

        var map = actionsAsset.FindActionMap(actionMapName, false);
        if (map == null)
        {
            Debug.LogWarning($"PlayerInput: action map '{actionMapName}' not found. Falling back to first map if present.");
            if (actionsAsset.actionMaps.Count > 0) map = actionsAsset.actionMaps[0];
            else return;
        }

        moveAction = map.FindAction(moveActionName, false);
        jumpAction = map.FindAction(jumpActionName, false);

        if (moveAction != null) moveAction.Enable();
        if (jumpAction != null)
        {
            jumpAction.performed += OnJumpPerformed;
            jumpAction.canceled += OnJumpCanceled;
            jumpAction.Enable();
        }
    }

    void OnDisable()
    {
        if (moveAction != null) moveAction.Disable();

        if (jumpAction != null)
        {
            jumpAction.performed -= OnJumpPerformed;
            jumpAction.canceled -= OnJumpCanceled;
            jumpAction.Disable();
        }
    }

    void Update()
    {
        if (moveAction == null) return;

        try
        {
            if (!string.IsNullOrEmpty(moveAction.expectedControlType) &&
                moveAction.expectedControlType.Equals("Vector2", StringComparison.OrdinalIgnoreCase))
            {
                moveValue = moveAction.ReadValue<Vector2>().x;
            }
            else
            {
                moveValue = moveAction.ReadValue<float>();
            }
        }
        catch
        {
            try { moveValue = moveAction.ReadValue<Vector2>().x; }
            catch { moveValue = moveAction.ReadValue<float>(); }
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        lastJumpPressedTime = Time.time;
        jumpHeld = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext ctx)
    {
        lastJumpReleasedTime = Time.time;
        jumpHeld = false;
    }
}
