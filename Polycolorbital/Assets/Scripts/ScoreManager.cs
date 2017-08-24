using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    private int level;

    private float time;
    private string minutes; 
    private string seconds;

    public Text levelText;
    public Text timerText;

    void Start ()
    {
        level = 1;
	}

    void ResetTimer ()
    {
        Time.timeScale = 0;
    }

    void ResetLevel (int level)
    {
        this.level = level;
    }
	
	void Update ()
    {
        levelText.text = level.ToString();
        timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

        time = Time.timeSinceLevelLoad;
        minutes = Mathf.Floor(time / 60).ToString("00");
        seconds = (time % 60).ToString("00");
    }
}
