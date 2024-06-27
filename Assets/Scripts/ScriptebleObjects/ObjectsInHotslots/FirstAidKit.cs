using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class HealingItem : MonoBehaviour
{
    public Item item;
    public bool instant;
    public float timeToHeal;
    private float currentTime;
    public float healAmount;
    private float FinalhealAmount;
    private void Awake()
    {
        currentTime = timeToHeal;
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            //anim.SetBool("Aim", true);

            if (instant)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("A1");
                    Heal();
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    currentTime -= Time.deltaTime;
                    if (currentTime <= 0f)
                    {
                        Heal();
                        currentTime = timeToHeal;
                        Debug.Log("A2");
                    }
                }
                else
                {
                    currentTime = timeToHeal;
                }
            }
        }
        else
        {
            //anim.SetBool("Aim", false);
        }
    }
    private void Heal()
    {
        float modifier = PlayerStats.PlayerStatsInstance.Intelligence.GetValue() / 10f;
        FinalhealAmount = healAmount * modifier + healAmount;

        PlayerHealth.PlayerHealthInstance.currentHealth += FinalhealAmount;
        PlayerHealth.PlayerHealthInstance.UpdateUI();

        Inventory.InventoryInstance.itemUsedOnce();

        Debug.Log("Heals " + FinalhealAmount);
    }
}
