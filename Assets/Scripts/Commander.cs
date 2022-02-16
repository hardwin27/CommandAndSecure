using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Commander : MonoBehaviour
{
    [SerializeField] private float maxHealth = 20f;
    private float health;

    [SerializeField] private Canvas commanderUI;
    [SerializeField] private Slider healthBar;

    private void Awake()
    {
        commanderUI.worldCamera = GameManager.Instance.GetMainCamera();
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = health;
    }

    public void TakingDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
        if (health <= 0)
        {
            GameManager.Instance.SetIsWin(false);
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }

    public float GetRemainingHealthPercentage()
    {
        return health / maxHealth;
    }
}
