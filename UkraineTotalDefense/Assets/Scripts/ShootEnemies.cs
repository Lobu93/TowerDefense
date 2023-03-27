using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour
{
    public List<GameObject> enemiesInRange;
    private float lastShotTime;
    private UnitData unitData;
    private GameObject currentSpawnPoint;

    public GameObject pointUp, pointUpDiagonalRight, pointRight, pointDownDiagonalRight;
    public GameObject pointDown, pointDownDiagonalLeft, pointLeft, pointUpDiagonalLeft;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();
        lastShotTime = Time.time;
        unitData = gameObject.GetComponentInChildren<UnitData>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = null;
        
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }
        
        if (target != null)
        {
            Vector3 direction = gameObject.transform.position - target.transform.position;
            RotateIntoEnemyDirection(direction);

            if (Time.time - lastShotTime > unitData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
        }

    }

    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Add(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate += OnEnemyDestroy;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            enemiesInRange.Remove(other.gameObject);
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            del.enemyDelegate -= OnEnemyDestroy;
        }
    }

    void Shoot(Collider2D target)
    {
        GameObject bulletPrefab = unitData.CurrentLevel.bullet;

        Vector3 startPosition = currentSpawnPoint.transform.position;
        Vector3 targetPosition = target.transform.position;
        startPosition.z = bulletPrefab.transform.position.z;
        targetPosition.z = bulletPrefab.transform.position.z;

        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        newBullet.transform.position = startPosition;
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        bulletComp.target = target.gameObject;
        bulletComp.startPosition = startPosition;
        bulletComp.targetPosition = targetPosition;

        // Calculate Rotation
        //Vector3 direction = gameObject.transform.position - target.transform.position;
        //gameObject.transform.rotation = Quaternion.AngleAxis(
        //    Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
        //    new Vector3(0, 0, 1));

        Animator animator = unitData.CurrentLevel.visualization.GetComponent<Animator>();
        animator.SetTrigger("fireShot");
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void RotateIntoEnemyDirection(Vector3 direction)
    {
        
        //gameObject.transform.rotation = Quaternion.AngleAxis(
        //    Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
        //    new Vector3(0, 0, 1));

        //Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;
        //Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;
        //Vector3 newDirection = (newEndPosition - newStartPosition);

        //float x = newDirection.x;
        //float y = newDirection.y;
        //float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;

        //GameObject sprite = gameObject.transform.Find("Sprite").gameObject;

        float rotationAngle = Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI;

        Animator animator = unitData.CurrentLevel.visualization.GetComponent<Animator>();

        // Up
        if (rotationAngle > -112.5f && rotationAngle < -67.5f)
        {
            animator.SetBool("isUp", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointUp;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isUp", false);
        }

        // Up Diagonal Right
        if (rotationAngle > -157.5f && rotationAngle < -112.5f)
        {
            animator.SetBool("isUpDiagonalRight", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointUpDiagonalRight;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isUpDiagonalRight", false);
        }

        // Right
        if ((rotationAngle > -180.0f && rotationAngle < -157.5f) || (rotationAngle > 157.5f && rotationAngle < 180.0f))
        {
            animator.SetBool("isRight", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointRight;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isRight", false);
        }

        // Down Diagonal Right
        if (rotationAngle > 112.5f && rotationAngle < 157.5f)
        {
            animator.SetBool("isDownDiagonalRight", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointDownDiagonalRight;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isDownDiagonalRight", false);
        }


        // Down 
        if (rotationAngle > 67.5f && rotationAngle < 112.5f)
        {
            animator.SetBool("isDown", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointDown;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isDown", false);
        }

        // Down Diagonal Left
        if (rotationAngle > 22.5f && rotationAngle < 67.5f)
        {
            animator.SetBool("isDownDiagonalLeft", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointDownDiagonalLeft;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isDownDiagonalLeft", false);
        }

        // Left
        if (rotationAngle > -22.5f && rotationAngle < 22.5f)
        {
            animator.SetBool("isLeft", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointLeft;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isLeft", false);
        }

        // Up Diagonal Left
        if (rotationAngle > 67.5f && rotationAngle < 22.5f)
        {
            animator.SetBool("isUpDiagonalLeft", true);
            animator.SetBool("isIdle", false);
            currentSpawnPoint = pointUpDiagonalLeft;
        }
        else
        {
            animator.SetBool("isIdle", true);
            animator.SetBool("isUpDiagonalLeft", false);
        }

        // Down Right 
        /* if (rotationAngle > -45.0f && rotationAngle < -15.0f)
        {
            animator.SetBool("isDownLeft", true);
        }

        // Down Left 
        if (rotationAngle > -165.0f && rotationAngle < -135.0f)
        {
            animator.SetBool("isDownLeft", false);
        } */

        // print("rotationAngle: " + rotationAngle);

        // sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);
    }
}
