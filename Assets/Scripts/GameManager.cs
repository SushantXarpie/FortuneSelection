using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

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
        if (!ValidateInput()) return;
        SceneManager.LoadScene("Wheel");
    }

    private void OnGridStartButtonClicked()
    {
        if (!ValidateInput()) return;
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

    private bool ValidateInput()
    {
        if (String.IsNullOrEmpty(IdInput.text))
        {
            Debug.Log("Please enter a name");
            StartCoroutine(ShowWarningText());
            return false;
        }
        if (!IsValidID(IdInput.text))
        {
            Debug.Log("Name is too short");
            StartCoroutine(ShowWarningText_2());
            return false;
        }
        EmpId = IdInput.text;
        return true;
    }

    private bool IsValidID(string Id)
    {
        foreach (Emp e in DatabaseManager.Instance.empList)
        {
            if (String.Equals(e.empId, Id, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
