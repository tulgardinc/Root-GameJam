using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsScript : MonoBehaviour
{

    public delegate void ButtonAction();
    public static event ButtonAction onClicked;
    public static event ButtonAction onPressed;
    public static event ButtonAction onRelease;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite openState;
    [SerializeField] Sprite closeState;

    [SerializeField] LayerMask clickLayer;
    [SerializeField]float canGetRadius;

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
       spriteRenderer.sprite = openState;


    }
    // Update is called once per frame
    void Update()
    {
        if(isPressing && this.transform.tag.Equals("pressure plate")) 
        {
            if (onPressed != null)
            {
                onPressed();
                spriteRenderer.sprite = closeState;
            }
        }
        else if (!isPressing && this.transform.tag.Equals("pressure plate"))
        {
            if (onRelease != null)
            {
                onRelease();
                spriteRenderer.sprite = openState;

            }
        }

        if (isClicking && this.transform.tag.Equals("button"))
        {
            if (onClicked != null)
            {
                onClicked();
                spriteRenderer.sprite = closeState;

            }
        }

        Collider2D candidate = Physics2D.OverlapCircle(transform.position, canGetRadius, clickLayer);
        if (candidate)
        {
            if (this.tag.Equals("button") && !firstTime)
            {
                isClicking = true;
                firstTime = true;


            }
            if (this.tag.Equals("pressure plate"))
            {
                isPressing = true;

            }

        }
        else
        {
            if (this.tag.Equals("pressure plate"))
            {
                isPressing = false;


            }
        }



    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (this.tag.Equals("button") && !firstTime)
        {
            isClicking = true;
            firstTime = true;
          

        }
        if (this.tag.Equals("pressure plate"))
        {
            isPressing = true;

        }


    }
    private void OnCollisionExit2D(Collision2D collision)
    {

        if (this.tag.Equals("pressure plate"))
        {
            isPressing = false;
         

        }

    }

}
