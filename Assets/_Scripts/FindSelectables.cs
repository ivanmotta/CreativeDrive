using DG.Tweening;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FindSelectables : MonoBehaviour
{
    public LayerMask whatToShootAt;
    public LayerMask whatToCancel;
    public float fadeMenuTime = 1f;
    public GameObject interactCanvas;
    public GameObject cursorCanvas;
    public GameObject thumbsCanvas;
    private Transform targetFound;
    private PlayerMove playerMovement;

    public Texture2D cursor;

    private bool finding = true;
    private bool moving = false;
    public GameObject placementIndicator;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMove>();
        targetFound = null;
        StartCoroutine(FadeMenus(interactCanvas, false));
        cursorCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
        Cursor.visible = false;

        finding = true;
        moving = false;
        placementIndicator.SetActive(false);
    }

    void FixedUpdate()
    {
        if (finding)
        {
            RaycastHit hit;
            Ray forwardRay = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(forwardRay, out hit, 15f, whatToShootAt.value))
            {
                targetFound = hit.collider.transform;
                targetFound.gameObject.GetComponent<GlowCollection>().enabled = true;
            }
            else if (targetFound != null && Physics.Raycast(forwardRay, out hit, 15f, whatToCancel.value))
            {
                targetFound.gameObject.GetComponent<GlowCollection>().enabled = false;
                targetFound = null;
            }
        }
        else if (moving)
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo, 15f, whatToCancel.value))
            {
                Vector3 goTo = new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
                placementIndicator.transform.position = goTo;
            }
        }
    }

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) && targetFound)
            {
                if (finding)
                {
                    playerMovement.RecordPosition();
                    playerMovement.enabled = false;
                    StartCoroutine(FadeMenus(interactCanvas, true));
                    interactCanvas.GetComponent<ObjControler>().SetTarget(targetFound, this);

                    cursorCanvas.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    finding = false;
                    moving = false;
                }
                else if (moving)
                {
                    targetFound.transform.position = placementIndicator.transform.position;
                    playerMovement.RecordPosition();
                    //Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }
    }

    public void BringItBack()
    {
        placementIndicator.SetActive(false);
        playerMovement.enabled = true;
        interactCanvas.GetComponent<ObjControler>().SetTarget(null, null);
        StartCoroutine(FadeMenus(interactCanvas, false));
        cursorCanvas.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        finding = true;
        moving = false;
    }

    public void ILikeToMoveIt(bool what)
    {
        //Cursor.visible = true;

        placementIndicator.SetActive(what);
        if (!what)
            playerMovement.Recenter();
        playerMovement.enabled = what;
        if (what)
            playerMovement.Recenter();
        moving = what;
    }

    public void DollyIt()
    {
        GameObject clone = Instantiate(targetFound.gameObject, targetFound.position, targetFound.rotation, GameObject.Find("BOSS").transform);
        clone.name = targetFound.gameObject.name;
        targetFound = clone.transform;
        interactCanvas.GetComponent<ObjControler>().SetTarget(targetFound, this);
        ILikeToMoveIt(true);

        thumbsCanvas.GetComponent<ListManager>().SetupItem(targetFound);
    }

    public void ExternalFade(GameObject who, bool enable)
    {
        StartCoroutine(FadeMenus(who, enable));
    }

    IEnumerator FadeMenus(GameObject who, bool enable)
    {
        float fullalpha = 1f;
        float zeroalpha = 0f;
        if (enable) {
            who.SetActive(true);
        }
        else {
            fullalpha = 0f;
            zeroalpha = 1f;
        }
        who.GetComponent<CanvasGroup>().alpha = zeroalpha;
        yield return new WaitForSeconds(0.1f);
        who.GetComponent<CanvasGroup>().DOFade(fullalpha, fadeMenuTime);
        yield return new WaitForSeconds(fadeMenuTime);
        if (!enable)
            who.SetActive(false);
    }
}
