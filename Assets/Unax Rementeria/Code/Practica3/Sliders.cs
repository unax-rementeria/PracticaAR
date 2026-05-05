using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sliders : MonoBehaviour
{
    public Slider tiempo;
    public float sliderTime;
    public Slider Vertical;
    public float sliderVert;
    public Slider Horizontal;
    public float sliderHoriz;
    public Button button;
    public GameObject canvas;
    public GameObject canvas2;
    public TMP_Text textoTiempo;
    public TMP_Text textoVert;
    public TMP_Text textoHoriz;
    public Score score;

    void Start()
    {
        tiempo.onValueChanged.AddListener(Tiempo);
        Vertical.onValueChanged.AddListener(Vert);
        Horizontal.onValueChanged.AddListener(Horiz);
        button.onClick.AddListener(Acceptar);

        textoTiempo.text = "Tiempo de busqueda: " + sliderTime;
        textoVert.text = "Gemas verticales: " + sliderVert;
        textoHoriz.text = "Gemas horizontales: "  + sliderHoriz;

        canvas2.SetActive(false);
    }

    void Tiempo(float value)
    {
        sliderTime = value;    
        textoTiempo.text = "Tiempo de busqueda: " + sliderTime;
    }

    void Vert(float value)
    {
        sliderVert = value;
        textoVert.text = "Gemas verticales: " + sliderVert;
    }
    void Horiz(float value)
    {
        sliderHoriz = value;
        textoHoriz.text = "Gemas horizontales: "  + sliderHoriz;
    }
    void Acceptar()
    {
        score.timeRemaining = sliderTime;
        canvas.SetActive(false);
        canvas2.SetActive(true);
    }
}