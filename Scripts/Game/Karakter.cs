using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Karakter : MonoBehaviour
{
    private Rigidbody2D rb;
    private CapsuleCollider2D coll; // Mengganti BoxCollider2D dengan CapsuleCollider2D
    private SpriteRenderer sprite;
    private Animator animator;
    private AudioManager audioManager; // Menambahkan referensi ke AudioManager

    [SerializeField] private LayerMask jumpableGround;

    public float kecepatanBerjalan = 3.0f;
    public float kecepatanLari = 6.0f;
    public float kecepatanMelompat = 7.0f;

    public int maksimalLompatan = 2;
    private int jumlahLompatan = 0;

    private bool isAttackingInProgress = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>(); // Menggunakan CapsuleCollider2D
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // Cari AudioManager di scene
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        bool isWalking = Mathf.Abs(horizontalInput) > 0;
        bool isRunning = isWalking && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        bool isJumping = jumlahLompatan > 0;

        float kecepatanX = isRunning ? kecepatanLari : kecepatanBerjalan;
        rb.velocity = new Vector2(horizontalInput * kecepatanX, rb.velocity.y);

        // Flip karakter sesuai arah gerakan horizontalInput.
        if (horizontalInput > 0)
        {
            sprite.flipX = false; // Menghadap kanan.
        }
        else if (horizontalInput < 0)
        {
            sprite.flipX = true; // Menghadap kiri.
        }

        if (Input.GetButtonDown("Jump") && jumlahLompatan < maksimalLompatan)
        {
            audioManager.PlaySFX(audioManager.jump);
            rb.velocity = new Vector2(rb.velocity.x, kecepatanMelompat);
            jumlahLompatan++;
        }

        // if (Input.GetMouseButtonDown(0) && !isAttackingInProgress)
        // {
        //     isAttackingInProgress = true;
        //     animator.SetBool("IsAttacking", true);
        // }
        else if (Input.GetMouseButtonUp(0) && isAttackingInProgress)
        {
            isAttackingInProgress = false;
            animator.SetBool("IsAttacking", false);
        }

        // Set IsIdle menjadi false saat karakter melakukan tindakan apapun (berjalan, berlari, melompat, menyerang).
        animator.SetBool("IsIdle", !(isWalking || isRunning || isJumping || isAttackingInProgress));

        UpdateAnimation(isWalking, isRunning, isJumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumlahLompatan = 0;
        }
    }

    private void UpdateAnimation(bool isWalking, bool isRunning, bool isJumping)
    {
        animator.SetBool("IsWalking", isWalking);
        animator.SetBool("IsRunning", isRunning);
        animator.SetBool("IsJumping", isJumping);
    }
}
