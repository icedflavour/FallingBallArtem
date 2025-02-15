using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float fallingSpeed;
    [SerializeField] private float jumpSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Debug.Log(Physics.gravity.y);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.down * Time.deltaTime * fallingSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            rb.velocity = Vector3.up * fallingSpeed;
            rb.velocity -= Vector3.down * Time.deltaTime * 9.8f;
        }
    }
}
