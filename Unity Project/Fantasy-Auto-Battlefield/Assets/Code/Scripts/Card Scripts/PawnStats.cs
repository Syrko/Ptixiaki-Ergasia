using UnityEngine;
using TMPro;

public class PawnStats : MonoBehaviour
{
    [SerializeField] GameObject attack;
    [SerializeField] GameObject defense;
    [SerializeField] GameObject hitpoints;

    TextMeshPro attackText;
    TextMeshPro defenseText;
    TextMeshPro hitpointsText;

    public TextMeshPro AttackText { get => attackText; set => attackText = value; }
    public TextMeshPro DefenseText { get => defenseText; set => defenseText = value; }
    public TextMeshPro HitpointsText { get => hitpointsText; set => hitpointsText = value; }

    private void Awake()
    {
        AttackText = attack.GetComponent<TextMeshPro>();
        DefenseText = defense.GetComponent<TextMeshPro>();
        HitpointsText = hitpoints.GetComponent<TextMeshPro>();
    }
}
