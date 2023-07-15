using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject Prefab;
    public int Cost;

    public int UpgradeCost;
    public GameObject UpgradePrefab;

    public int GetSellAmount()
    {
        return Cost / 2;
    }
}
