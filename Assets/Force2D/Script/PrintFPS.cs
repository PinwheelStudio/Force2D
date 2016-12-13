//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//public class PrintFPS : MonoBehaviour {

//    public Text output;

//    int max = 0;
//    int min = int.MaxValue;
//    int fps;
//    int frameCount = 0;

//    public void Start()
//    {
//        InvokeRepeating("Print", 3, 1);
//    }

//	void Update () {
//        //t.text = ((int)(1 / Time.deltaTime)).ToString();
//        if (IsInvoking("Print")) 
//            frameCount++;
//        if (Input.touchCount == 3)
//            min = int.MaxValue;
//	}



//    void Print()
//    {

//        if (frameCount < 100 && frameCount > max) 
//            max = frameCount;
//        if (frameCount > 0 && frameCount < min) 
//            min = frameCount;

//        output.text = "min FPS: " + min + "\ncurrent FPS: " + frameCount + "\nmax FPS: " + max;

//        frameCount = 0;
//    }
//}
using UnityEngine;
using UnityEngine.UI;

public class PrintFPS : MonoBehaviour
{

    // Attach this to a GUIText to make a frames/second indicator.
    //
    // It calculates frames/second over each updateInterval,
    // so the display does not keep changing wildly.
    //
    // It is also fairly accurate at very low FPS counts (<10).
    // We do this not by simply counting frames per interval, but
    // by accumulating FPS for each frame. This way we end up with
    // correct overall FPS even if the interval renders something like
    // 5.5 frames.
    public Text t;
    public float updateInterval = 0.5F;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    void Start()
    {
        if (!t)
        {
            Debug.Log("UtilityFramesPerSecond needs a GUIText component!");
            enabled = false;
            return;
        }
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            t.text = format;

            //if (fps < 30)
            //    guiText.material.color = Color.yellow;
            //else
            //    if (fps < 10)
            //    guiText.material.color = Color.red;
            //else
            //    guiText.material.color = Color.green;
            //	DebugConsole.Log(format,level);
            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}