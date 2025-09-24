using UnityEngine;
using Mirror;

public class PlayerAttack : NetworkBehaviour
{
    [Header("Attack Settings")]
    public float damageAmount = 25f;
    public LayerMask enemyLayers;
    
    [Header("Input Settings")]
    public KeyCode attackKey = KeyCode.Space;

    private SpriteRenderer spriteAtack;
    public GameObject playerAtack;
    public GameObject playerAim;
    
    [Header("Visual Settings")]
    public float attackVisualDuration = 0.2f; // Duração em segundos
    
    private bool isShowingAttack = false;
    private float attackVisualTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteAtack = playerAtack.GetComponent<SpriteRenderer>();

        // Garante que o sprite de ataque comece invisível
        if (spriteAtack != null)
        {
            spriteAtack.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        // Controla a animação do ataque
        if (isShowingAttack)
        {
            attackVisualTimer -= Time.deltaTime;
            if (attackVisualTimer <= 0f)
            {
                HideAttackVisual();
            }
        }

        // Verifica se o jogador pressionou a tecla de ataque
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        
        // Rotaciona o playerAim baseado na posição do mouse
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 aimScreenPos = Camera.main.WorldToScreenPoint(playerAim.transform.position);
        
        Vector2 direction = mouseScreenPos - aimScreenPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Define rotação fixa para X e Y, modificando apenas Z
        playerAim.transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    void Attack()
    {
        // Mostra o visual do ataque
        ShowAttackVisual();
        
        // Calcula o ponto de ataque relativo à posição do jogador
        
        // Detecta inimigos na área de ataque com rotação de 90 graus (3D)
        Collider[] hitEnemies = Physics.OverlapBox(playerAtack.transform.position, new Vector3 (playerAtack.transform.localScale.x/2, playerAtack.transform.localScale.y/2, 0), Quaternion.Euler(0, 0, 90f), enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            // Ignora o próprio colisor do jogador
            if (enemy.transform == transform)
                continue;
            GameObject enemyObject = enemy.gameObject;
            PlayerHealth playerHealth = enemyObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.CmdTakeDamage(damageAmount);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Calcula a posição do gizmo
        Vector3 gizmoPosition = playerAtack.transform.position;
        
        // Salva a matriz atual e aplica rotação de 90 graus
        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(gizmoPosition, Quaternion.Euler(90f, 0, 0), Vector3.one);
        
        // Define a cor do gizmo
        Gizmos.color = Color.red;
        
        // Desenha um cubo wireframe com o MESMO tamanho usado no OverlapBox (localScale completo)
        // OverlapBox usa half-extents, mas o gizmo DrawCube já espera tamanho completo
        Gizmos.DrawWireCube(Vector3.zero, new Vector3 (playerAtack.transform.localScale.x, playerAtack.transform.localScale.y, 0));
        
        // Opcionalmente, desenha um cubo semi-transparente para melhor visualização
        Gizmos.color = new Color(1, 0, 0, 0.3f); // Vermelho com transparência
        // Gizmos.DrawCube(Vector3.zero, playerAtack.transform.localScale);
        
        // Restaura a matriz original
        Gizmos.matrix = oldMatrix;
    }
    
    void ShowAttackVisual()
    {
        if (spriteAtack != null)
        {
            spriteAtack.enabled = true;
            isShowingAttack = true;
            attackVisualTimer = attackVisualDuration;
            
        }
    }
    
    void HideAttackVisual()
    {
        if (spriteAtack != null)
        {
            spriteAtack.enabled = false;
            isShowingAttack = false;
            attackVisualTimer = 0f;
        }
    }
    
}

