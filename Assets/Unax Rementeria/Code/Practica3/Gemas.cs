using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Gemas : MonoBehaviour
{
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private GameObject Prefab;
    public Button start;
    public GameObject canvas;
    public Sliders slider;
    public Score score;
    public ARPlaneManager arPlaneManager;

    private List<ARPlane> horizontalPlanes = new List<ARPlane>();
    private List<ARPlane> verticalPlanes   = new List<ARPlane>();

    void Start()
    {
        start.onClick.AddListener(comenzar);
    }

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        foreach (ARPlane plane in args.added)
            AddPlaneToList(plane);

        foreach (ARPlane plane in args.updated)
            ReclassifyPlane(plane);

        foreach (ARPlane plane in args.removed)
            RemovePlaneFromLists(plane);
    }

    void AddPlaneToList(ARPlane plane)
    {
        if (IsHorizontal(plane))
            horizontalPlanes.Add(plane);
        else if (IsVertical(plane))
            verticalPlanes.Add(plane);
    }

    void ReclassifyPlane(ARPlane plane)
    {
        // Remove from both lists first, then re-add to the correct one
        RemovePlaneFromLists(plane);
        AddPlaneToList(plane);
    }

    void RemovePlaneFromLists(ARPlane plane)
    {
        horizontalPlanes.Remove(plane);
        verticalPlanes.Remove(plane);
    }

    bool IsHorizontal(ARPlane plane) => plane.alignment == PlaneAlignment.HorizontalUp || plane.alignment == PlaneAlignment.HorizontalDown;

    bool IsVertical(ARPlane plane) => plane.alignment == PlaneAlignment.Vertical;

    
    public void SpawnOnRandomHorizontalPlane()
    {
        SpawnOnRandomPlane(horizontalPlanes, Prefab);
    }

    public void SpawnOnRandomVerticalPlane()
    {
        SpawnOnRandomPlane(verticalPlanes, Prefab);
    }

    void SpawnOnRandomPlane(List<ARPlane> planes, GameObject prefab)
    {
        if (planes.Count == 0)
        {
            Debug.LogWarning("No planes available in this list yet.");
            return;
        }

        // Pick a random plane from the list
        ARPlane randomPlane = planes[Random.Range(0, planes.Count)];

        // Align the prefab rotation to the plane's surface
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector3.up, randomPlane.normal);

        Instantiate(prefab, randomPlane.transform.position, Quaternion.identity);
    }

    public void comenzar()
    {
        arPlaneManager.enabled = false;
        for (int i = 0; i < slider.sliderVert; i++)
        {
            SpawnOnRandomVerticalPlane();
        }
        for (int i = 0; i < slider.sliderHoriz; i++)
        {
            SpawnOnRandomHorizontalPlane();
        }
        canvas.SetActive(false);
        score.scoreCanvas.SetActive(true);
    }

}
