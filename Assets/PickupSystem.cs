using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    Collider2D itemInRange;
    [SerializeField] float canGetRadius;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] Transform itemPos;
    bool itemPicked = false;
    [SerializeField] Animator itemGetAnimation;

    GameObject pickedObject;

    void Update()
    {
        Collider2D candidate = Physics2D.OverlapCircle(transform.position, canGetRadius, itemLayer);
        if (candidate && candidate.gameObject.tag == "flower")
        {
            itemInRange = candidate;
        }

        if (itemInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (itemPicked)
                {
                    itemGetAnimation.SetBool("isPicked", false);
                    itemPicked = false;
                    pickedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    pickedObject = null;
                }
                else
                {
                    itemGetAnimation.SetBool("isPicked", true);
                    pickedObject = itemInRange.gameObject;
                    Debug.Log("picked");
                    itemPicked = true;
                    pickedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                }
            }

            if (itemPicked)
            {
                pickedObject.transform.position = itemPos.transform.position;
            }
        }
    }

}
