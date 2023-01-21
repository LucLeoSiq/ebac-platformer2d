using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SO_UIIntUpdate : MonoBehaviour
{
    public SOInt soInt;
    public TextMeshProUGUI uiTextValue;

    // Start is called before the first frame update
    void Start()
    {
        uiTextValue.text = soInt.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        uiTextValue.text = soInt.value.ToString();
    }
}
