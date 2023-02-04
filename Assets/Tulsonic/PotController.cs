using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotController : MonoBehaviour
{
    [SerializeField] GameObject rootPoint;
    [SerializeField] GameObject rootPrefab;
    List<RootWithDirection> roots = new List<RootWithDirection>();

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("HorizontalPot"), Input.GetAxis("VerticalPot")).normalized;

        RootWithDirection lastRoot = roots.Count > 0 ? roots[roots.Count - 1] : null;

        print(input);

        if (input.magnitude != 0)
        {
            if (lastRoot != null)
            {
                if (input == lastRoot.direction)
                {
                    lastRoot.Extend();
                }
                else if (input == lastRoot.direction)
                {
                    lastRoot.Shrink();
                    if (lastRoot.IsDead())
                    {
                        Destroy(lastRoot.root);
                        roots.RemoveAt(roots.Count - 1);
                    }
                }
                else
                {
                    roots.Add(new RootWithDirection(input, Instantiate(
                        rootPoint,
                        lastRoot.root.transform.position + new Vector3(input.x, input.y, 0),
                        Quaternion.LookRotation(input, Vector3.forward)
                        )
                    ));
                }
            }
            else if (input.y < 0)
            {
                roots.Add(new RootWithDirection(input, Instantiate(
                    rootPoint,
                    transform.position + Vector3.down * 0.5f,
                    Quaternion.LookRotation(Vector3.down, Vector3.forward)
                    )
                ));
            }
        }
    }

}

public class RootWithDirection
{
    public GameObject root;
    public Vector2 direction;
    const float stepSize = 1f;

    public RootWithDirection(Vector2 direction, GameObject root)
    {
        this.direction = direction;
    }

    public void Extend()
    {
        root.transform.localScale += new Vector3(direction.x, direction.y, 0) * stepSize;
    }

    public void Shrink()
    {
        root.transform.localScale -= new Vector3(direction.x, direction.y, 0) * stepSize;
    }

    public bool IsDead()
    {
        return root.transform.localScale.x == 0 || root.transform.localScale.y == 0;
    }
}