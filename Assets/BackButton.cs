using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject prevButton;

    public void BackButtonClick()
    {
        for (var i = 0; i < 3; i++) //todo algorithmic
        {
            var gameOb = GameObject.Find("Item(Clone)").name = $"{i}";
            Destroy(GameObject.Find(gameOb));
        }
        
        scrollView.SetActive(false);
        prevButton.SetActive(true);
    }
}