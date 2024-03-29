//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scipts + Action Assets/Action Assets + Their Scripts/Controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Battlefield Control"",
            ""id"": ""cb864428-2e93-4af3-a4c7-510829912156"",
            ""actions"": [
                {
                    ""name"": ""TapSelect"",
                    ""type"": ""Value"",
                    ""id"": ""b84baae1-33c9-43ab-be22-ff92a65cafb2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""SlowTap(duration=1E-05)"",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BoxSelect"",
                    ""type"": ""Value"",
                    ""id"": ""e27ac622-637d-45f5-9cac-e433da4b4ca2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""SlowTap(duration=1E-05)"",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7e69e035-d65a-4e09-b41f-f2bdc8c4c353"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TapSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d894b0d4-e093-44ae-84e1-b10d691e4b09"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BoxSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Battlefield Control
        m_BattlefieldControl = asset.FindActionMap("Battlefield Control", throwIfNotFound: true);
        m_BattlefieldControl_TapSelect = m_BattlefieldControl.FindAction("TapSelect", throwIfNotFound: true);
        m_BattlefieldControl_BoxSelect = m_BattlefieldControl.FindAction("BoxSelect", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Battlefield Control
    private readonly InputActionMap m_BattlefieldControl;
    private List<IBattlefieldControlActions> m_BattlefieldControlActionsCallbackInterfaces = new List<IBattlefieldControlActions>();
    private readonly InputAction m_BattlefieldControl_TapSelect;
    private readonly InputAction m_BattlefieldControl_BoxSelect;
    public struct BattlefieldControlActions
    {
        private @Controls m_Wrapper;
        public BattlefieldControlActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @TapSelect => m_Wrapper.m_BattlefieldControl_TapSelect;
        public InputAction @BoxSelect => m_Wrapper.m_BattlefieldControl_BoxSelect;
        public InputActionMap Get() { return m_Wrapper.m_BattlefieldControl; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattlefieldControlActions set) { return set.Get(); }
        public void AddCallbacks(IBattlefieldControlActions instance)
        {
            if (instance == null || m_Wrapper.m_BattlefieldControlActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BattlefieldControlActionsCallbackInterfaces.Add(instance);
            @TapSelect.started += instance.OnTapSelect;
            @TapSelect.performed += instance.OnTapSelect;
            @TapSelect.canceled += instance.OnTapSelect;
            @BoxSelect.started += instance.OnBoxSelect;
            @BoxSelect.performed += instance.OnBoxSelect;
            @BoxSelect.canceled += instance.OnBoxSelect;
        }

        private void UnregisterCallbacks(IBattlefieldControlActions instance)
        {
            @TapSelect.started -= instance.OnTapSelect;
            @TapSelect.performed -= instance.OnTapSelect;
            @TapSelect.canceled -= instance.OnTapSelect;
            @BoxSelect.started -= instance.OnBoxSelect;
            @BoxSelect.performed -= instance.OnBoxSelect;
            @BoxSelect.canceled -= instance.OnBoxSelect;
        }

        public void RemoveCallbacks(IBattlefieldControlActions instance)
        {
            if (m_Wrapper.m_BattlefieldControlActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBattlefieldControlActions instance)
        {
            foreach (var item in m_Wrapper.m_BattlefieldControlActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BattlefieldControlActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BattlefieldControlActions @BattlefieldControl => new BattlefieldControlActions(this);
    public interface IBattlefieldControlActions
    {
        void OnTapSelect(InputAction.CallbackContext context);
        void OnBoxSelect(InputAction.CallbackContext context);
    }
}
