using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryTree : MonoBehaviour
{
    public float canGetRadius;
    public Transform itemCheck;
    public LayerMask itemLayer;
    bool isItemInRange;
    bool pickItem;
    public GameObject pickedItem;
    bool itemOnHand;
    public Transform itemLocation;
    public RayStuff rayManager;

    // Start is called before the first frame update
    void Start()
    {
        itemOnHand = false;
        pickItem = false;

    }

    void FixedUpdate()
    {
        isItemInRange = Physics2D.OverlapCircle(itemCheck.position, canGetRadius, itemLayer);



    }
    // Update is called once per frame
    void Update()
    {


        if (pickItem)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                pickedItem.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                itemOnHand = true;
                pickItem = false;
            }

        }
        if (itemOnHand)
        {
            pickedItem.transform.position = itemLocation.position;
            if (Input.GetKeyDown(KeyCode.R))
            {
                itemOnHand = false;
                rayManager.isItPicked = true;
                pickedItem.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

            }

        }


    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (isItemInRange)
        {
            pickItem = true;
            pickedItem = collision.gameObject;

        }

    }
}
