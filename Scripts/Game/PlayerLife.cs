using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private int trapTouches = 0;
    private int maxTrapTouches = 3;
    private AudioManager audioManager;

    // Waktu penundaan sebelum memanggil GameOver
    public float gameOverDelay = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            HurtOrDie();
        }
        else if (collision.gameObject.CompareTag("Lava"))
        {
            Die(); // Panggil Die() langsung jika menyentuh Lava
        }
    }

    public void TakeDamage(int damage)
    {
        HurtOrDie();
    }

    private void HurtOrDie()
    {
        trapTouches++;

        if (trapTouches >= maxTrapTouches)
        {
            Die();
        }
        else
        {
            // Kurangi health saat terkena trap
            HealthManager.health--;
            UpdateHealthUI();

            animator.SetTrigger("Hurt");
            audioManager.PlaySFX(audioManager.hurt);
        }
    }

    private void Die()
    {
        audioManager.PlaySFX(audioManager.death);
        rb.bodyType = RigidbodyType2D.Static;
        animator.SetTrigger("Die");

        // Set health menjadi 0 saat player mati
        HealthManager.health = 0;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        // Cari objek dengan skrip HealthManager
        HealthManager healthManager = FindObjectOfType<HealthManager>();

        // Jika objek ditemukan, perbarui UI health
        if (healthManager != null)
        {
            healthManager.UpdateHealthUI();
        }
    }
}
