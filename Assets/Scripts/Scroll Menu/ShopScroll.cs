using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scroll_Menu
{
    public class ShopScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private HorizontalLayoutGroup horizontalLayoutGroup;
        [SerializeField] private float lerpSpeed = 3;
        [SerializeField] private float stopVelocityX = 200;
        [SerializeField] private float minItemSize = 200;
        [SerializeField] private float maxItemSize = 500;

        private bool _isInitialized;
        private bool _isDragging;
        private float _correctivePositionX;
        private RectTransform _content;
        private List<RectTransform> _items = new();
        

        private void OnEnable()
        {
            _content = scrollRect.content;

            var center = -scrollRect.viewport.transform.localPosition.x;
            _correctivePositionX = center - maxItemSize;
            horizontalLayoutGroup.padding = new RectOffset((int)center, (int)center, horizontalLayoutGroup.padding.top, horizontalLayoutGroup.padding.bottom);
        }

        private void OnDisable()
        {
            _content = null;

            _items.Clear();
        }

        private void Update()
        {
            if (_isInitialized == false)
            { 
                return; 
            }

            var nearestIndex = 0;
            var nearestDistance = float.MaxValue;
            var center = scrollRect.transform.position.x;

            for (var i = 0; i < _items.Count; i++)
            {
                var itemDistance = Mathf.Abs(center - _items[i].position.x);

                if (itemDistance < nearestDistance)
                {
                    nearestDistance = itemDistance;
                    nearestIndex = i;
                }

                var size = Mathf.Lerp(maxItemSize, minItemSize, itemDistance / center);
                _items[i].sizeDelta = CalculateSize(_items[i].sizeDelta, size);
            }

            if (_isDragging) return;
            if (Mathf.Abs(scrollRect.velocity.x) < stopVelocityX)
            {
                ScrollTo(nearestIndex);
            }
        }

        public void Initialize(List<ItemView> items)
        {
            if (_isInitialized)
                throw new InvalidOperationException("Already initialized");

            items.ForEach(item => _items.Add((RectTransform)item.transform));
            _isInitialized = true;
        }

        public void Remove(List<ItemView> items)
        {
            items.ForEach(item => _items.Remove((RectTransform)item.transform));

            _isInitialized = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _isDragging = true;
            scrollRect.inertia = true;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _isDragging = false;
        }

        private void ScrollTo(int index)
        {
            scrollRect.inertia = false;

            var item = _items[index];
            var contentTargetPositionX = -1 * Mathf.Clamp(item.anchoredPosition.x - item.sizeDelta.x - _correctivePositionX, 0, _content.sizeDelta.x);
            var anchoredPosition = _content.anchoredPosition;
            var nextContentPosition = new Vector2(contentTargetPositionX, anchoredPosition.y);

            _content.anchoredPosition = Vector2.Lerp(anchoredPosition, nextContentPosition, lerpSpeed * Time.deltaTime);
        }

        private static Vector2 CalculateSize(Vector2 from, float to)
        {
            return Vector2.Lerp(from, Vector2.one * to, 0.5f);
        }
    }
}
