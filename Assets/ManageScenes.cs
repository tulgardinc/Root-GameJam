using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageScenes : MonoBehaviour
{

    [SerializeField] string nextSceneName;
    bool isPLayerHere;
    bool isTreeHere;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        isPLayerHere = false;
        isTreeHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPLayerHere && isTreeHere) 
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("player"))
        {
            isPLayerHere = true;
        }
        if (collision.transform.tag.Equals("flower"))
        {
            isTreeHere = true;
        }
    }
}
