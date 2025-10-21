using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonManger : MonoBehaviour
{


    public void Quit() => Application.Quit();

    public void OnClick()
    {
        Quit();
    }
}
