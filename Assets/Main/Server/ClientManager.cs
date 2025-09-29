using UnityEngine;

public class ClientManager : MonoBehaviour
{
    public string playerNickname;
    public ClassesScriptable activeClass;

    void Awake()
    {
        var objs = GameObject.FindObjectsByType(typeof(ClientManager),FindObjectsSortMode.None);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
