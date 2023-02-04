using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayStuff : MonoBehaviour
{
    public CarryTree Object;
    public bool isItPicked;
    GameObject carriedObject;
    Vector2 vector;
    public LayerMask ground;


    // Start is called before the first frame update
    void Start()
    {
        isItPicked = false;
    }

    void FixedUpdate()
    {
        carriedObject = Object.pickedItem;
    }
    // Update is called once per frame
    void Update()
    {


        if (isItPicked)
        {
            Debug.Log(carriedObject.name);
            Debug.Log(carriedObject.transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(carriedObject.transform.position, transform.TransformDirection(Vector2.down), 10f, ground);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 10f, Color.red);
            if (hit)
            {
                Debug.Log(hit.transform.position.y);
                float posY = (float)(hit.transform.position.y + 0.3f);
                Debug.Log(posY);
                vector = new Vector2(transform.position.x, posY);
                carriedObject.transform.position = vector;
                isItPicked = false;
            }
        }
    }
}
