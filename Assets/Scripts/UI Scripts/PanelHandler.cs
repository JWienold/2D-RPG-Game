using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public void SetPanel(int p)
    {
        switch (p)
        {
            case 0:
                p1.SetActive(true);
                p2.SetActive(false);
                p3.SetActive(false);
                p4.SetActive(false);
                break;
            case 1:
                p1.SetActive(false);
                p2.SetActive(true);
                p3.SetActive(false);
                p4.SetActive(false);
                break;
            case 2:
                p1.SetActive(false);
                p2.SetActive(false);
                p3.SetActive(true);
                p4.SetActive(false);
                break;
            case 3:
                p1.SetActive(false);
                p2.SetActive(false);
                p3.SetActive(false);
                p4.SetActive(true);
                break;
            default:
                break;
        }
    }
}
