using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;

public class ConnectionUIManager : MonoBehaviour
{
    [SerializeField] UIDocument _uiDocument;
    private void Awake()
    {
#if UNITY_SERVER
    // サーバービルドは自動でStartServer
    if (!NetworkManager.Singleton.IsServer)
    {
        NetworkManager.Singleton.StartServer();
    }
#else
        var hostButton = _uiDocument.rootVisualElement.Q<Button>("HostButton");
        var clientButton = _uiDocument.rootVisualElement.Q<Button>("ClientButton");
        hostButton.clicked += () =>
        {
            hostButton.visible = false;
            clientButton.visible = false;
            NetworkManager.Singleton.StartHost();
        };
        clientButton.clicked += () =>
        {
            hostButton.visible = false;
            clientButton.visible = false;
            NetworkManager.Singleton.StartClient();
        };
#endif
    }
}
