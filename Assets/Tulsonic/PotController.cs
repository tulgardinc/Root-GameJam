using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PotController : MonoBehaviour
{
    [SerializeField] GameObject rootPoint;
    [SerializeField] GameObject rootPrefab;
    [SerializeField] Sprite horizontalSprite;
    [SerializeField] Sprite verticalSprite;
    [SerializeField] Sprite topLeftSprite;
    [SerializeField] Sprite topRightSprite;
    [SerializeField] Sprite bottomLeftSprite;
    [SerializeField] Sprite bottomRightSprite;
    [SerializeField] float pushSpeed;

    List<RootWithDirection> roots = new List<RootWithDirection>();
    const float stepSize = 1f;
    bool isBeingPushedUp = false;
    Vector3 pushTarget;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void DeleteLastRoot()
    {
        Destroy(roots[roots.Count - 1].root);
        roots.RemoveAt(roots.Count - 1);
        var lastRoot = roots[roots.Count - 1];
        if (lastRoot.direction == Vector3.right || lastRoot.direction == Vector3.left)
        {
            lastRoot.root.GetComponent<SpriteRenderer>().sprite = horizontalSprite;
        }
        else if (lastRoot.direction == Vector3.up || lastRoot.direction == Vector3.down)
        {
            lastRoot.root.GetComponent<SpriteRenderer>().sprite = verticalSprite;
        }
    }

    private void HandleBeingPushed()
    {
        if (isBeingPushedUp)
        {
            Vector3 difference = pushTarget - transform.position;
            Vector2 velocity = new Vector2(difference.x, difference.y).normalized;
            rb.MovePosition(new Vector2(transform.position.x, transform.position.y) + velocity * pushSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, pushTarget) < 0.2f)
            {
                transform.position = pushTarget;
                isBeingPushedUp = false;
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    private void Update()
    {

        HandleBeingPushed();

        if (roots.Count == 0)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                roots.Add(new RootWithDirection(Vector3.down, Instantiate(rootPrefab, rootPoint.transform.position, Quaternion.identity)));
                roots[roots.Count - 1].root.transform.parent = transform;
                roots[roots.Count - 1].root.GetComponent<SpriteRenderer>().sprite = verticalSprite;
            }
        }
        else
        {
            RootWithDirection lastRoot = roots[roots.Count - 1];
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (lastRoot.direction == Vector3.up)
                {
                    DeleteLastRoot();
                }
                else
                {
                    print(lastRoot.root);
                    if (AddNewRoot(lastRoot.root.transform.position, Vector3.down, verticalSprite))
                    {
                        if (lastRoot.direction == Vector3.right)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = topRightSprite;
                        }
                        else if (lastRoot.direction == Vector3.left)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = topLeftSprite;
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (lastRoot.direction == Vector3.right)
                {
                    DeleteLastRoot();
                }
                else
                {
                    if (AddNewRoot(lastRoot.root.transform.position, Vector3.left, horizontalSprite))
                    {
                        if (lastRoot.direction == Vector3.up)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = topRightSprite;
                        }
                        else if (lastRoot.direction == Vector3.down)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = bottomRightSprite;
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (lastRoot.direction == Vector3.left)
                {
                    DeleteLastRoot();
                }
                else
                {
                    if (AddNewRoot(lastRoot.root.transform.position, Vector3.right, horizontalSprite))
                    {

                        if (lastRoot.direction == Vector3.up)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = topLeftSprite;
                        }
                        else if (lastRoot.direction == Vector3.down)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = bottomLeftSprite;
                        }
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (lastRoot.direction == Vector3.down)
                {
                    DeleteLastRoot();
                }
                else
                {
                    if (AddNewRoot(lastRoot.root.transform.position, Vector3.up, verticalSprite))
                    {
                        if (lastRoot.direction == Vector3.right)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = bottomRightSprite;
                        }
                        else if (lastRoot.direction == Vector3.left)
                        {
                            lastRoot.root.GetComponent<SpriteRenderer>().sprite = bottomLeftSprite;
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            for (int i = roots.Count - 1; i >= 0; i--)
            {
                Destroy(roots[i].root);
                roots.RemoveAt(i);
            }
        }

    }

    private bool AddNewRoot(Vector3 pos, Vector3 dir, Sprite sprite)
    {
        if (isBeingPushedUp) return false;

        Collider2D col = IsTileEmpty(pos + dir * (stepSize));
        if (col == null || col.gameObject.layer != LayerMask.NameToLayer("Root"))
        {
            if (col != null && col.gameObject.tag == "Pushable")
            {
                var innerCol = IsTileEmpty(col.gameObject.transform.position + dir * (stepSize));
                Pushable pushable = col.gameObject.GetComponent<Pushable>();
                if (!innerCol && !pushable.isMoving)
                {
                    col.gameObject.GetComponent<Pushable>().StartMove(col.gameObject.transform.position + dir * stepSize);
                }
                else
                {
                    return false;
                }
            }
            print(pos);
            if (col != null && col.gameObject.layer == LayerMask.NameToLayer("Default"))
            {
                if (dir != Vector3.down)
                {
                    return false;
                }
                else
                {
                    isBeingPushedUp = true;
                    rb.bodyType = RigidbodyType2D.Kinematic;
                    pushTarget = transform.position + Vector3.up * stepSize;
                }
            }
            GameObject newRoot = Instantiate(rootPrefab, pos + dir * stepSize, Quaternion.identity);

            newRoot.transform.parent = transform;
            newRoot.GetComponent<SpriteRenderer>().sprite = sprite;
            roots.Add(new RootWithDirection(dir, newRoot));
            return true;
        }
        return false;
    }

    private Collider2D IsTileEmpty(Vector3 pos)
    {
        return Physics2D.OverlapBox(pos, new Vector2(0.5f, 0.5f), 0);
    }

}

public class RootWithDirection
{
    public GameObject root;
    const float stepSize = 1f;
    public Vector3 direction;

    public RootWithDirection(Vector3 direction, GameObject root)
    {
        this.direction = direction;
        this.root = root;
    }
}