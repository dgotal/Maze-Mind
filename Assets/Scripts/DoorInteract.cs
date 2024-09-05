using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DoorInteract : MonoBehaviour
{
    public POSInteract posInteract; // Referenca na POSInteract skriptu
    private Renderer doorRenderer;
    private Material[] originalMaterials;
    private Animator doorAnimator; // Animator za otvaranje vrata
    private bool isDoorOpen = false; // Provjera je li vrata već otvorena

    void Start()
    {
        doorRenderer = GetComponent<Renderer>();
        originalMaterials = doorRenderer.materials;  // Spremite originalne materijale
        doorAnimator = GetComponent<Animator>();

        // Onemogućite Animator na početku
        if (doorAnimator != null)
        {
            doorAnimator.enabled = false;
        }
    }

    void OnMouseOver()
    {
        if (posInteract.IsYearCorrect() && !isDoorOpen)
        {
            Debug.Log("Miš je iznad vrata i godina je točna!");
            Material[] newMaterials = doorRenderer.materials;
            if (newMaterials.Length > 1)
            {
                newMaterials[0] = newMaterials[1]; // Switchanje na drugi materijal
            }
            doorRenderer.materials = newMaterials;
        }
    }

    void OnMouseExit()
    {
        Debug.Log("Miš je izašao iz vrata");
        doorRenderer.materials = originalMaterials; // Vratite originalne materijale
    }

    void OnMouseDown()
    {
        if (posInteract.IsYearCorrect() && !isDoorOpen)
        {
            isDoorOpen = true;
            Debug.Log("Godina je točna! Otvaranje vrata...");

            // Omogućite Animator kada je godina točna
            if (doorAnimator != null)
            {
                doorAnimator.enabled = true;
                doorAnimator.SetTrigger("OpenDoor");
                StartCoroutine(WaitForAnimationAndLoadScene(doorAnimator.GetCurrentAnimatorStateInfo(0).length));
            }
        }
    }

    private IEnumerator WaitForAnimationAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
