using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoTypeText : MonoBehaviour
{
    public Text displayedTxt;
    public Text inputTxt;
    public float textSpeed;

    private string outputTxt = null;
    private int i = -1;

    private bool finishTyping;

    private void Awake() {
        StartCoroutine("autoRepeat");
    }

    private string TypeText(string text){
        //Typing Text
        i++;
        char[] characters = text.ToCharArray();
        outputTxt = outputTxt + characters[i].ToString();

        //Check if the text finishes typing 
        if(i == (characters.Length - 1))
        {
            finishTyping = true;
        }
        return outputTxt;
    }

    IEnumerator autoRepeat(){
        while(true){
            yield return new WaitForSeconds(textSpeed);

            if(!finishTyping){
                displayedTxt.text = TypeText(inputTxt.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(finishTyping){
            StartCoroutine("disablePanel");
        }
    }

    IEnumerator disablePanel(){
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
}
