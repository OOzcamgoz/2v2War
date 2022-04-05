using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceDiamondManager : MonoBehaviour
{
    int width;
    int height;
    GridLayoutGroup grid;

    string choices;

    void Start()
    {
        choices = "";
        grid = gameObject.GetComponent<GridLayoutGroup>();
        width = (int)(grid.cellSize.x + grid.spacing.x);
        height = (int)(grid.cellSize.y + grid.spacing.y);
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ButtonMoveRight()
    {
        transform.localPosition -= new Vector3(width, 0, 0);
        choices += "1";
    }

    public void ButtonMoveDown()
    {
        transform.localPosition += new Vector3(0, height, 0);
        choices += "0";
    }

    public string GetChoices()
    {
        //print(choices);
        return choices;
    }
}
