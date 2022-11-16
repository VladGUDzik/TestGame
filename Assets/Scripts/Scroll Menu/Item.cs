using UnityEngine;

namespace Scroll_Menu
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 66)]
    public class Item : ScriptableObject
    {
        [field: SerializeField] public Sprite icon { get; private set; }
        [field: SerializeField] public int price { get; private set; }
        [field: SerializeField] public Bonus.Bonus bonus { get; private set; }
       
    }
}