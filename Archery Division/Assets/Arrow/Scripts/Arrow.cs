using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private float damage = 0;

    [SerializeField]
    private float torque = 0;

    [SerializeField]
    private Rigidbody rb = null;

    private string enemyTag = "";

    private bool didHit = false;

    public void SetEnemyTag(string enemyTag)
    {
        this.enemyTag = enemyTag;
    }

    public void Fly(Vector3 force)
    {
        rb.isKinematic = false;
        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(transform.right * torque);
        transform.SetParent(null);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (didHit) return;
        didHit = true;
        var action = collider.GetComponent<GroundCollision>();
        if (collider.CompareTag(enemyTag))
        {
            var health = collider.GetComponent<HealthController>();
            health.ApplyDamage(damage);
            //gameObject.SetActive(false);
            health.ArrowAction("The arrow hit the enemy", didHit);
        }
        else
        {
            action.ArrowAction("The arrow hit the ground");
        }
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        transform.SetParent(collider.transform);    
    }
}