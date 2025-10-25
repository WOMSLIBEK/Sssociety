using Godot;
using System;

public partial class Deck : Node2D
{
    const int deckSize = 12;
    Card[] cards = new Card[deckSize];

    enum RockPaperScissors
    {
        Bite,
        Hiss,
        Constrict
    }

    public Deck(int bite, int hiss, int constrict)
    {
        if (bite + hiss + constrict != deckSize)
        {
            throw new ArgumentException("The total number of cards must be equal to the deck size: " + deckSize);
        }

        // Initialize cards array and add to scene tree
        for (int i = 0; i < deckSize; i++)
        {
            if (i < bite)
            {
                cards[i] = new Card((int)RockPaperScissors.Bite);
            }
            else if (i < bite + hiss && i >= bite)
            {
                cards[i] = new Card((int)RockPaperScissors.Hiss);
            }
            else
            {
                cards[i] = new Card((int)RockPaperScissors.Constrict);
            }

            cards[i].Name = "Card_" + i;
            AddChild(cards[i]);
        }



    }

    //just bc the vieport is only known on entering tree
    public override void _EnterTree()
    {
        ResetCardPositions();
    }



    
    private void ResetCardPositions()
    {
        for (int i = 0; i < deckSize; i++)
        {
            Vector2 screenSize = GetViewport().GetVisibleRect().Size;
            Vector2 offset = new Vector2(screenSize.X/2, screenSize.Y);

            cards[i].CardStaticPosition = offset + new Vector2(
                ((i - deckSize / 2) * (screenSize.X / 1.5f)) / deckSize, -50); // Slight offset for visibility
            
        }
    }
}
