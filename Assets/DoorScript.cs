using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DoorScript : MonoBehaviour
{


    [SerializeField] ButtonsScript buttonToConnect;

    [SerializeField] GameObject referencePointUp;
    [SerializeField] GameObject referencePointDown;
    [SerializeField] float doorSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        if(buttonToConnect.gameObject.tag.Equals("pressure plate"))
        {
            ButtonsScript.onPressed += OpenDoor;
            ButtonsScript.onRelease += CloseDoor;


        }
        if (buttonToConnect.gameObject.tag.Equals("button"))
        {
            ButtonsScript.onClicked += tempOpen;

        }
    }
    private void OnDisable()
    {
        if (buttonToConnect.gameObject.tag.Equals("pressure plate"))
        {
            ButtonsScript.onPressed -= OpenDoor;

        }
        if (buttonToConnect.gameObject.tag.Equals("button"))
        {
            ButtonsScript.onClicked -= tempOpen;

        }
    }

    void OpenDoor()
    {
     
        float step = doorSpeed * Time.deltaTime;
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, referencePointUp.transform.position, step);
        
        
      
    }
    void CloseDoor()
    {
        float step = doorSpeed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, referencePointDown.transform.position, step);
    }

    void tempOpen()
    {
        float step = doorSpeed * Time.deltaTime;
      
        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, referencePointUp.transform.position, step);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
