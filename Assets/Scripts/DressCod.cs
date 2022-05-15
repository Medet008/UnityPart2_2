using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DressCod : MonoBehaviour
{
    [SerializeField] private Renderer Renderer;
    [SerializeField] private Renderer Hemlet;
   
    
    public void SetDressCod(Material material)
    {
        Renderer.material = material;
     
    }

    public void SetHemlet(Material material)
    {
        Hemlet.material = material;
    }
  
  
}
