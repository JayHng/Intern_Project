using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour
{
    [SerializeField] private Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartBlinking();
    }

    IEnumerator Blink(){
        while(true){
             switch (text.color.a.ToString())
            {
                case "0":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            }
            // text.text = "HELLO";
            // yield return new WaitForSeconds(1f);
            // text.text = "Hi";
            // yield return new WaitForSeconds(1f);

        }
    }
    void StartBlinking(){
        StartCoroutine("Blink");
    }
    void StopBlinking(){
        StopCoroutine("Blink");
    }
}
