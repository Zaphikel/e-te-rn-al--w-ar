using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public int damage;
    public Rigidbody2D rb;
    public Vector3 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(1000 * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(this.gameObject, 0.05f);
    }

}
