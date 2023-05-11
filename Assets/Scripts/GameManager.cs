using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [HideInInspector] public string Name;

    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button startButton;
    [SerializeField] private TextMeshProUGUI warningText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
    }

    private void OnStartButtonClicked()
    {
        if (String.IsNullOrEmpty(nameInput.text))
        {
            Debug.Log("Please enter a name");
            StartCoroutine(ShowWarningText());
            return;
        }
        if (nameInput.text.Length < 5)
        {
            Debug.Log("Name is too long");
            StartCoroutine(ShowWarningText_2());
            return;
        }
        Name = nameInput.text;
        DatabaseManager.Instance.InsertToDB(Name);
        SceneManager.LoadScene("Game");
    }

    private IEnumerator ShowWarningText()
    {
        warningText.text = "Please enter a name";
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
    }
    private IEnumerator ShowWarningText_2()
    {
        warningText.text = "Name is too short";
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
    }
}
