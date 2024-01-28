using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public int damage = 1;
    private Animator animator;
    public float destroyDelay = 2f; // Delay sebelum objek di-nonaktifkan

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Dapatkan komponen PlayerLife dari objek pemain
            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();

            // Pastikan bahwa playerLife tidak null sebelum mencoba mengurangkan darah
            if (playerLife != null)
            {
                // Periksa apakah pemain menyentuh bagian atas collider
                if (IsTopCollision(collision))
                {
                    // Set animator IsWalk false dan IsDead true
                    SetAnimatorStates(false, true);

                    // Nonaktifkan collider agar tidak berinteraksi lagi
                    GetComponent<Collider2D>().enabled = false;

                    // Menunggu sebelum menonaktifkan objek
                    StartCoroutine(DisableObjectAfterDelay());
                }
                else
                {
                    // Jika tidak menyentuh bagian atas, kurangkan darah pemain
                    playerLife.TakeDamage(damage);
                }
            }
        }
    }

    // Fungsi untuk mengecek apakah collision terjadi pada bagian atas collider
    private bool IsTopCollision(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        Vector2 normal = contact.normal;
        return normal.y < -0.5f; // Sesuaikan nilai threshold sesuai kebutuhan
    }

    // Fungsi untuk mengatur animator states
    private void SetAnimatorStates(bool isWalking, bool isDead)
    {
        animator.SetBool("IsWalk", isWalking);
        animator.SetBool("IsDead", isDead);
    }

    // Fungsi untuk menonaktifkan objek setelah penundaan
    IEnumerator DisableObjectAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);

        // Nonaktifkan objek setelah penundaan
        gameObject.SetActive(false);
    }
}
