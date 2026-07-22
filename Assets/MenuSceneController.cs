using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    public GameObject inputPanelGo;
    public TMP_InputField nameInputField;
    public TextMeshProUGUI nameText;
    private void Start()
    {
        UpdateNameUI();
    }
    public void OnChangeNameButtonClick()
    {
        string name = PlayerPrefs.GetString("Name", "");
        nameInputField.text = name;
        inputPanelGo.SetActive(true);
        AudioManager.Instance.PlayClip(Config.btn_click);
    }
    public void OnSubmitButtonClick()
    {
        PlayerPrefs.SetString("Name",nameInputField.text);
        inputPanelGo.SetActive(false);
        UpdateNameUI();
        AudioManager.Instance.PlayClip(Config.btn_click);
    }
    void UpdateNameUI()
    {
        string name = PlayerPrefs.GetString("Name", "-");
        nameText.text = name;
    }
    public void OnAdventureButtonClick()
    {
        AudioManager.Instance.PlayClip(Config.btn_click);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
