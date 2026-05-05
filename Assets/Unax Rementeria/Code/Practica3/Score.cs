using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Score : MonoBehaviour
{
    public float totalScore = 0;
    public GameObject scoreCanvas;
    public TMP_Text score;
    public TMP_Text time; 
    public TMP_Text Win; 
    public Sliders slider;

    [SerializeField] private Button startButton;
    [SerializeField] public float startTime; 

    public float timeRemaining;
    private bool isRunning;

    void Start()
    {
        scoreCanvas.SetActive(false);
        //timeRemaining = slider.sliderTime;
        startButton.onClick.AddListener(StartStop);
        UpdateDisplay();
    }

    void Update()
    {
        if (!isRunning) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            OnCountdownFinished();
        }
        Debug.Log("Score: "+totalScore);
        UpdateDisplay();

        if(totalScore >= (slider.sliderHoriz + slider.sliderVert))
        {
            Win.text = "HAS GANADO!";
        }
    }

    public void StartStop()
    {
        isRunning = true;
    }

    void OnCountdownFinished()
    {
        Win.text = "Se acabo el tiempo!";
        Debug.Log("Se acabo el tiempo!");
    }

    void UpdateDisplay()
    {
        time.text = "Tiempo:" + (int)timeRemaining;
        score.text = "Score: " + totalScore + "/" + (slider.sliderHoriz + slider.sliderVert);
    }
}
