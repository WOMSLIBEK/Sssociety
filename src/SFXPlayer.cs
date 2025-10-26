using Godot;
using System;
using System.IO;

public partial class SFXPlayer : AudioStreamPlayer2D
{
    AudioStream bite = GD.Load<AudioStream>("res://assets/music/dealing cards.wav");
    AudioStream hiss = GD.Load<AudioStream>("res://assets/music/hiss 1.wav");
    AudioStream constrict = GD.Load<AudioStream>("res://assets/music/constrict 1.wav");
    AudioStream defaultSound = GD.Load<AudioStream>("res://assets/music/rattlesnake rattle.wav");

    public void PlaySFXBasedOnCard(Card card)
    {
        if (card.cardType == GameManager.RockPaperScissors.Bite)
        {
            Stream = bite;
        }
        if (card.cardType == GameManager.RockPaperScissors.Hiss)
        {
            Stream = hiss;

        }
        if (card.cardType == GameManager.RockPaperScissors.Constrict)
        {
            Stream = constrict;

        }
        if (card.cardType == GameManager.RockPaperScissors.Special)
        {
            Stream = defaultSound;

        }

        Play();

        
        
    }



    
}
