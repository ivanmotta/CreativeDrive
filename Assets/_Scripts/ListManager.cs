using UnityEngine.UI;
using UnityEngine;
using TMPro;
//using DG.Tweening;

public class ListManager : MonoBehaviour
{
    public Transform listParent;
    public GameObject menuItemPrefab;
    private Image img;
    private TextMeshProUGUI txt;
    private bool open = true;

    private void Start()
    {
        open = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            MoveMenu();
    }

    public void SetupItem(Transform item)
    {
        GameObject instance = Instantiate(menuItemPrefab, listParent);
        txt = instance.GetComponentInChildren<TextMeshProUGUI>();
        txt.text = item.GetComponent<ObjContentHolder>().objName;

        img = instance.transform.Find("Img").GetComponent<Image>();
        img.sprite = thumbnail(item);
    }

    public Sprite thumbnail(Transform source)
    {
        //RuntimePreviewGenerator.BackgroundColor = Color.black;
        //RuntimePreviewGenerator.OrthographicMode = true;
        //Texture2D tempText = RuntimePreviewGenerator.GenerateModelPreview(source);
        Texture2D tempText = source.GetComponent<ObjContentHolder>().objThumb;

        Rect rec = new Rect(0, 0, tempText.width, tempText.height);
        return Sprite.Create(tempText, rec, new Vector2(.5f, .5f));
    }

    public void MoveMenu()
    {
        if (open)
            GetComponent<Animation>().Play("MenuHide");
        if (!open)
            GetComponent<Animation>().Play("MenuUnhide");
        open = !open;
    }
}
