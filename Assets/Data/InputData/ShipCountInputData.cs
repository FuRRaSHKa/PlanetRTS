// GENERATED AUTOMATICALLY FROM 'Assets/Data/InputData/ShipCountInputData.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ShipCountInputData : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ShipCountInputData()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ShipCountInputData"",
    ""maps"": [
        {
            ""name"": ""UI"",
            ""id"": ""b1cad5bf-8654-408a-bdaa-56af4516ee5a"",
            ""actions"": [
                {
                    ""name"": ""ChangeShipCount"",
                    ""type"": ""Value"",
                    ""id"": ""11fc2f96-6312-493c-bb3b-5e00976e7c8e"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ef852503-6b04-4c69-8a3d-160b2ec0827f"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeShipCount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a7d88e94-58b7-418c-89d4-8fc260beb98b"",
                    ""path"": ""1DAxis(whichSideWins=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeShipCount"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""09fa25c1-608b-4b29-93ac-664e3a2996c7"",
                    ""path"": ""<Keyboard>/minus"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeShipCount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""248ef2d0-8c3e-48fe-9ed1-3b7d1afa56a7"",
                    ""path"": ""<Keyboard>/equals"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ChangeShipCount"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_ChangeShipCount = m_UI.FindAction("ChangeShipCount", throwIfNotFound: true);
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_ChangeShipCount;
    public struct UIActions
    {
        private @ShipCountInputData m_Wrapper;
        public UIActions(@ShipCountInputData wrapper) { m_Wrapper = wrapper; }
        public InputAction @ChangeShipCount => m_Wrapper.m_UI_ChangeShipCount;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @ChangeShipCount.started -= m_Wrapper.m_UIActionsCallbackInterface.OnChangeShipCount;
                @ChangeShipCount.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnChangeShipCount;
                @ChangeShipCount.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnChangeShipCount;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ChangeShipCount.started += instance.OnChangeShipCount;
                @ChangeShipCount.performed += instance.OnChangeShipCount;
                @ChangeShipCount.canceled += instance.OnChangeShipCount;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IUIActions
    {
        void OnChangeShipCount(InputAction.CallbackContext context);
    }
}
