using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POSManager : MonoBehaviour
{
    public GameObject posCanvas;

    void Start()
    {
        // Sakrij POS UI pri početku igre
        posCanvas.SetActive(false);
    }

    public void ShowPOSCanvas()
    {
        // Prikaz POS UI-a kada se klikne na POS uređaj
        posCanvas.SetActive(true);
    }

    public void HidePOSCanvas()
    {
        // Sakrij POS UI nakon unosa ili zatvaranja
        posCanvas.SetActive(false);
    }
}
