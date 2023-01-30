using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SO_UIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    void Start()
    {
        uiTextValue.text = soInt.value.ToString();
    }

    void Update()
    {
        uiTextValue.text = soInt.value.ToString();
    }
}
