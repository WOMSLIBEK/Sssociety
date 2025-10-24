using Godot;
using System;

public partial class Player : Node
{
    //egg
    [Export(PropertyHint.None, "Order: Violence,Kindness,Retribution")]
    int[] deckComposition = new int[3] {4, 4, 4};

    Deck playerDeck;

    public override void _Ready()
    {
        playerDeck = new Deck(deckComposition[0], deckComposition[1], deckComposition[2]);
        playerDeck.Name = "Deck";
        AddChild(playerDeck);
    }


    

}
