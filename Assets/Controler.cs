using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controler : MonoBehaviour
{

    public int selectedShip;
    public void shipcos(int ship)
    {
        selectedShip = ship;
        Debug.Log(selectedShip);
    }
       
    void Update()
    {
        
    }
}
