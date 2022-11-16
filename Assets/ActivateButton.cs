using UnityEngine;

public class ActivateButton : MonoBehaviour
{
   [SerializeField] private GameObject scrollMenu;
   [SerializeField] private GameObject prevButton;
   
   public void ActivateMenu()
   {
      scrollMenu.SetActive(true);
      prevButton.SetActive(false);
   }
}
