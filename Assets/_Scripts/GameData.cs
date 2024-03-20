using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 2)]
public class GameData : ScriptableObject
{
    [SerializeField] private int _coins;
    [SerializeField] private int _stoneValue;
    [SerializeField] private int _maxStacked;
    public int Coins 
    {
        get => _coins;
        set => _coins = value;
    }

    public int MaxStacked
    {
        get => _maxStacked;
    }

    private int _levelNum;

    private int _stone = 0;
    public int Stone
    {
        get => _stone;
        set => _stone = value;
    }

    public int StoneValue
    {
        get => _stoneValue;
    }
}
