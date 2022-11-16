using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Health : MonoBehaviour
{
    [SerializeField] private List<Image> hearts;
    [SerializeField] private int numOfHearts;
    [SerializeField] private GameObject heart;
    
   
    private int _maxHealth;
    private void Awake()
    {
        _maxHealth = hearts.Count;
    }
    
    private void Update()
    {
        ActiveHearts();
    }

    private void ActiveHearts()
    {
        for (var i = 0; i < _maxHealth; i++)
        {
            hearts[i].enabled = i < numOfHearts;
            hearts[i].gameObject.SetActive(i < numOfHearts);
        }
    }

    private void UpdateHearts()
    {
        var heartFill = numOfHearts;
        foreach (var i in hearts)
        {
            i.fillAmount = heartFill;
            heartFill -= 1;
        }
    }

    public void AddHealth(int val)
    {
        if (val <= 0) return;
      
        numOfHearts += val;
        ClearHearts();
        UpdateHearts();
    }

    private void ClearHearts()
    {
        foreach (var i in hearts)
        {
            Destroy(i.gameObject);
        }
        hearts.Clear();
        for (var i = 0; i < _maxHealth; i++)
        {
            var g = Instantiate(heart, transform);
            hearts.Add(g.GetComponent<Image>());
        }
    }
    
}