using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Death Effects Object")] [SerializeField] GameObject deathFX;
    [SerializeField] float enemyHealth = 50f;
    // Start is called before the first frame update
    void OnParticleCollision(GameObject other)
    {
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        deathFX.SetActive(true);
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        Invoke("DestroyEnemyGameObject", 1f);
    }
    private void DestroyEnemyGameObject() //Referenced by string in DestroyEnemy method.
    {
        Destroy(gameObject);
    }
}
