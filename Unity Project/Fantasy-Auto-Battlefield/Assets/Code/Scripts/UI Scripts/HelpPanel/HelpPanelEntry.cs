using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "HelpPanelEntry", menuName = "Help Panel Entry", order = 53)]
public class HelpPanelEntry : ScriptableObject
{
    [SerializeField, TextArea(3, 10)]
    string helpText;
    [SerializeField]
    Sprite helpImage;

    public string HelpText { get => helpText; }
    public Sprite HelpImage { get => helpImage; }
}
