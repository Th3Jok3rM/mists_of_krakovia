using Mirror;
using TMPro;
using UnityEngine;

public class PlayerStatus : NetworkBehaviour
{
    ClientManager clientManager;
    public TextMeshProUGUI txtNickmane;

    //servidor
    [SyncVar(hook = nameof(updateNickname))]
    string nickname;

    void Start()
    {
        if (isLocalPlayer)
        {
            clientManager = FindFirstObjectByType<ClientManager>();
            CmdUpdateNickname(clientManager.playerNickname);
        }
    }
    private void Update()
    {
        if (isLocalPlayer && Input.GetKeyDown(KeyCode.G)) {
            CmdUpdateNickname(clientManager.playerNickname);
        }
    }

    public void updateNickname(string olValue, string newValue)
    {
        txtNickmane.text = nickname;
    }
    [Command]
    public void CmdUpdateNickname(string newValue)
    {
        nickname = newValue;
    }
}
