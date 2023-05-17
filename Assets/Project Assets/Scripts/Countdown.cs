using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public int time = 5;
    public TextMeshProUGUI countDownText;
    //public Component componentToEnable;

    private void Start()
    {
        StartCoroutine(CountdownStart());
    }

    /*private void Update()
    {
        if(time <= 0)
        {
        }
    }*/

    private IEnumerator CountdownStart()
    {
        while(time > 0)
        {
            countDownText.text = time.ToString();   // convert the time to a string and set it to the text in unity

            yield return new WaitForSeconds(1f);    // wait for 1 second then go back to code to execute the rest

            this.time--;    
        }

        // once while loop is done, start game
        this.countDownText.text = "START!";

        yield return new WaitForSeconds(1f);

        this.countDownText.gameObject.SetActive(false);
    }

    public int GetTime()
    {
        return this.time;
    }
}
