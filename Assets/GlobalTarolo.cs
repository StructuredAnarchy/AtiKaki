using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalTarolo : MonoBehaviour
{
    public static GlobalTarolo Instance;
    // Start is called before the first frame update
    public TextMeshProUGUI timerText; // Egy UI Text, amivel megjelenítjük az idõt a képernyõn
    private float startTime;
    private bool finished = false;
    public GameObject Player;
    public bool BossFight;

    public GameObject End;

    internal void Gyoztel()
    {
        End.SetActive(true);
        StartCoroutine(Vege());
    }
    private IEnumerator Vege()
    {
        yield return new WaitForSeconds(4f);
        Application.Quit();
    }
    void Start()
    {
        startTime = Time.time;
        Instance = this;
        this.BossFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
            return;

        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;
    }
    public void Finish()
    {
        finished = true;
        timerText.color = Color.yellow; // Opcionális: Színváltás, ha vége a pályának
    }
}
