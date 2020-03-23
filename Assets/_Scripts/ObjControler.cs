using DG.Tweening;
using UnityEngine;
using System.Collections;

public class ObjControler : MonoBehaviour
{
    private Manipulate targetObj;
    private FindSelectables camScript;
    private GameObject donePiece;
    private GameObject materialPanel;
    private CanvasGroup interactCanvas;

    void Start()
    {
        interactCanvas = GetComponent<CanvasGroup>();
        interactCanvas.interactable = true;

        donePiece = GameObject.Find("DONEpiece");
        donePiece.SetActive(false);
        donePiece.GetComponent<CanvasGroup>().interactable = false;

        materialPanel = GameObject.Find("MaterialPanel");
        materialPanel.SetActive(false);
    }

    public void SetTarget(Transform obj, FindSelectables from)
    {
        if (obj)
            targetObj = obj.gameObject.GetComponent<Manipulate>();
        else
            targetObj = null;
        camScript = from;
    }

    public void ChangeSize(float amount)
    {
        if (targetObj)
            targetObj.Size(amount);
    }
    public void ResetSize()
    {
        if (targetObj)
            targetObj.SizeReset();
    }
    public void ChangeRotation(float amount)
    {
        if (targetObj)
            targetObj.Rotation(amount);
    }
    public void ResetRotation()
    {
        if (targetObj)
            targetObj.RotationReset();
    }
    public void ChangePostion(bool what)
    {
        if (targetObj)
            camScript.ILikeToMoveIt(what);
    }
    public void ResetPosition()
    {
        if (targetObj)
            targetObj.PositionReset();
    }
    public void CloneSelected()
    {
        if (targetObj)
            camScript.DollyIt();
    }
    public void MenuFadeDONE(bool enable)
    {
        StartCoroutine(FadeDone(enable));
    }

    IEnumerator FadeDone(bool enable)
    {
        interactCanvas.interactable = !enable;
        donePiece.GetComponent<CanvasGroup>().interactable = enable;
        if (enable) {
            interactCanvas.DOFade(0, .3f);
            yield return new WaitForSeconds(0.4f);
            interactCanvas.gameObject.SetActive(false);
        }
        else {
            interactCanvas.DOFade(1, .3f);
        }
        camScript.ExternalFade(donePiece, enable);
    }

    public void MenuFadeMATERIAL(bool enable)
    {
        interactCanvas.interactable = !enable;

        camScript.ExternalFade(materialPanel, enable);
        if (enable) {
            materialPanel.GetComponent<MatControler>().SetupObj(targetObj.gameObject);
            interactCanvas.DOFade(0, .3f);
        }
        else
            interactCanvas.DOFade(1, .3f);
    }

    public void BackToNavigate()
    {
        camScript.BringItBack();
    }
}
