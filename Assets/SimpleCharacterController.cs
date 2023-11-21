using UnityEngine;

public class SimpleCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeedMultiplier = 2f;
    public float jumpForce = 500f;
    public float crouchSpeedDivisor = 2f;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public GameObject bulletPrefab;
    public GameObject tavmero;
    public Transform bulletSpawn;
    public float fallMultiplier = 2.5f;
    private AudioSource audioSource;
    public AudioClip shotaud;
    public AudioClip jumpaud;



    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool isCrouching;
    private bool isSprinting;
    private bool isShooting;
    private float groundCheckRadius = 0.2f;
    public bool bal = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isSprinting = Input.GetButton("Sprint");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlaySound(jumpaud);
            Jump();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            PlaySound(shotaud);
            Shoot();
        }

        // Animator paramétereinek frissítése
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetBool("IsCrouching", isCrouching);
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsSprinting", isSprinting);
        animator.SetFloat("VerticalVelocity", rb.velocity.y);
        animator.SetBool("IsShooting", isShooting);
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Horizontal");
        
        if(move < 0) 
            {bal = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);}
        if(move > 0)
            {bal = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);}
        float speed = isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed;

        if (isCrouching)
        {
            move /= crouchSpeedDivisor;
            speed /= crouchSpeedDivisor;
        }

        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Jump()
    {
        //rb.velocity = new Vector2(rb.velocity.x, 0);
        //rb.AddForce(new Vector2(0, jumpForce));
        rb.velocity = Vector2.up * jumpForce;
        animator.SetTrigger("Jump");
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Az egér pozíciójának lekérdezése
        mousePosition.z = 0;
        if(Vector2.Distance(mousePosition,tavmero.transform.position) > 7){
            var a = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            isShooting = true;
            animator.SetTrigger("Shoot");
            isShooting = false;
        }
    }
}
