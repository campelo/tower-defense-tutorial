using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int StartMoney = 300;

    public static int Lives;
    public int StartLives = 20;

    public static int Rounds;
    // Start is called before the first frame update
    void Start()
    {
        Money = StartMoney;
        Lives = StartLives;
        Rounds = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static void RemoveLive()
    {
        Lives -= 1;
    }

    public static void MoneyGain(int amount)
    {
        Money += amount;
    }
}
