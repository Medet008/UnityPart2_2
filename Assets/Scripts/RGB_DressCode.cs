using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RGB_DressCode : MonoBehaviour
{
    [SerializeField] private MeshRenderer serjant; 
    [SerializeField] private Slider red;
    [SerializeField] private Slider green;
    [SerializeField] private Slider blue;
    public void SetEditRgba()
    {
        Color color = serjant.material.color;
        color.r = red.value;
        color.g = green.value;
        color.b = blue.value;
        serjant.material.color = color;
        serjant.material.SetColor("_Emissoin", color); 
    }
}
