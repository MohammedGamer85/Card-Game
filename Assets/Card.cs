using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour{
    public Color FlippedCardColor = new();

    private Color _baseCardColor = Color.black;
    private Color _cardColor = Color.black;

    public bool IsGameCard;
    public bool IsFlipped;

    private Button _button;
    private RawImage _image;

    public void Start() {
        _image = gameObject.AddComponent<RawImage>();
        _button = gameObject.AddComponent<Button>();
        _button.targetGraphic = _image;
        _button.onClick.AddListener(onClickEvent);
    }

    // Update is called once per frame
    public void Update() {
        if (IsFlipped)
            _cardColor = FlippedCardColor;
        else
            _cardColor = _baseCardColor;
        _image.color = _cardColor;
    }

    private void onClickEvent() {
        if (GameManger.ClickedCard(this)) {
            StartCoroutine(GameManger.Validate());
        }
    }   
}
