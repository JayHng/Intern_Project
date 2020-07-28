using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingText : MonoBehaviour{
//     [SerializeField] private Text text;

//     // Start is called before the first frame update
//     void Start()
//     {
//         text = GetComponent<Text>();
//         StartBlinking();
//     }

//     IEnumerator Blink(){
//         while(true){
//              switch (text.color.a.ToString())
//             {
//                 case "0":
//                     text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
//                     yield return new WaitForSeconds(0.5f);
//                     break;
//                 case "1":
//                     text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
//                     yield return new WaitForSeconds(0.5f);
//                     break;
//             }
//         }
//     }
//     void StartBlinking(){
//         StartCoroutine("Blink");
//     }
//     void StopBlinking(){
//         StopCoroutine("Blink");
//     }

 private float timer;
    private bool blink = true;
    // Start is called before the first frame update
    void Start()
    {
        blink = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (blink)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5)
            {
                GetComponent<Text>().enabled = true;
            }
            if (timer >= 1)
            {
                GetComponent<Text>().enabled = false;
                timer = 0f;
            }
        }
    }
}
