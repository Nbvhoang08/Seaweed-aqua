using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GamePlay : CanvasUI
{
    [Header("Heart Setting")]
    [SerializeField] private Sprite fullSprite;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private List<Image> HeartImages = new List<Image>();
    [SerializeField] private GameObject HeartPrefab;
    [SerializeField] private Transform HeartContainer;

    

    [Header("Game Manager")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Text LevelName;

    public void HomeBtn()
    {
        UIManager.Instance.CloseAll();
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
        SoundManager.Instance.PlayVFXSound(2);
        UIManager.Instance.OpenUI<ChooseLevel>();

    }

    private void Update()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        else
        {
            UpdateHPCount();
        }

        UpdateLevelText();
   


    }
    
    private void UpdateLevelText()
    {
        if (LevelName != null)
        {
            LevelName.text = "Level: " + SceneManager.GetActiveScene().name;
        }
    }

    public void UpdateHPCount()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        int remainingSteps = _gameManager.hp;

        for (int i = 0; i < HeartImages.Count; i++)
        {
            if (i < remainingSteps)
            {
                HeartImages[i].sprite = fullSprite;
            }
            else
            {
                HeartImages[i].sprite = emptySprite;
            }
        }
    }
    public void spawnHeartManual()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        // Xóa các UI Count hiện có
        foreach (Transform child in HeartContainer)
        {
            Destroy(child.gameObject);
        }
        HeartImages.Clear();
        for (int i = 0; i < 3; i++)
        {
            GameObject heart = Instantiate(HeartPrefab, HeartContainer);
            RectTransform rectTransform = heart.GetComponent<RectTransform>();
            // Thêm vào danh sách
            Image stepCountImage = heart.GetComponent<Image>();
            stepCountImage.sprite = fullSprite;
            HeartImages.Add(stepCountImage);
        }


    }
}
