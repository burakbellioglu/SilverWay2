using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTrigger : MonoBehaviour
{

    public ProductManager manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.gameObject.SetActive(true);
            manager.gameObject.transform.parent.GetComponent<Animator>().SetTrigger("Get");
            manager.buyPanels.SetActive(true);
            manager.exitButton.interactable = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            manager.exitButton.interactable = true;
            StartCoroutine(manager.CloseProducts_Delay());
        }
    }


}
