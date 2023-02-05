using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{

    public delegate void ButtonAction();
    public static event ButtonAction onClicked;
    public static event ButtonAction onPressed;
    public static event ButtonAction onRelease;

    bool firstTime;
    bool isPressing;
    bool isClicking;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnEnable()
    {
       
       firstTime = false;

    }
    // Update is called once per frame
    void Update()
    {
        if(isPressing && this.transform.tag.Equals("pressure plate")) 
        {
            if (onPressed != null)
            {
                onPressed();
            }
        }
        else if (!isPressing && this.transform.tag.Equals("pressure plate"))
        {
            if (onRelease != null)
            {
                onRelease();
            }
        }

        if (isClicking && this.transform.tag.Equals("button"))
        {
            if (onClicked != null)
            {
                onClicked();
            }
        }

        
   
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.tag.Equals("button") && !firstTime)
        {
            isClicking = true;
            firstTime = true;
            this.gameObject.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y - 1f, transform.lossyScale.z);
          

        }
        if (this.tag.Equals("pressure plate"))
        {
            isPressing = true;
            this.gameObject.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y - 1f, transform.lossyScale.z);

        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (this.tag.Equals("pressure plate"))
        {
            isPressing = false;
         
            this.gameObject.transform.lossyScale.Set(transform.lossyScale.x, transform.lossyScale.y + 1f, transform.lossyScale.z);

        }

    }

}
