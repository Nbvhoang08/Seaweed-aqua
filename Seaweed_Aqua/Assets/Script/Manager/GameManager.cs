using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;
    private bool _gameOver;
    private bool _hasWon;
    [SerializeField] private List<GameObject> Rubber = new List<GameObject>();
    void Start()
    {
        hp = 3;
        _gameOver = false;
        _hasWon = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0 && !_gameOver && !AllRubberUnActive())
        {
            _gameOver = true;
            StartCoroutine(GameOver());
        }
        else if (hp > 0 && !_hasWon && AllRubberUnActive())
        {
            _hasWon = true;
            StartCoroutine(WinGame());
            LVManager.Instance.SaveGame();
        }
    }

    private bool AllRubberUnActive()
    {
        foreach (GameObject obj in Rubber)
        {
            if (obj.activeSelf)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.Instance.OpenUI<Fail>();
        Time.timeScale = 0;
    }
    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(3f);
        UIManager.Instance.OpenUI<Congrat>();
        Time.timeScale = 0;
    }
}
