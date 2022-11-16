
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scroll_Menu
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image boughtIcon;
        [SerializeField] private TMP_Text priceText;
        [SerializeField] private Button button;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color boughtButtonColor;

        private Item _item;

        private void OnEnable()
        {
            button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnButtonClick);
        }

        public void Initialize(Item item)
        {
            _item = item;
            itemIcon.sprite = item.icon;
            priceText.text = item.price.ToString();
            boughtIcon.enabled = false;
        }
        
        private void BuyItem()
        {
            button.interactable = false;
            _item.bonus.Awake();
        }

        private void OnButtonClick()
        {
            BuyItem();
            
            priceText.enabled = false;
            boughtIcon.enabled = true;
            buttonImage.color = boughtButtonColor;
        }
    }
}
