using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UILevelSelection : MonoBehaviour
{
    [SerializeField] protected Transform btnContent;
    [SerializeField] protected Sprite unlockedLevelSprite;
    [SerializeField] protected Sprite lockedLevelSprite;

    private void Start()
    {
        LoadButton();   
    }


    protected void LoadButton()
    {
        int unlockedLevel = LevelManager.Instance.Level; // the highest level unlocked
        for (int i = 0; i < btnContent.childCount;i++)
        {
            Transform loadLevelButton = btnContent.GetChild(i).GetChild(0);
            Image img =loadLevelButton.GetComponent<Image>();
            GameObject lockIcon = loadLevelButton.Find("lock").gameObject;

            if (i+1 <= unlockedLevel)
            {
                // unlocked
                img.sprite = unlockedLevelSprite;
                lockIcon.SetActive(false);
                AddListener(loadLevelButton.gameObject);
                
            } else
            {
                // locked
                img.sprite = lockedLevelSprite;
                lockIcon.SetActive(true);
            }
        }
    }

    protected void AddListener(GameObject buttonUI)
    {
        if (buttonUI.TryGetComponent<Button>(out Button btn))
        {
            int level = buttonUI.transform.parent.GetSiblingIndex() + 1;

            btn.onClick.AddListener(delegate { LevelManager.Instance.LoadLevel(level); });
        }
    }
   
   
   
}
