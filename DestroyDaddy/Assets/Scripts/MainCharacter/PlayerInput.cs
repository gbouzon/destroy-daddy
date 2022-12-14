//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Assets/Scripts/MainCharacter/PlayerInput.inputactions
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

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""MainCharacterControls"",
            ""id"": ""6d4c5e53-3a68-4c61-a537-a75353faf901"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""6c59c3df-61bc-4cb2-acd6-3f10208d8da3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""00b39d40-a242-4c9e-8e69-920cdad67e57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""15b63275-9c84-452f-aab6-17e38e9d8c14"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""35e0bb62-ddd6-4862-bdc9-f8ba0e117391"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""7579409c-3a09-4fe9-aef9-cf4255e1b83e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""2efca7e6-fd35-4e54-867e-a19f6c2b421c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WSAD"",
                    ""id"": ""5416ee71-7bed-4691-885c-a80035a3ba3c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""08c81946-abfd-4606-aa4a-bd76d95231e7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ec1d8068-4e8d-4d88-93f4-fff3c11a1e31"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f1c1e540-62a4-4aa5-aced-3cc9c6521086"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ce8a4b8a-cbbf-4a87-867d-06d77db7698b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKey"",
                    ""id"": ""e960bc13-e9bc-4c39-99cd-6fd51b65a5a3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""87da9562-631e-4b6f-8654-d9f9fa2079ef"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3f5e3b0d-59da-4912-ac2c-af903be16fd2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5bef2098-c67b-405e-b953-30c9ebfba39b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c41578a5-c97d-4581-b82a-befe8e528b37"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e36234ac-2e7c-4c00-a01c-7178779880db"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec53a098-3f75-4956-99e4-bc16c22b55a6"",
                    ""path"": ""<Keyboard>/rightShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""314fd11e-c71f-4283-9915-e98c46c94679"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7a3e6a07-b2d0-4a09-9a7f-171f4871b7b0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f223b575-f587-445a-9b35-c17fc69d8ee5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5054771d-6bef-4481-9eec-1f409509012a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8965f79-a1df-43f9-b6a7-c91980d68d1b"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false),ScaleVector2(x=0.15,y=0.15)"",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainCharacterControls
        m_MainCharacterControls = asset.FindActionMap("MainCharacterControls", throwIfNotFound: true);
        m_MainCharacterControls_Move = m_MainCharacterControls.FindAction("Move", throwIfNotFound: true);
        m_MainCharacterControls_Run = m_MainCharacterControls.FindAction("Run", throwIfNotFound: true);
        m_MainCharacterControls_Jump = m_MainCharacterControls.FindAction("Jump", throwIfNotFound: true);
        m_MainCharacterControls_Aim = m_MainCharacterControls.FindAction("Aim", throwIfNotFound: true);
        m_MainCharacterControls_Shoot = m_MainCharacterControls.FindAction("Shoot", throwIfNotFound: true);
        m_MainCharacterControls_Mouse = m_MainCharacterControls.FindAction("Mouse", throwIfNotFound: true);
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

    // MainCharacterControls
    private readonly InputActionMap m_MainCharacterControls;
    private IMainCharacterControlsActions m_MainCharacterControlsActionsCallbackInterface;
    private readonly InputAction m_MainCharacterControls_Move;
    private readonly InputAction m_MainCharacterControls_Run;
    private readonly InputAction m_MainCharacterControls_Jump;
    private readonly InputAction m_MainCharacterControls_Aim;
    private readonly InputAction m_MainCharacterControls_Shoot;
    private readonly InputAction m_MainCharacterControls_Mouse;
    public struct MainCharacterControlsActions
    {
        private @PlayerInput m_Wrapper;
        public MainCharacterControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MainCharacterControls_Move;
        public InputAction @Run => m_Wrapper.m_MainCharacterControls_Run;
        public InputAction @Jump => m_Wrapper.m_MainCharacterControls_Jump;
        public InputAction @Aim => m_Wrapper.m_MainCharacterControls_Aim;
        public InputAction @Shoot => m_Wrapper.m_MainCharacterControls_Shoot;
        public InputAction @Mouse => m_Wrapper.m_MainCharacterControls_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_MainCharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainCharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(IMainCharacterControlsActions instance)
        {
            if (m_Wrapper.m_MainCharacterControlsActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnMove;
                @Run.started -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnRun;
                @Jump.started -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnJump;
                @Aim.started -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnAim;
                @Shoot.started -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnShoot;
                @Mouse.started -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_MainCharacterControlsActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_MainCharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public MainCharacterControlsActions @MainCharacterControls => new MainCharacterControlsActions(this);
    public interface IMainCharacterControlsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
}
