using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    Text text;
    PlayerScript player;


    private void Start()
    {
        text = GetComponent<Text>();
        player = FindObjectOfType<PlayerScript>();
    }

    private void Update()
    {
        text.text = player.GetHealthInfo().ToString();
    }




}
