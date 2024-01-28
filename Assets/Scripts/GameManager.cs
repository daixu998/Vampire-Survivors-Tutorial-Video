using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState { Playing, Paused, GameOver }


    /// <summary>
    /// 当前的游戏状态
    /// </summary>
    public GameState currentState;
    //以前的状态
    public GameState previousState;

    [Header("UI")]
    public GameObject pasuseScreen;
    public GameObject gameOverScreen;
    [Header("Current Stat Displays")]
    public Text currentHealthDisplay;
    public Text currentRecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectilespeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Displays")]
    public Image chosenCharaterImage;
    public Text chosenCharacterName;

    public Text levelReachedDisplay;

    public List<Image> chosenweaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    public bool isGameOver = false;




    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
        DisabelScreen();
    }
    void Update()
    {

        // TestSwitchState();
        switch (currentState)
        {
            case GameState.Playing:
                ChangeForPauseAndResume();
                break;
            case GameState.Paused:
                ChangeForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    DisplayResults();
                    Debug.Log("Game Over");
                }

                break;

            default:
                Debug.LogWarning("Unhandled game state: " + currentState);
                break;
        }
    }

    public void ChangeState(GameState gameState)
    {

        currentState = gameState;
    }

    void TestSwitchState()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState++;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            currentState--;
        }
    }

    public void PauseGame()
    {

        if (currentState != GameState.Paused)
        {

            previousState = currentState;
            pasuseScreen.SetActive(true);
            ChangeState(GameState.Paused);
            Time.timeScale = 0;
        }

    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            // currentState = previousState ;
            pasuseScreen.SetActive(false);
            ChangeState(previousState);
            DisabelScreen();
            Time.timeScale = 1;
        }
    }

    void ChangeForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisabelScreen()
    {

        pasuseScreen.SetActive(false);
        gameOverScreen.SetActive(false);

    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }
    //游戏结束黑屏
    public void DisplayResults()
    {
        gameOverScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject characterScriptableObject)
    {
        chosenCharaterImage.sprite = characterScriptableObject.Icon;
        chosenCharacterName.text = characterScriptableObject.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenweaponsAndPassiveItemsUI(List<Image> chosenweaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenweaponsData.Count != chosenweaponsUI.Count || chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Chosen weapons and passive items data lists have different lengths");
            return;
        }
        for (int i = 0; i < chosenweaponsUI.Count; i++)
        {
            if (chosenweaponsData[i].sprite)
            {
                chosenweaponsUI[i].enabled =true;
                chosenweaponsUI[i].sprite = chosenweaponsData[i].sprite;
            }
            else
            {
                chosenweaponsUI[i].enabled = false;

            }
        }
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled =true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;

            }
        }
    }
}
