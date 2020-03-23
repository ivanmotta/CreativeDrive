using UnityEngine.UI;
using UnityEngine;

public class MatControler : MonoBehaviour
{
    public Image[] colors;
    public Image[] textures;
    private MaterialOpts targetOptions;

    public void SetupObj(GameObject target)
    {
        targetOptions = target.GetComponent<MaterialOpts>();
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i].color = targetOptions.colors[i];

            Texture2D tempText = targetOptions.textures[i];
            Rect rec = new Rect(0, 0, tempText.width, tempText.height);
            textures[i].sprite = Sprite.Create(tempText, rec, new Vector2(.5f, .5f));
        }
    }

    public void SetColor(int index)
    {
        targetOptions.ChangeColor(index);
    }

    public void SetTexture(int index)
    {
        targetOptions.ChangeTexture(index);
    }
}
