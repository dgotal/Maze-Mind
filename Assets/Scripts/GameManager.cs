using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public GameObject[] characterPrefabs; // Prefabovi svih likova

    void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt("SelectedCharacterIndex");
        Instantiate(characterPrefabs[selectedIndex], transform.position, transform.rotation);
    }
}


