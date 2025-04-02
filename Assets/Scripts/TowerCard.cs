using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TowerCard : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;
    private GameObject objectDragInst;
    private GameManager gameManager;

    public TMP_Text towerEnergy;
    public float towerCost = 50f;

    private void Start()
    {
        gameManager = GameManager.instance;
        towerEnergy.text = "Cost: " + towerCost.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        objectDragInst.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectDragInst = Instantiate(objectDrag, canvas.transform);
        objectDragInst.transform.position = Input.mousePosition;
        objectDragInst.GetComponent<TowerDragging>().card = this;

        gameManager.draggingObj = objectDragInst;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (gameManager != null && objectDragInst != null)
        {
            TowerCard card = objectDragInst.GetComponent<TowerDragging>().card;
            if (card != null)
            {
                gameManager.PlaceObject(card.towerCost);
            }
            else
            {
                Debug.LogError("TowerDragging.card is Null");
            }
        }
            gameManager.draggingObj = null;
            Destroy(objectDragInst);
    }
}
