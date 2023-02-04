using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

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

    List<RootWithDirection> roots = new List<RootWithDirection>();
    const float stepSize = 1f;

    private void DeleteLastRoot()
    {
        Destroy(roots[roots.Count - 1].root);
        roots.RemoveAt(roots.Count - 1);
    }

    private void Update()
    {
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
                    roots.Add(AddRoot(lastRoot.root.transform.position, Vector3.down, verticalSprite));
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
                    roots.Add(AddRoot(lastRoot.root.transform.position, Vector3.left, horizontalSprite));
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
                    roots.Add(AddRoot(lastRoot.root.transform.position, Vector3.right, horizontalSprite));
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
                    roots.Add(AddRoot(lastRoot.root.transform.position, Vector3.up, verticalSprite));
                }
            }
        }
    }

    private RootWithDirection AddRoot(Vector3 pos, Vector3 dir, Sprite sprite)
    {
        GameObject newRoot = Instantiate(rootPrefab, pos + dir * stepSize, Quaternion.identity);
        newRoot.transform.parent = transform;
        newRoot.GetComponent<SpriteRenderer>().sprite = sprite;
        return new RootWithDirection(dir, newRoot);
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