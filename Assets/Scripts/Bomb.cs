using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour
{
    public float countdown = 3f;  // Vrijeme do eksplozije
    public float explosionRadius = 5f;  // Radijus eksplozije
    public float explosionForce = 700f;  // Sila eksplozije
    public LayerMask destructibleLayer;  // Layer za uništive objekte
    public GameObject explosionEffect;  // Prefab za vizualni efekt eksplozije
    public Material activatedMaterial;  // Materijal za bombu kad se aktivira

    // Zvukovi
    public AudioClip activationSound;    // Zvuk aktivacije/odbrojavanja
    public AudioClip explosionSound;     // Zvuk eksplozije
    public AudioSource audioSource;      // AudioSource komponenta za puštanje zvukova

    private Renderer bombRenderer;       // Renderer za promjenu materijala
    private Material[] originalMaterials; // Originalni materijali bombe
    private bool hasExploded = false;    // Provjera je li bomba već eksplodirala
    private bool isActivated = false;    // Provjera je li bomba aktivirana

    void Start()
    {
        bombRenderer = GetComponent<Renderer>();
        originalMaterials = bombRenderer.materials;  // Spremi originalne materijale
    }

    void OnMouseDown()
    {
        if (!isActivated)
        {
            ActivateBomb();
        }
    }

    void ActivateBomb()
    {
        // Promijeni materijal bombe
        Material[] newMaterials = bombRenderer.materials;
        newMaterials[0] = activatedMaterial; // Promjena materijala na aktivirani
        bombRenderer.materials = newMaterials;

        // Pusti zvuk aktivacije/odbrojavanja
        if (audioSource != null && activationSound != null)
        {
            audioSource.PlayOneShot(activationSound);
        }

        // Postavi bombu kao aktiviranu
        isActivated = true;

        // Pokreni odbrojavanje za eksploziju
        StartCoroutine(ExplosionCountdown());
    }

    IEnumerator ExplosionCountdown()
    {
        yield return new WaitForSeconds(countdown);
        Explode();
    }

    void Explode()
    {
        if (hasExploded) return;  // Ako je već eksplodirala, ne eksplodiraj opet

        hasExploded = true;

        // Pusti zvuk eksplozije
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        // Prikaz vizualnog efekta eksplozije
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);
        }

        // Pronađi sve objekte unutar radijusa eksplozije
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, destructibleLayer);

        foreach (Collider nearbyObject in colliders)
        {
            // Primijeni silu eksplozije na objekte s Rigidbodyjem
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }

            // Uništi specifične objekte (npr. zid)
            if (nearbyObject.CompareTag("Destructible"))
            {
                Destroy(nearbyObject.gameObject);
            }
        }

        // Uništi bombu nakon eksplozije
        Destroy(gameObject);
    }

    void OnMouseExit()
    {
        // Vraćanje na originalni materijal nakon izlaska miša, ako nije aktivirana
        if (!isActivated)
        {
            bombRenderer.materials = originalMaterials;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Vizualizacija eksplozije u editoru
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
