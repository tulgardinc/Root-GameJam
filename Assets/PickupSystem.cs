using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    bool isItemInRange;
    [SerializeField] float canGetRadius;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] Transform itemPos;
    bool itemPicked = false;
    bool isClicked = false;
    

    GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        isItemInRange = Physics2D.OverlapCircle(transform.position, canGetRadius, itemLayer);



    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(isItemInRange);
        if (isItemInRange)
        {
            Debug.Log("inRange");
            if (Input.GetKeyDown(KeyCode.R) && !isClicked)
            {
                isClicked= true;
                Debug.Log("picked");
                itemPicked = true;
                temp.GetComponent<Rigidbody2D>().gravityScale = 0;
               
               
            } else if (Input.GetKeyDown(KeyCode.R) && isClicked)
            {
                isClicked = false;
                itemPicked = false;
                temp.GetComponent<Rigidbody2D>().gravityScale = 1;
            }

            if (itemPicked)
            {
                temp.transform.position = itemPos.transform.position;
            }

        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isItemInRange)
        {
            int layer = collision.gameObject.layer;

            if (layer == LayerMask.NameToLayer("Tree"))
            {
                temp = collision.gameObject;
            }
            
        }
    }
}
