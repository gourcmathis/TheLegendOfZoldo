using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    private float temp = 0f;

    [Range(1, 10)]
    public float jumpVelocity;

    private bool isJumping;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider2D playerCollider;

    private Vector3 velocity = Vector3.zero;

    public static PlayerMovement instance;

    private void Awake()
    {
        temp = moveSpeed;
        if(instance != null)
        {
            Debug.LogWarning("More than one instance of PlayerMovement");
            return;
        }

        instance = this;
    }

    void FixedUpdate()
    {
        isGrounded = GroundCheck();
        if(isGrounded)
        {
            animator.SetBool("isJumping", !isGrounded);
        }
        
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * 100 * Time.fixedDeltaTime;
        
        MovePlayer(horizontalMovement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public bool GroundCheck()
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.playerCollider.bounds.center,
        this.playerCollider.bounds.size, 0, Vector2.down, 0.1f, 
        this.collisionLayers);
        if(hit.collider != null && hit.collider.tag == "Platform")
        {
            this.transform.parent = hit.transform;
        }
        else
        {
            this.transform.parent = null;
        }
        animator.SetBool("isJumping", !isGrounded);
        return hit.collider != null;     

        
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump") && isGrounded)
        {    
            animator.SetBool("isJumping", true);
            isJumping = true;

        }
        if(rb.velocity.y < 0){
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }    

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
         {
             moveSpeed = 0;
         } else {
             moveSpeed = temp;
         }

    }

 


    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement,rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.01f);

        if (isJumping == true)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
