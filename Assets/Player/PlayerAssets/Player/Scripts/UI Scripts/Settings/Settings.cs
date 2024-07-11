using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;

public class Settings : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    private InputActionAsset inputActionAsset;
    [SerializeField] private GameObject rebindPanel;
    [SerializeField] private TMP_Text text;

    private Dictionary<string, InputControl> originalControls = new Dictionary<string, InputControl>();

    private void Awake()
    {
        inputActionAsset = playerInput.actions;

        // Check for duplicate bindings during initialization
        foreach (var action in inputActionAsset)
        {
            CheckForDuplicateBindings(action);
        }
    }

    public void StartRebinding(InputActionReference actionToRebind)
    {
        try
        {
            rebindPanel.SetActive(true);
            Time.timeScale = 1f;

            var rebindOperation = actionToRebind.action.PerformInteractiveRebinding()
                .WithControlsExcluding("Mouse")
                .OnMatchWaitForAnother(0.1f)
                .OnComplete(operation => OnRebindComplete(actionToRebind, operation))
                .Start();
        }
        catch (DuplicateBindingException ex)
        {
            Debug.Log(ex.Message);
            // Handle the duplicate binding exception (show a warning, etc.)
        }
    }

    private void CheckForDuplicateBindings(InputAction action)
    {
        foreach (var otherAction in inputActionAsset)
        {
            if (action != otherAction && action.bindings.Contains(otherAction.bindings[0]))
            {
                Debug.Log($"Duplicate binding detected for {action.name}");
                throw new DuplicateBindingException($"Duplicate binding detected for {action.name}");
            }
        }
    }

    private void OnRebindComplete(InputActionReference actionToRebind, InputActionRebindingExtensions.RebindingOperation operation)
    {
        rebindPanel.SetActive(false);
        Time.timeScale = 1f;

        if (!originalControls.ContainsKey(actionToRebind.action.name))
        {
            originalControls[actionToRebind.action.name] = operation.selectedControl;
            Debug.Log($"Rebinding {actionToRebind.action.name} to {operation.selectedControl}");

            string displayText = operation.selectedControl.displayName;
            int lastSlashIndex = displayText.LastIndexOf('/');
            if (lastSlashIndex != -1)
            {
                displayText = displayText.Substring(lastSlashIndex + 1);
            }

            Debug.Log("Changing text");
            text.text = displayText;
        }
        else
        {
            InputControl defaultControl = originalControls[actionToRebind.action.name];
            if (defaultControl == operation.selectedControl)
            {
                Debug.Log($"Rejected rebind as it matches the default control for {actionToRebind.action.name}");
            }
            else
            {
                Debug.Log($"Rejected duplicate rebind for {actionToRebind.action.name}");
            }
        }

        operation.Dispose();
    }
}

[Serializable]
public class DuplicateBindingException : Exception
{
    public DuplicateBindingException() { }
    public DuplicateBindingException(string message) : base(message) { }
    public DuplicateBindingException(string message, Exception inner) : base(message, inner) { }
    protected DuplicateBindingException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
}
