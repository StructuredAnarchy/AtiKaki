using System.Collections;
using UnityEngine;
using TMPro; // Make sure to include the TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class story : MonoBehaviour
{
    public TextMeshProUGUI[] textObjects; // Array to hold your TextMeshPro objects
    public Image im;
    public Image im2;
    public float displayTime = 2.0f; // Time to display text
    public float transitionTime = 1.0f; // Time for fade in and out

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)) SceneManager.LoadScene("KurvaMenu");
    }

    void Start()
    {
        StartCoroutine(DisplayTextSequence());
    }

    IEnumerator DisplayTextSequence()
    {
        bool fej = false;
        for (int i = 0; i < textObjects.Length; i++)
        {
            TextMeshProUGUI textObject = textObjects[i];
            if ( i == 3)
            {
                displayTime = 4;
            }
            if ( i == 5 ) displayTime = 6;
            if (fej) { im2.color = new Color(0, 0, 0, 255); }
            if(textObject.tag == "lovedek") { im.color = new Color(255,255,255,255); fej = true; }
            // Fade in
            StartCoroutine(FadeText(textObject, true));
            yield return new WaitForSeconds(transitionTime);

            // Wait for the display time
            yield return new WaitForSeconds(displayTime);

            // Fade out
            StartCoroutine(FadeText(textObject, false));
            yield return new WaitForSeconds(transitionTime); 
            
        }
        SceneManager.LoadScene("KurvaMenu");
    }

    IEnumerator FadeText(TextMeshProUGUI text, bool fadeIn)
    {
        // Fade in
        if (fadeIn)
        {
            for (float t = 0.01f; t < transitionTime; t += Time.deltaTime)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, t / transitionTime));
                yield return null;
            }
        }
        // Fade out
        else
        {
            for (float t = 0.01f; t < transitionTime; t += Time.deltaTime)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, t / transitionTime));
                yield return null;
            }
        }
    }
}