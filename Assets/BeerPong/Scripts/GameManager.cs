using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singelton
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            return _instance;
        }

        set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }
        else
        {
            Debug.LogError("More than 1 GameManagers in the scene");
            Destroy(this.gameObject);
        }
    }
    #endregion

    public enum GameStatus { SETUP, PLAYING, BETWEENSHOTS };
    public GameStatus currentGameStatus;

    public Text debugText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        debugText.text = currentGameStatus.ToString();
    }
}
