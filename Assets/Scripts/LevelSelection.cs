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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

    public void OpenAgentSelect()
    {
        SceneManager.LoadScene(1);
    }

    public void SetSelectedLevel(int id)
    {
        selectedLevel = id;
        UpdateLevelText();
    }
}
