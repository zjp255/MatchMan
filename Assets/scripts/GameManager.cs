using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public GameObject gmUI;
    public GameObject begUI;
    public GameObject EscUI;
    public GameObject querenUI;
   
    public inventory beg;
    bool isBegOpen = false;
    bool isEscTure = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

    }
    private void Update()
    {
        if (begUI != null && EscUI != null)
        {
            OpenTheBeg();
            EscManu();
        }
    }

    public static void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//重新加载当前场景

        

        Time.timeScale = 1f;
        instance.gmUI.SetActive(false);

        
    }

    public static void GameOver(bool IsAlive)
    {
        if (IsAlive == false)
        {
            instance.gmUI.SetActive(true);
            Time.timeScale = 0f;
        }
    
    }

    public static void StarNewGame()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        SaveManager.DeleteData();
        SceneManager.LoadScene("Map");
    }

    public static void StarGame()
    {
        instance.querenUI.SetActive(true);
    }
    public static void QuerenCancer()
    {
        instance.querenUI.SetActive(false);
    }
    public static void ExitGame()
    {
        Application.Quit();
    }

    public static void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OpenTheBeg()
    {
        if(Input.GetKeyDown(KeyCode.Tab) )
        {
            isBegOpen = !isBegOpen;
            begUI.SetActive(isBegOpen);
        }
    }

    public void CloseTheBeg()
    {
        isBegOpen = !isBegOpen;
        begUI.SetActive(false);
    }

    void EscManu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            isEscTure = !isEscTure;
            if(isEscTure == false)
                Time.timeScale = 1f;
            EscUI.SetActive(isEscTure);
        }
    }

    public void ContinueGame()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        //instance.beg.itemList.Clear();
        SceneManager.LoadScene("Map");
    }

    public void ContinueGameEsc()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        instance.EscUI.SetActive(false);
    }
}
