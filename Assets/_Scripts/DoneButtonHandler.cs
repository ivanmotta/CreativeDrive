using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButtonHandler : MonoBehaviour
{
    public GameObject interact;
    public GameObject player;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            DoneIt();
    }

    public void DoneIt()
    {
        interact.gameObject.SetActive(true);
        interact.GetComponent<ObjControler>().MenuFadeDONE(false);
        interact.GetComponent<ObjControler>().ChangePostion(false);
        player.GetComponent<PlayerMove>().Recenter();
    }
}
