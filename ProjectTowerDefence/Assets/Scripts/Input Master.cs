// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input Master.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input Master"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""fba0fcc4-76b6-4224-b72f-673e30be6a8b"",
            ""actions"": [
                {
                    ""name"": ""movment"",
                    ""type"": ""PassThrough"",
                    ""id"": ""79a3c449-7d08-42d2-889a-9a4f0f493116"",
                    ""expectedControlType"": ""Dpad"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KeyBoard"",
                    ""id"": ""114d9769-9845-47d3-9caa-a25fdf6dac82"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movment"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d0d49f31-7afb-41fd-9c5d-f6518c77345b"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b244c6ad-2b8f-4702-b275-d7dbd4d529d2"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5619b4f9-44d7-47b8-80fb-2cdc793ef883"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2ec6ee3b-21fb-4130-9e78-0736f0489da4"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movment"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Buildings"",
            ""id"": ""33def222-c2bb-4358-8741-bb2c3f2d63b3"",
            ""actions"": [
                {
                    ""name"": ""Build start"",
                    ""type"": ""Button"",
                    ""id"": ""44ea28d7-f3f3-4b98-94aa-cbb60adf5885"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build cancel"",
                    ""type"": ""Button"",
                    ""id"": ""af0d9b76-9041-4cbd-8aa2-07bd66d86cc8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build change Id"",
                    ""type"": ""Value"",
                    ""id"": ""46e77adc-830c-4383-aa95-d1b55a95732c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Build new"",
                    ""type"": ""Button"",
                    ""id"": ""bce87a92-3228-46fe-853f-bbc32981d17b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""85e4cd7a-1f0a-49c0-a931-dd4dd93e9f0a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f9efcff4-2cde-4dbc-a1fb-0de6fcffd5ae"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d9541d0b-06c1-4892-8b9f-1002a6a47864"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build change Id"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""976c8cbf-2cac-4d3d-b299-0345a4a8adf3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build change Id"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d9f8d6bf-e49e-45c2-aa12-0e897b01be9c"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build change Id"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""e4f73748-a175-4cb4-9d4f-977c0b61fe05"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build new"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_movment = m_Camera.FindAction("movment", throwIfNotFound: true);
        // Buildings
        m_Buildings = asset.FindActionMap("Buildings", throwIfNotFound: true);
        m_Buildings_Buildstart = m_Buildings.FindAction("Build start", throwIfNotFound: true);
        m_Buildings_Buildcancel = m_Buildings.FindAction("Build cancel", throwIfNotFound: true);
        m_Buildings_BuildchangeId = m_Buildings.FindAction("Build change Id", throwIfNotFound: true);
        m_Buildings_Buildnew = m_Buildings.FindAction("Build new", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_movment;
    public struct CameraActions
    {
        private @InputMaster m_Wrapper;
        public CameraActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @movment => m_Wrapper.m_Camera_movment;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @movment.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovment;
                @movment.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovment;
                @movment.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovment;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @movment.started += instance.OnMovment;
                @movment.performed += instance.OnMovment;
                @movment.canceled += instance.OnMovment;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Buildings
    private readonly InputActionMap m_Buildings;
    private IBuildingsActions m_BuildingsActionsCallbackInterface;
    private readonly InputAction m_Buildings_Buildstart;
    private readonly InputAction m_Buildings_Buildcancel;
    private readonly InputAction m_Buildings_BuildchangeId;
    private readonly InputAction m_Buildings_Buildnew;
    public struct BuildingsActions
    {
        private @InputMaster m_Wrapper;
        public BuildingsActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Buildstart => m_Wrapper.m_Buildings_Buildstart;
        public InputAction @Buildcancel => m_Wrapper.m_Buildings_Buildcancel;
        public InputAction @BuildchangeId => m_Wrapper.m_Buildings_BuildchangeId;
        public InputAction @Buildnew => m_Wrapper.m_Buildings_Buildnew;
        public InputActionMap Get() { return m_Wrapper.m_Buildings; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BuildingsActions set) { return set.Get(); }
        public void SetCallbacks(IBuildingsActions instance)
        {
            if (m_Wrapper.m_BuildingsActionsCallbackInterface != null)
            {
                @Buildstart.started -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildstart;
                @Buildstart.performed -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildstart;
                @Buildstart.canceled -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildstart;
                @Buildcancel.started -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildcancel;
                @Buildcancel.performed -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildcancel;
                @Buildcancel.canceled -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildcancel;
                @BuildchangeId.started -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildchangeId;
                @BuildchangeId.performed -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildchangeId;
                @BuildchangeId.canceled -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildchangeId;
                @Buildnew.started -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildnew;
                @Buildnew.performed -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildnew;
                @Buildnew.canceled -= m_Wrapper.m_BuildingsActionsCallbackInterface.OnBuildnew;
            }
            m_Wrapper.m_BuildingsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Buildstart.started += instance.OnBuildstart;
                @Buildstart.performed += instance.OnBuildstart;
                @Buildstart.canceled += instance.OnBuildstart;
                @Buildcancel.started += instance.OnBuildcancel;
                @Buildcancel.performed += instance.OnBuildcancel;
                @Buildcancel.canceled += instance.OnBuildcancel;
                @BuildchangeId.started += instance.OnBuildchangeId;
                @BuildchangeId.performed += instance.OnBuildchangeId;
                @BuildchangeId.canceled += instance.OnBuildchangeId;
                @Buildnew.started += instance.OnBuildnew;
                @Buildnew.performed += instance.OnBuildnew;
                @Buildnew.canceled += instance.OnBuildnew;
            }
        }
    }
    public BuildingsActions @Buildings => new BuildingsActions(this);
    public interface ICameraActions
    {
        void OnMovment(InputAction.CallbackContext context);
    }
    public interface IBuildingsActions
    {
        void OnBuildstart(InputAction.CallbackContext context);
        void OnBuildcancel(InputAction.CallbackContext context);
        void OnBuildchangeId(InputAction.CallbackContext context);
        void OnBuildnew(InputAction.CallbackContext context);
    }
}
