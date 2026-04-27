using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class Scenes : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button Exit;
    public GameObject canvas;

    public ARPlaneManager arPlaneManager;
    public ARTrackedImageManager arImageManager;
    public ARRaycastManager arRaycastManager;
    public GameObject planos;
    public GameObject imagen;

    private int? currentGameplayScene = null;

    void Start()
    {
        button1.onClick.AddListener(Planos);
        button2.onClick.AddListener(Imagenes);
        Exit.onClick.AddListener(VolverInicio);
        canvas.SetActive(true);
        planos.SetActive(false);
        imagen.SetActive(false);

        SetPlaneMode(false);
        SetImageMode(false);
    }

    public void Planos()
    {
        canvas.SetActive(false);
        planos.SetActive(true);
        SetPlaneMode(true);      
        SetImageMode(false);     
        StartCoroutine(SwitchScene(1));
    }

    public void Imagenes()
    {
        canvas.SetActive(false);
        imagen.SetActive(true);
        SetPlaneMode(false);     
        SetImageMode(true);      
        StartCoroutine(SwitchScene(2));
    }

    public void VolverInicio()
    {
        Transform xrOrigin = arPlaneManager.transform;

        while (xrOrigin.parent != null)
        {
            xrOrigin = xrOrigin.parent;
        }

        Transform trackables = xrOrigin.Find("Trackables");

        if (trackables != null)
        {
            for (int i = trackables.childCount - 1; i >= 0; i--)
            {
                Destroy(trackables.GetChild(i).gameObject);
            }
        }
        
        canvas.SetActive(true);
        planos.SetActive(false);
        imagen.SetActive(false);
        SetPlaneMode(false);     
        SetImageMode(false);

        if (currentGameplayScene.HasValue)
        {
            StartCoroutine(UnloadCurrentScene());
        }
    }


    private void SetPlaneMode(bool active)
    {
        if (arPlaneManager != null)  
        {
            arPlaneManager.enabled  = active;
        }
        if (arRaycastManager != null) 
        {
            arRaycastManager.enabled = active;
        }
    }

    private void SetImageMode(bool active)
    {
        if (arImageManager != null) 
        {
            arImageManager.enabled = active;
        }
    }

    private IEnumerator SwitchScene(int newSceneIndex)
    {
        if (currentGameplayScene.HasValue)
        {
            yield return SceneManager.UnloadSceneAsync(currentGameplayScene.Value);
        }

        yield return SceneManager.LoadSceneAsync(newSceneIndex, LoadSceneMode.Additive);

        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(newSceneIndex));
        currentGameplayScene = newSceneIndex;
    }

    private IEnumerator UnloadCurrentScene()
    {
        yield return SceneManager.UnloadSceneAsync(currentGameplayScene.Value);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        currentGameplayScene = null;
    }
}