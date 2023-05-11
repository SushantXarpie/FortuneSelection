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

    [HideInInspector] public string EmpId;

    [SerializeField] private TMP_InputField IdInput;
    [SerializeField] private Button startGridButton;
    [SerializeField] private Button startWheelButton;
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
        startGridButton.onClick.AddListener(OnGridStartButtonClicked);
        startWheelButton.onClick.AddListener(OnWheelStartButtonClicked);

    }

    private void OnWheelStartButtonClicked()
    {
        // ValidateInput();
        if (String.IsNullOrEmpty(IdInput.text))
        {
            Debug.Log("Please enter a name");
            StartCoroutine(ShowWarningText());
            return;
        }
        if (IdInput.text.Length < 5)
        {
            Debug.Log("Name is too short");
            StartCoroutine(ShowWarningText_2());
            return;
        }
        EmpId = IdInput.text;
        SceneManager.LoadScene("Wheel");
    }

    private void OnGridStartButtonClicked()
    {
        // ValidateInput();
        if (String.IsNullOrEmpty(IdInput.text))
        {
            Debug.Log("Please enter a name");
            StartCoroutine(ShowWarningText());
            return;
        }
        if (IdInput.text.Length < 5)
        {
            Debug.Log("Name is too short");
            StartCoroutine(ShowWarningText_2());
            return;
        }
        EmpId = IdInput.text;
        SceneManager.LoadScene("Game");
    }

    private IEnumerator ShowWarningText()
    {
        warningText.text = "Please enter your Id";
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
    }
    private IEnumerator ShowWarningText_2()
    {
        warningText.text = "Id is not correct";
        warningText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
    }

    private void ValidateInput()
    {
        if (String.IsNullOrEmpty(IdInput.text))
        {
            Debug.Log("Please enter a name");
            StartCoroutine(ShowWarningText());
            return;
        }
        if (IdInput.text.Length < 5)
        {
            Debug.Log("Name is too short");
            StartCoroutine(ShowWarningText_2());
            return;
        }
        EmpId = IdInput.text;
    }
}
