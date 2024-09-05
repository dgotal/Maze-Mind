using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    public Image characterImage;
    public TMP_Text descriptionText;
    public Button nextButton;
    public Button backButton;
    public Button playButton;

    public Sprite[] characterSprites;
    public string[] characterDescriptions;

    private int currentIndex = 0;

    void Start()
    {
        characterDescriptions = new string[]
        {
            "She’s as bright as a summer day with a smile that can melt ice. This blonde is full of energy, ready to conquer the world and look amazing while doing it!",
            "Dark hair, sharp gaze, and an attitude that says, 'Don't mess with me.' He’s mysterious, smart, and always one step ahead – the perfect hero for any adventure!",
            "Fiery hair, even fierier spirit! This redhead is the life of the party, with a smile that lights up the darkest of days. With him, there's never a dull moment!",
            "Full of energy and always ready for trouble! His red hair might be bright, but his wit shines even more. This teen knows how to have fun, and he's not afraid to show it!",
            "Smart, insightful, and with red hair that adds an extra dose of cool. This doctor knows all about medicine and how to bring a smile to your face!",
            "Sweet, wise, and always ready for an adventure! She may have seen it all, but her spirit is as young as ever. With her by your side, every journey becomes nostalgic and incredibly fun!",
            "Supermom with style! This blonde isn’t just a parent, she’s a fighter. With her around, every challenge turns into child’s play!",
            "Bold, brave, and with a smile that hides a thousand secrets. This policewoman isn’t just here to keep the peace – she’s ready for any challenge, and she’ll do it with flair!"
        };

        UpdateCharacterDisplay();
    }

    public void NextCharacter()
    {
        currentIndex = (currentIndex + 1) % characterSprites.Length;
        UpdateCharacterDisplay();
    }

    public void PreviousCharacter()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = characterSprites.Length - 1;
        UpdateCharacterDisplay();
    }

void UpdateCharacterDisplay()
{
    if (characterSprites.Length == 0 || characterDescriptions.Length == 0) return;

    characterImage.sprite = characterSprites[currentIndex];
    descriptionText.text = characterDescriptions[currentIndex];
}



    public void PlayGame()
    {
        PlayerPrefs.SetInt("SelectedCharacterIndex", currentIndex);
        SceneManager.LoadScene("RestaurantScene");
    }
}
