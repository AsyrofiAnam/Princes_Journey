using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    public float destroyDelay = 2f; // Delay sebelum objek di-nonaktifkan

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weak Point")
        {
            // Nonaktifkan objek "Weak Point" setelah penundaan
            StartCoroutine(DisableObjectAfterDelay(collision.gameObject));
        }
    }

    IEnumerator DisableObjectAfterDelay(GameObject objToDisable)
    {
        yield return new WaitForSeconds(destroyDelay);

        // Nonaktifkan objek setelah penundaan
        objToDisable.SetActive(false);
    }
}
