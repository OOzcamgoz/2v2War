using UnityEngine;

public class TurnPage : MonoBehaviour
{
    [Header("Drag in next page")]
    public Transform nextPage;

    public void TurnToPage()
    {
        nextPage.SetAsFirstSibling();
    }
}
