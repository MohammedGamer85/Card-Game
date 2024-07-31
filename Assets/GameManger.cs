using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace Assets {
    public static class GameManger {
        public const int CARDS_NUM = 12;

        private static bool _gameCardIsFlipped;
        private static bool _playerCardIsFlipped;

        private static Card _gameCard;
        private static Card _playerCard;

        private static bool _validating;

        private static int _score;

        public static bool ClickedCard(Card card) {
            if (_validating)
                return false;
            if (card.IsGameCard) {
                if (_gameCardIsFlipped)
                    return false;

                _gameCard = card;
                _gameCardIsFlipped = true;
                card.IsFlipped = true;
            } else {
                if (_playerCardIsFlipped)
                    return false;

                _playerCard = card;
                _playerCardIsFlipped = true;
                card.IsFlipped = true;
            }
            if (_playerCardIsFlipped && _gameCardIsFlipped) {
                return true;
            } else {
                return false;
            }
        }

        public static IEnumerator Validate() {
            if (!_gameCardIsFlipped || !_playerCardIsFlipped)
                yield break;

            UIManger.Title.text = "Validating";
            _validating = true;
            yield return new WaitForSeconds(1);
            UIManger.Title.text = "Score: " + _score.ToString();
            _validating = false;

            if (_gameCard.FlippedCardColor != _playerCard.FlippedCardColor) {
                _gameCard.IsFlipped = false;
                _playerCard.IsFlipped = false;
                rest();
                yield break;
            }

            _gameCard.gameObject.SetActive(false);
            _playerCard.gameObject.SetActive(false);
            _score++;
            UIManger.Title.text = "Score: " + _score.ToString();
            if (_score == CARDS_NUM)
                UIManger.Title.text = "Game Finished";
            rest();
        }

        private static void rest() {
            _playerCardIsFlipped = false;
            _gameCardIsFlipped = false;
        }
    }
}
