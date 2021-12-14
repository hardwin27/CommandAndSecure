using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public static LevelSelection instance;

    public int selectedLevel { private set; get; } = 0;
    [SerializeField] Text levelText;

    private void Awake()
    {
        instance = this;

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        UpdateLevelText();
    }

    private void UpdateLevelText()
    {
        levelText.text = selectedLevel.ToString();
    }

    public void UpdateSelectedLevel(int addValue)
    {
        selectedLevel += addValue;
        if(selectedLevel < 0 || selectedLevel >3)
        {
            selectedLevel -= addValue;
            return;
        }
        UpdateLevelText();
    }

    public void OpenLevel()
    {
        SceneManager.LoadScene(1);
    }
}
