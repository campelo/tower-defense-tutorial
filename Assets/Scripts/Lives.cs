using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private TextMeshProUGUI _textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();    
    }

    // Update is called once per frame
    void Update()
    {
        _textMeshPro.text = $"Lives: {PlayerStats.Lives}";
    }
}
