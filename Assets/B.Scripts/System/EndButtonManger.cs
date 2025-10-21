using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButtonManger : MonoBehaviour
{
    public string SceneName;

    public void Quit() => Application.Quit();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(SceneName);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Quit();
            Debug.Log("³ª°¡±â");
        }
    }
}
