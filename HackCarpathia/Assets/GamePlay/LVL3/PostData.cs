using UnityEngine;

[CreateAssetMenu(fileName = "NowyPost", menuName = "Gra/Post")]
public class PostData : ScriptableObject
{
    public Sprite zdjecie;
    // Wa¿ne: Enum Frakcja musi byæ publiczny w igControler, ¿eby tu by³ widoczny
    public igControler.Frakcja przynaleznosc;

    [Header("Wartoœci Punktowe")]
    public int punktyZaLike = 10;
    public int punktyZaShare = 50;

    [Header("Mechanika Prawdy")]
    public bool toJestFake = false; // Czy post jest k³amstwem?
    [Range(0, 100)] public int trudnoscRozpoznania; // Jak silny filtr na³o¿yæ (0-100)
}