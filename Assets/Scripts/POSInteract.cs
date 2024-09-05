using UnityEngine;
using UnityEngine.UI;

public class POSInteract : MonoBehaviour
{
    public GameObject popupCanvas; // Referenca na Canvas pop-up prozora
    public InputField inputField; // Referenca na InputField za unos godine
    public Button submitButton; // Referenca na Submit gumb
    public string correctYear = "1976"; // Točna godina za unos

    private Renderer posRenderer;
    private Material[] originalMaterials;
    private bool isYearCorrect = false;

    void Start()
    {
        posRenderer = GetComponent<Renderer>();
        originalMaterials = posRenderer.materials;  // Spremite originalne materijale

        // Sakrij pop-up prozor na početku
        popupCanvas.SetActive(false);

        // Dodaj listener za Submit gumb
        submitButton.onClick.AddListener(SubmitYear);

        // Dodaj listener za EndEdit događaj
        inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
    }

    void OnMouseOver()
    {
        Debug.Log("Miš je iznad objekta");
        Material[] newMaterials = posRenderer.materials;
        newMaterials[0] = newMaterials[1]; // Switchanje na drugi materijal
        posRenderer.materials = newMaterials;
    }

    void OnMouseExit()
    {
        Debug.Log("Miš je izašao iz objekta");
        posRenderer.materials = originalMaterials; // Vratite originalne materijale
    }

    void OnMouseDown()
    {
        Debug.Log("POS uređaj je kliknut!");

        // Prikaži pop-up prozor kada se klikne na POS uređaj
        popupCanvas.SetActive(true);

        // Fokusiraj InputField i aktiviraj ga
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void SubmitYear()
    {
        string enteredYear = inputField.text;
        Debug.Log("Unesena godina: " + enteredYear);

        if (enteredYear == correctYear)
        {
            Debug.Log("Točna godina! Vrata su otključana.");
            isYearCorrect = true;
            popupCanvas.SetActive(false);
        }
        else
        {
            Debug.Log("Neispravna godina! Pokušaj ponovno.");
            inputField.text = "";
        }
    }

    private void OnInputFieldEndEdit(string input)
    {
        Debug.Log("Enter stisnut!!");
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            SubmitYear();
        }
    }

    public bool IsYearCorrect()
    {
        return isYearCorrect;
    }
        void Update()
    {
        // Provjera pritiska ESC tipke
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Sakrij Canvas
            popupCanvas.SetActive(false);
        }
    }
}
