using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class ZombieAI : MonoBehaviour
{
    public Transform player;  // Referenca na igrača
    public float attackRange = 2f;  // Udaljenost na kojoj zombi može napasti
    public float attackCooldown = 1f;  // Vrijeme između napada
    public int damage = 10;  // Koliko štete nanosi zombi

    private NavMeshAgent agent;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();  // Dobivanje reference na NavMeshAgent
    }

    void Update()
    {
        // Kretanje zombija prema igraču
        agent.SetDestination(player.position);

        // Provjera udaljenosti između zombija i igrača
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Ako je zombi unutar napadačke udaljenosti i nije u cooldownu
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Zombi napada!");

        // Ovdje možeš dodati animaciju napada ili zvuk
        // ...

        // Ovdje se može implementirati logika za nanošenje štete igraču
        // PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        // if (playerHealth != null)
        // {
        //     playerHealth.TakeDamage(damage);
        // }

        // Postavi cooldown
        nextAttackTime = Time.time + attackCooldown;
        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    // Vizualizacija napadačke zone
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
