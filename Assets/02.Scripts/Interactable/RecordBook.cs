using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RecordBook : MonoBehaviour
{
    [SerializeField] BaseUI RecordUI;
    private void Start()
    {
        RecordUI.UpdateUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RecordUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            RecordUI.gameObject.SetActive(false);
        }
    }
}
