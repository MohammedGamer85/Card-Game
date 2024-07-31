using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using static Assets.GameManger;

public class UIManger : MonoBehaviour {
    public GameObject GameCardsParent;
    public GameObject PlayerCardsParent;
    public GameObject CardPrefab;

    [SerializeField] private TMP_Text _title;
    private readonly System.Random _rng = new();
    private static Color[] _listOfColors = new Color[CARDS_NUM];

    public static TMP_Text Title;
    public void Awake() {
        Title = _title;
        _listOfColors = GenerateUniqueColors(CARDS_NUM);
    }

    public void Start() {
        for (int i = 0; i < CARDS_NUM; i++) {
            Card card = Instantiate(CardPrefab, GameCardsParent.transform).GetComponent<Card>();
            card.IsGameCard = true;
            card.FlippedCardColor = _listOfColors[i];
            int number = _rng.Next(0, CARDS_NUM);
            card.transform.SetSiblingIndex(number);

            card = Instantiate(CardPrefab, PlayerCardsParent.transform).GetComponent<Card>();
            card.IsGameCard = false;
            card.FlippedCardColor = _listOfColors[i];
            number = _rng.Next(0, CARDS_NUM);
            card.transform.SetSiblingIndex(number);
        }
    }

    public Color[] GenerateUniqueColors(int numberOfColors) {

        HashSet<Color> uniqueColors = new();

        while (uniqueColors.Count < numberOfColors) {
            Color newColor = new(
                (float)_rng.NextDouble(),
                (float)_rng.NextDouble(),
                (float)_rng.NextDouble(),
                255
            );

            uniqueColors.Add(newColor);
        }

        return uniqueColors.ToArray();
    }
}
