using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    Transform tr;
    Rigidbody rb;

    public float moveSpeed = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!isLocalPlayer)
        {
            Destroy(rb);

            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null)
            {
                Destroy(cam.gameObject);
            }
                
            return;
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
}
