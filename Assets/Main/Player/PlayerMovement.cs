using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    Transform tr;
    Rigidbody rb;
    ClassesScriptable classObj;
    public ClientManager clientManager;
    [SerializeField] private Animator animator;

    private string currentAnimationState;

    [SyncVar(hook = nameof(upAnimIdle))] string animStateIdle;
    [SyncVar(hook = nameof(upAnimAttack))] string animStateAttack;
    [SyncVar(hook = nameof(upAnimDie))] string animStateDie;

    public float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clientManager = FindFirstObjectByType<ClientManager>();
        classObj = clientManager.activeClass;
        rb = GetComponent<Rigidbody>();
        if (!isLocalPlayer)
        {
            Destroy(rb);

            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null)
            {
                Destroy(cam.gameObject);
            }
            

        } else
        {
            moveSpeed = classObj.classSpeed;
            CmdUpdateDieState(clientManager.activeClass.animDie);
            CmdUpdateAttackState(clientManager.activeClass.animAttack);
            CmdUpdateIdleState(clientManager.activeClass.animIdle);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        Move();
    }

    void Move()
    {
        // Captura input do jogador
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector3(h * moveSpeed, rb.linearVelocity.y, v * moveSpeed);
    }

    public void upAnimIdle(string olValue, string newValue)
    {
        changeAnimation(animStateIdle);
    }
    public void upAnimAttack(string olValue, string newValue)
    {
        changeAnimation(animStateAttack);
    }
    public void upAnimDie(string olValue, string newValue)
    {
        changeAnimation(animStateDie);
    }
    [Command]
    public void CmdUpdateIdleState(string newState)
    {
        animStateIdle = newState;
    }
    [Command]
    public void CmdUpdateAttackState(string newState)
    {
        animStateAttack = newState;
    }
    [Command]
    public void CmdUpdateDieState(string newState)
    {
        animStateDie = newState;
    }

    private void changeAnimation(string state)
    {
        if (currentAnimationState == state) return;
        animator.Play(state);
        currentAnimationState = state;
    }
}
