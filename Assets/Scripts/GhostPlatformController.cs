using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPlatformController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            StartCoroutine(Disappear());
        }
    }

    IEnumerator Disappear() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
