using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using System;
using TMPro;

public class SpinManger : MonoBehaviour
{
    [SerializeField] private PickerWheel pickerWheel;
    [SerializeField] private Button spinButton;
    [SerializeField] private TextMeshProUGUI rewardText;

    void Start()
    {
        spinButton.GetComponentInChildren<TextMeshProUGUI>().text = "Spin";
        spinButton.GetComponent<Image>().color = Color.green;
        spinButton.onClick.AddListener(OnSpinButtonClicked);
        rewardText.gameObject.SetActive(false);
    }

    private void OnSpinButtonClicked()
    {
        spinButton.GetComponentInChildren<TextMeshProUGUI>().text = "Spinning...";
        spinButton.GetComponent<Image>().color = Color.red;
        pickerWheel.OnSpinStart(SpinStarted);
        pickerWheel.OnSpinEnd(SpinEnded);
        pickerWheel.Spin();
        spinButton.interactable = false;
    }

    private void SpinStarted()
    {
        Debug.Log("Spin started");
    }

    private void SpinEnded(WheelPiece piece)
    {
        Debug.Log("Spin ended");
        spinButton.interactable = true;
        spinButton.GetComponent<Image>().color = Color.green;
        spinButton.GetComponentInChildren<TextMeshProUGUI>().text = "Spin";
        Debug.Log("Wheel piece name : " + piece.Label);
        Debug.Log("Wheel piece Value : " + piece.Amount);
        string Name = "";
        foreach (Emp e in DatabaseManager.Instance.empList)
        {
            if (e.empId == GameManager.Instance.EmpId)
            {
                Name = e.empName;
                break;
            }
        }
        rewardText.text = $"{Name} Won {piece.Label}";
        rewardText.gameObject.SetActive(true);
        rewardText.GetComponent<Animator>().Play("Move_Animation");
        StartCoroutine(HideRewardText());
    }

    private IEnumerator HideRewardText()
    {
        yield return new WaitForSeconds(1.5f);
        rewardText.gameObject.SetActive(false);
    }
}
