using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatusMenuController : MonoBehaviour
{
    public UI_StatusBar healthBar;
    public UI_StatusBar manaBar;
 
    // Start is called before the first frame update
    void Start()
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.healthChanged += healthBar.ChangeValue;
        playerStats.manaChanged += manaBar.ChangeValue;
    }

}
