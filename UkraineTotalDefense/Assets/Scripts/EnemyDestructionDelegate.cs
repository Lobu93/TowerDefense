using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour
{
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;

    [SerializeField] GameObject enemyExplosionFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (enemyDelegate != null)
        {
            enemyDelegate(gameObject);
        }
    }

    public void TriggerEnemyExplosionFX()
    {
        Transform explosionLocation = gameObject.transform;
        GameObject explosion = Instantiate(enemyExplosionFX, explosionLocation.position, explosionLocation.rotation);

        ParticleSystem particleSystem = enemyExplosionFX.GetComponent<ParticleSystem>();

        Destroy(explosion, particleSystem.main.duration);
    }
}
