using UnityEngine;
using Mirror;

public class StartDedicatedServer : MonoBehaviour
{
    void Start()
    {
        NetworkManager.singleton.StartServer();
    }
}