using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GV;

public class CardManager : MonoBehaviour {











    public void UseCard() { // 사용한 카드를 배열에서 지운 뒤 카드 하나 생성해 배열에 추가
        _gameChances -= 1;
        _totalCost += 140;
        _cards[_selectedCardIndex] = _cards[2];
        _cards.RemoveAt(2);
        CreateCard();
        UpgradCard();
    }

    public void SwapCard(int index) { // 카드 스왑
        if(_swapChances > 0) {
            _swapChances -= 1;
            //DeselectCard();
            _cards[index] = _cards[2];
            _cards.RemoveAt(2);
            CreateCard();
            UpgradCard();
            SendCardData();
        } else {
            Debug.Log("No chances");
        }
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
    }





    public int GetSelectedCardIndex() {
        return _selectedCardIndex;
    }


    public int GetSwapChances() {
        return _swapChances;
    }
    public int GetGameChances() {
        return _gameChances;
    }
    public int GetTotalCost() {
        return _totalCost;
    }
    public int GetGameGrade() {
        return _gameGrade;
    }
    public void CalcGameGrade() {
        if(_gameChances <= 0) {
            if(_gameGrade > 0) {
                _gameGrade--;
                _gameChances += (_gameGrade == 1) ? 2 : 1; 
            }
        }
    }
    public void DictionaryInit() {
        _gameChanceDictionary = new List<List<int>> {
            new List<int> { // 투구
                7, 7, 7, 8, 8, 11, 11
            },
            new List<int> { // 어께
                7, 7, 7, 10, 10, 13, 13
            },
            new List<int> { // 상의
                5, 5, 5, 5, 5, 8, 8
            },
            new List<int> { //하의
                7, 7, 7, 10, 10, 13, 13
            },
            new List<int> { // 장갑
                7, 7, 7, 8, 8, 11, 11
            },
            new List<int> { // 무기
                5, 5, 5, 5, 5, 8, 8
            }
        };
    }
    public void SetSwapChances(int value) {
        _swapChances += value;
    }

    public void SetGameChances(int value) {
        _gameChances += value;
    }
    public void MystiqueActivate() {
        int targetIndex = _selectedCardIndex == 0 ? 1 : 0;
        List<ECardType> tmp = new List<ECardType> { ECardType.eruption, ECardType.resonance };
        int randIndex = _rand.Next(tmp.Count);
        ECardType randType = tmp[randIndex];
        ECardRank randRank = randType == ECardType.eruption ? ECardRank.first : ECardRank.third;
        _cards[targetIndex] = new Card(randRank, randType);
    }
    public void EnhancementActivate() {
        int targetIndex = _selectedCardIndex == 0 ? 1 : 0;
        _cards[targetIndex]._rank += 1;
    }
    public void DuplicationActivate() {
        int targetIndex = _selectedCardIndex == 0 ? 1 : 0;
        _cards[targetIndex] = _cards[_selectedCardIndex];
    }
}

