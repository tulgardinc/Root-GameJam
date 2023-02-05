using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    Collider2D itemInRange;
    [SerializeField] float canGetRadius;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] Transform itemPos;
    bool itemPicked = false;
    [SerializeField] Animator itemGetAnimation;

    CharacterMovementScript movementScr;

    GameObject pickedObject;

    private void Start()
    {
        movementScr = GetComponent<CharacterMovementScript>();
    }

    void Update()
    {
        Collider2D candidate = Physics2D.OverlapCircle(transform.position, canGetRadius, itemLayer);
        if (candidate && candidate.gameObject.tag == "flower")
        {
            itemInRange = candidate;
        }
        else
        {
            itemInRange = null;
        }

        if (itemInRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && !movementScr.isClimbing)
            {
                if (itemPicked)
                {
                    itemGetAnimation.SetBool("isPicked", false);
                    itemPicked = false;
                    pickedObject.GetComponent<Rigidbody2D>().gravityScale = 1;
                    pickedObject.GetComponent<PotController>().isBeingHeld = false;
                    pickedObject = null;
                }
                else
                {
                    itemGetAnimation.SetBool("isPicked", true);
                    pickedObject = itemInRange.gameObject;
                    Debug.Log("picked");
                    pickedObject.GetComponent<PotController>().isBeingHeld = true;
                    pickedObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                    itemPicked = true;
                }
            }

            if (itemPicked)
            {
                pickedObject.transform.position = itemPos.transform.position;
            }
        }
    }

}
