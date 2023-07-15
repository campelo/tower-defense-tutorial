using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TextMeshProUGUI _moneyText;

    // Start is called before the first frame update
    void Start()
    {
        _moneyText = GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        _moneyText.text = $"${PlayerStats.Money}";
    }
}
