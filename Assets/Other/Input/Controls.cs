// GENERATED AUTOMATICALLY FROM 'Assets/Other/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""087dada7-70fe-47b0-8fe9-f319b42fe88e"",
            ""actions"": [
                {
                    ""name"": ""Pointer"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8fc2a029-c45b-443c-a1a8-6ff78377ecbf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pickup"",
                    ""type"": ""Button"",
                    ""id"": ""0e18fd90-fb42-46be-ab1a-90b3a0d70e89"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""252899ec-a4e5-40c7-925a-4690efdc620c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ad190de-9a7a-4c23-b705-88795fe1112f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pickup"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Pointer = m_Game.FindAction("Pointer", throwIfNotFound: true);
        m_Game_Pickup = m_Game.FindAction("Pickup", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Pointer;
    private readonly InputAction m_Game_Pickup;
    public struct GameActions
    {
        private @Controls m_Wrapper;
        public GameActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pointer => m_Wrapper.m_Game_Pointer;
        public InputAction @Pickup => m_Wrapper.m_Game_Pickup;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Pointer.started -= m_Wrapper.m_GameActionsCallbackInterface.OnPointer;
                @Pointer.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnPointer;
                @Pointer.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnPointer;
                @Pickup.started -= m_Wrapper.m_GameActionsCallbackInterface.OnPickup;
                @Pickup.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnPickup;
                @Pickup.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnPickup;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pointer.started += instance.OnPointer;
                @Pointer.performed += instance.OnPointer;
                @Pointer.canceled += instance.OnPointer;
                @Pickup.started += instance.OnPickup;
                @Pickup.performed += instance.OnPickup;
                @Pickup.canceled += instance.OnPickup;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface IGameActions
    {
        void OnPointer(InputAction.CallbackContext context);
        void OnPickup(InputAction.CallbackContext context);
    }
}
