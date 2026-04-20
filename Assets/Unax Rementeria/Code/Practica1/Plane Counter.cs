using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PlaneCounter : MonoBehaviour
{
    GameObject Trackables;
    Transform Tracks;   
    public Canvas canvas;
    public TMP_Text texto;
    public Button button;
    public List<GameObject> prefabs;
    public TMP_Dropdown dropdown;
    public GameObject SelectedPrefab;
    void Start()
    {
        Trackables = GameObject.Find("Trackables");
        Tracks = Trackables.transform;
        SelectedPrefab = prefabs[0];

        dropdown.onValueChanged.AddListener(OnDropdownChange);
    }

    
    void FixedUpdate()
    {
        int childCount = Tracks.childCount;
        Debug.Log(childCount);
        texto.text = "Planes detected: " + childCount;
    }

    public void DeleteAllPlanes()
    {
        foreach (Transform child in Tracks)
        {
            Destroy(child.gameObject);
        }
    }

    public void OnDropdownChange(int index)
    {
        SelectedPrefab = prefabs[index];
        Debug.Log("Selected prefab: " + SelectedPrefab.name);
    }
}
