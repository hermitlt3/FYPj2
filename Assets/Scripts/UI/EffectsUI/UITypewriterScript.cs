using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UITypewriterScript : MonoBehaviour
{
    Text theText;
    string story;

    public float delayTime = 0.125f;

    void Start()
    {
        theText = GetComponent<Text>();
        story = theText.text;
        theText.text = "";
    }

    public IEnumerator RunText(int numberOfLetters)
    {
        int temp = 0;
        foreach (char c in story)
        {
            temp++;
            if (temp > numberOfLetters)
            {
                break;
            }

            theText.text += c;
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void BackspaceText()
    {
        if(theText.text.Length > 0)
        {
            theText.text = theText.text.Remove(theText.text.Length - 1);
        }
    }

    public IEnumerator RemoveAllTextTimely()
    {
        while(theText.text.Length > 0)
        {
            theText.text = theText.text.Remove(theText.text.Length - 1);
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void ChangeText(string newText)
    {
        story = newText;
    }
}