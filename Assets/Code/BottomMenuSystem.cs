using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomMenuSystem : MonoBehaviour
{
    public static BottomMenuSystem Instance { get; private set; }


    public GameObject[] menus;
    public int startMenu = -1;
    private int currentMenu = -1;


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

    }

    //if its -1 then hide menu
    public void ShowMenu(int id)
    {
        if (id == -1)
        {
            if (currentMenu == -1)
                return; //already hidden

            menus[id].SetActive(false);
            return;
        }

        //Make sure we have a valid id
        if (id >= menus.Length)
        {
            Debug.LogError("Menu Id is too large: " + id);
            return;
        }

        //hide previous menu, show new one
        if(currentMenu != -1)
        {
            menus[currentMenu].SetActive(false);
            currentMenu = id;
            menus[currentMenu].SetActive(true);
        }
    }
}
