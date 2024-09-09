using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObject : MonoBehaviour
{
    public float pushForce = 10f; // Snaga kojom se objekt gura
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Preuzmi ulaz korisnika (kretanje)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Izračunaj silu guranja
        Vector3 force = new Vector3(horizontal, 0, vertical) * pushForce;

        // Primijeni silu na stolicu
        rb.AddForce(force, ForceMode.Force);
    }
}
