using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OfficeSystem : MonoBehaviour
{
    public string stage1name;

    public GameObject Map;
    [SerializeField] private PlayerMovement pm;
    private bool isRunning;
    private bool officeChecker = false;
    private int state = 0;
    private bool isTrigger;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.E) && !isRunning && officeChecker == false&&isTrigger==true)
            {
                isRunning = true;
                officeChecker = true;
                pm.objectOnOff = true;
                 Map.SetActive(true);
            }

        if (Input.GetKeyDown(KeyCode.E)&& !isRunning && officeChecker == true)
        {
            isRunning = true;
            officeChecker = false;
            pm.objectOnOff = false;
            Map.SetActive(false);

        }

        if (officeChecker == true)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (Input.GetKeyDown(KeyCode.Alpha0 + i))
                {
                    if (i==1)
                    {
                        SceneManager.LoadScene(stage1name);
                    }
                    if (i == 2)
                    {
                        Debug.Log("스테이지 2로 이동");
                    }
                    if (i == 3)
                    {
                        Debug.Log("스테이지 3로 이동");
                    }
                    if (i == 4)
                    {
                        Debug.Log("스테이지 4로 이동");
                    }
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        isTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }


}
