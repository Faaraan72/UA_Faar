using UnityEngine;
using UAFaar.Cards;
using UAFaar.Board;

public class TestSpawn : MonoBehaviour
{
    [SerializeField] private CardLibrary cardLibrary;
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private int pairCount = 4;

    //TEsting
    private void Start()
    {
        var cards = cardLibrary.GetRandomPairs(pairCount);
        boardManager.CreateBoard(cards);
    }
}
