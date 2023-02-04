using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    bool isItemInRange;
    [SerializeField] float canGetRadius;
    [SerializeField] LayerMask itemLayer;
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
        if (isItemInRange)
        {
            Debug.Log("Touching");

        }
    }
}
