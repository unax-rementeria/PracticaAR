using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrows : MonoBehaviour
{
    List<GameObject> flechas = new List<GameObject>();
    public int index = 0;

    void Start()
    {
        foreach (Transform flecha in transform)
        {
            flechas.Add(flecha.gameObject);
        }
        foreach (GameObject flecha in flechas)
        {
            flecha.SetActive(false);
        }

        flechas[index].SetActive(true);
    }

    public void NextArrow()
    {
        flechas[index].SetActive(false); 

        index++;

        if (index < flechas.Count)
        {
            flechas[index].SetActive(true); 
        }
    }
    
}
