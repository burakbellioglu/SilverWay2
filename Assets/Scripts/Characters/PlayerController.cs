using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Componentler
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerCombat playerCombat;

    [Header("Degisken degerleri")]

    public float speed;
    public float jumpingPower;
    private float frictionS = 1.5f;

    private bool isRunning;
    private bool coroutineAllowed_running = true;
    private bool coroutineAllowed_jump = false;

    private float moveInput;
    public bool isFacingRight = true;

    [Header("Atanacak degerler")]

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Particle atamalarý")]
    public ParticleSystem runParticle;
    public ParticleSystem jumpParticle;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();

    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxis("Horizontal");

        if(playerCombat.isAttacking)
            rb.velocity = new Vector2(speed/2 * moveInput, rb.velocity.y);
        else
            rb.velocity = new Vector2(speed * moveInput, rb.velocity.y);


            Friction();
    }

    private void Update()
    {

        //Zýplama hareket ve animasyonlarý
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            anim.SetTrigger("jumpTakeOf");
            coroutineAllowed_jump = true; //Jump  Particle
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        else if (IsGrounded())
        {
            anim.SetBool("isJumping", false);
            SpawnJumpParticle(); //Jump  Particle
        }
        else
        {
            anim.SetBool("isJumping", true);
        }

        //Karakteri döndürme
        if(!playerCombat.isAttacking)
            Flip();

        //Hareket animasyonu
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
            isRunning = false;
        }
        else
        {
            anim.SetBool("isRunning", true);
            isRunning = true;
        }

        //Hareket particle
        if(IsGrounded() && isRunning) //yerde ise ve hareket halinde ise
        {
            StartCoroutine(SpawnRunParticle());
        }
        else
        {
            StopCoroutine(SpawnRunParticle());
            coroutineAllowed_running = true;
        }
       

    }


    #region Movement Particle
    IEnumerator SpawnRunParticle()
    {
        if (coroutineAllowed_running)
        {
            coroutineAllowed_running = false;
            yield return new WaitForSeconds(0.4f);
            while (IsGrounded() && isRunning)
            {
                Instantiate(runParticle, groundCheck.transform.position, runParticle.transform.rotation);
                yield return new WaitForSeconds(0.4f);
            }
        }

    }

    private void SpawnJumpParticle()
    {
        if (coroutineAllowed_jump)
        {
            coroutineAllowed_jump = false;
            Instantiate(jumpParticle, groundCheck.transform.position, runParticle.transform.rotation);
        }
    }

    #endregion


    //Ground Check
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && moveInput < 0f || !isFacingRight && moveInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    //Surtunme
    void Friction()
    {
        Vector3 hizDusur = rb.velocity;
        hizDusur.z = 0;
        hizDusur.y = rb.velocity.y;
        hizDusur.x *= frictionS;
        rb.velocity = hizDusur;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.1f);
    }

}
