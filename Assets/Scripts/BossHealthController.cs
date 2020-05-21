using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthController : MonoBehaviour
{
    public int health = 500;

    public void UpdateScore()
    {
        GetComponent<Text>().text = "Boss Health: " + health;
    }
}
