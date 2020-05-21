using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public int health = 300;

    public void UpdateScore()
    {
        GetComponent<Text>().text = "Health: " + health;
    }
}
