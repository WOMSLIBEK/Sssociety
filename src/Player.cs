using Godot;
using System;

public partial class Player : Node2D
{
    //default is none of the main presets
    int[] deckComposition = new int[3] { 4, 4, 4 };

    int[] bitePreset = new int[3] { 6, 4, 2 };
    int[] hissPreset = new int[3] { 2, 6, 4 };
    int[] constrictPreset = new int[3] {4,2,6 };
    
    public void InitialisePlayer(GameManager.RockPaperScissors presetChoice,bool AI)
    {
        if (presetChoice == GameManager.RockPaperScissors.Bite)
        {
            deckComposition = bitePreset;
        }
        if (presetChoice == GameManager.RockPaperScissors.Hiss)
        {
            deckComposition = hissPreset;

        }
        if(presetChoice == GameManager.RockPaperScissors.Constrict)
        {
            deckComposition = constrictPreset;
        }
    } 

    Deck playerDeck;

    public override void _Ready()
    {
        playerDeck = new Deck(deckComposition[0], deckComposition[1], deckComposition[2]);
        playerDeck.Name = "Deck";
        AddChild(playerDeck);
    }


    

}
