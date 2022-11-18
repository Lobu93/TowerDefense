using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour
{
    public delegate void EnemyDelegate(GameObject enemy);
    public EnemyDelegate enemyDelegate;

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
}
