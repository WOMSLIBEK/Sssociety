using Godot;
using System;

public partial class RulesDisplay : VBoxContainer
{
    [Export]
    RichTextLabel biteRules;
    [Export]
    RichTextLabel hissRules;
    [Export]
    RichTextLabel constrictRules;

    public override void _Ready()
    {
        GameManager.gameManager.Connect(GameManager.SignalName.RuleOffsetChanged, Callable.From(RulesOffsetChanged));
    }

    private void RulesOffsetChanged()
    {
        if (GameManager.gameManager.RuleOffset == 0)
        {
            biteRules.Text = "[right] [color=#f5bb57]BITE[/color] beats [color=#83fcfc]CONSTRICT[/color]";
            hissRules.Text = "[right] [color=#f7e943]HISS[/color] beats [color=#f5bb57]BITE[/color]";
            constrictRules.Text = "[right] [color=#83fcfc]CONSTRICT[/color] beats [color=#f7e943]HISS[/color]";
        }
        if (GameManager.gameManager.RuleOffset == 1)
        {
            biteRules.Text = "[right] [color=#f5bb57]BITE[/color] beats [color=#f7e943]HISS[/color]";
            hissRules.Text = "[right] [color=#f7e943]HISS[/color] beats [color=#83fcfc]CONSTRICT[/color]";
            constrictRules.Text = "[right] [color=#83fcfc]CONSTRICT[/color] beats [color=#f5bb57]BITE[/color]";
        }
        if(GameManager.gameManager.RuleOffset == 2)
        {
            biteRules.Text = "[right] [color=#f5bb57]BITE[/color] beats [color=#f5bb57]BITE[/color]";
            hissRules.Text = "[right] [color=#f7e943]HISS[/color] beats [color=#f7e943]HISS[/color]";
            constrictRules.Text = "[right] [color=#83fcfc]CONSTRICT[/color] beats [color=#83fcfc]CONSTRICT[/color]";
        }
        
    }

    
}
