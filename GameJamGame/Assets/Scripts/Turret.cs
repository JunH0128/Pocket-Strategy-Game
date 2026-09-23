using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float laneWidth = 2f;
    [SerializeField] private float bps = 1f; // Bullet per second
    [SerializeField] private float rotationSpeed = 5f;

   

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }


        if (target != null)
        {
            RotateTowardsTarget();
            
            // Debug to see what's happening
            Debug.Log($"Target: {target.name}, Distance: {Vector2.Distance(target.position, transform.position)}");
        }
        else
        {
            Debug.Log("No target found");
        }
        

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("Bullet fired at target: " + target.name);

        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }

    private void FindTarget()
    {
        Vector2 boxCenter =
        (Vector2)transform.position + Vector2.down * (targetingRange / 2f);

        Collider2D[] colliders = Physics2D.OverlapBoxAll(
            boxCenter,
            new Vector2(laneWidth, targetingRange),
            0f,
            enemyMask
        );

        if (colliders.Length > 0)
        {
            target = colliders[0].transform;
        }
        else
        {
            target = null;
        }
    }

    private bool CheckTargetIsInRange()
    {
        if (target == null) return false;

        float xDistance = Mathf.Abs(target.position.x - transform.position.x);

        // How far BELOW the turret the zombie is
        float yDistance = transform.position.y - target.position.y;

        return xDistance <= laneWidth / 2f &&
            yDistance >= 0f &&
            yDistance <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        //if (target == null) return;

        // Calculate direction to target
        //Vector2 direction = (target.position - turretRotationPoint.position).normalized;
        
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg + 90f;
        // angle -= 90f; // For sprites pointing up
        // angle += 90f; // For sprites pointing down
        
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        
        turretRotationPoint.rotation = targetRotation;
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;

        Vector3 boxCenter =
            transform.position + Vector3.down * (targetingRange / 2f);

        Gizmos.DrawWireCube(
            boxCenter,
            new Vector3(laneWidth, targetingRange, 0f)
        );
    }
}