using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardGameObject : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    private bool is_dragging = false;
    public void setMetadata(string card_name, string description, Sprite sprite) {
        UnityEngine.UI.Image card_image = transform.Find("Image").GetComponent<UnityEngine.UI.Image>();
        card_image.sprite = sprite;

        TextMeshProUGUI description_text = transform.Find("Description").transform.Find("Text").GetComponent<TextMeshProUGUI>();
        description_text.text = description;

        TextMeshProUGUI name_text = transform.Find("Name").GetComponent<TextMeshProUGUI>();
        name_text.text = card_name;
    }

    public void OnPointerDown(PointerEventData event_data) {
        this.is_dragging = true;    
    }

    public void OnPointerUp(PointerEventData eventData) {
        this.is_dragging = false;
    }

    void Update()
    {
        if (this.is_dragging) {
            Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(mouse_position.x, mouse_position.y);
        }
    }
}
