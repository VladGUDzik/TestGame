using System.Collections.Generic;
using UnityEngine;

namespace Scroll_Menu
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private ItemView template;
        [SerializeField] private Transform content;
        [SerializeField] private ShopScroll shopScroll;
        [SerializeField] private List<Item> items;
        
        private static List<ItemView> _spawneItems = new();
        private ItemView _spawnedItem;

        private void SetListItem()
        {
            foreach (var item in items)
            {
                _spawnedItem = Instantiate(template, content);
                _spawnedItem.Initialize(item);
                _spawneItems.Add(_spawnedItem);
            }
            
            shopScroll.Initialize(_spawneItems);
        }
        
        private void OnEnable()
        {
            SetListItem();
        }

        private void OnDisable()
        {
            shopScroll.Remove(_spawneItems);
            
            _spawneItems.Clear();
        }
    }
}