using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Credit_Scene : MonoBehaviour
{
    // Tag untuk player
    public string playerTag = "Player";

    // Nama scene yang akan di-load
    public string sceneToLoad = "Credit_Scene";

    // Update is called once per frame
    void Update()
    {
        // Periksa jika objek menyentuh player dan tombol Enter ditekan
        if (Input.GetKeyDown(KeyCode.Space) && IsPlayerTouching())
        {
            // Load scene "Credit_Scene"
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    // Fungsi untuk memeriksa apakah objek menyentuh player
    bool IsPlayerTouching()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(playerTag))
            {
                return true;
            }
        }

        return false;
    }
}
