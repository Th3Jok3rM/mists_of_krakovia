using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LoginManage : MonoBehaviour
{
    ClientManager clientManager;
    public ClassesScriptable[] listOfClasses;
    public GameObject btnEnterObj;

    private void Awake()
    {
        clientManager = FindAnyObjectByType<ClientManager>();
    }
    public void btnEnter()
    {
        if (clientManager.playerNickname.Length < 1) return;
        SceneManager.LoadScene("TesteServer");
    }
    public void chanceSelectedClass(int index)
    {
        clientManager.activeClass = listOfClasses[index];
    }
    public void changeNickname(string nickname)
    {
        if (nickname.Length < 1) btnEnterObj.GetComponent<UnityEngine.UI.Button>().interactable = false; else btnEnterObj.GetComponent<UnityEngine.UI.Button>().interactable = true;
        if (nickname.Length > 0) clientManager.playerNickname = nickname;
    }
}
