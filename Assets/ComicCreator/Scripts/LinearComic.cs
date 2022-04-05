using UnityEngine;
using UnityEngine.UI;

public class LinearComic : MonoBehaviour
{
    int width;
    int height;
    HorizontalLayoutGroup layout;    

    void Start()
    {
        layout = GetComponent<HorizontalLayoutGroup>();
        RectTransform rect = GetComponent<RectTransform>();
        width = (int)(rect.rect.width + layout.spacing);
        height = (int)(rect.rect.height);
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ButtonMoveRight()
    {
        transform.localPosition -= new Vector3(width, 0, 0);
    }
}