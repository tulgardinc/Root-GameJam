using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    Vector3 target;
    Rigidbody2D rb;
    [SerializeField] float speed;
    [HideInInspector] public bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isMoving)
        {
            Vector2 velocity = (target - transform.position).normalized;
            Vector2 position2D = new Vector2(transform.position.x, transform.position.y);
            rb.MovePosition(position2D + velocity * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, target) < 0.2f)
            {
                transform.position = target;
                isMoving = false;
            }
        }
    }

    public void StartMove(Vector3 target)
    {
        this.target = target;
        isMoving = true;
    }
}
