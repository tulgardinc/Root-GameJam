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
        Debug.Log(isTreeHere);
        Debug.Log(isPLayerHere);
        if (isPLayerHere && isTreeHere) 
        {
            Debug.Log("Changing Scene");
            SceneManager.LoadScene(nextSceneName);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            isPLayerHere = false;
        }
        if (collision.transform.tag.Equals("flower"))
        {
            isTreeHere = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Player"))
        {
            isPLayerHere = true;
        }
        if (collision.transform.tag.Equals("flower"))
        {
            isTreeHere = true;
        }
    }
  
}
