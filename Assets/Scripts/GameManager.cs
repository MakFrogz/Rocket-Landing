using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public  static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    public void NextLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextLevelIndex = 0;
        }
        else
        {
            PlayerPrefs.SetInt("level", nextLevelIndex);
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }

    public void LoadLevel()
    {
        int levelIndex = PlayerPrefs.GetInt("level", 0);
        SceneManager.LoadScene(levelIndex);
    }

}
