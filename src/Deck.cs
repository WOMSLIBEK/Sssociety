using Godot;
using System;

public partial class Deck : Node
{
    const int deckSize = 12;
    Card[] cards = new Card[deckSize];

    enum RockPaperScissors
    {
        Violence,
        Kindness,
        Retribution
    }

    public Deck(int violenceAmount, int kindnessAmount, int retributionAmount)
    {
        if (violenceAmount + kindnessAmount + retributionAmount != deckSize)
        {
            throw new ArgumentException("The total number of cards must be equal to the deck size: " + deckSize);
        }

        // Initialize cards array and add to scene tree
        for (int i = 0; i < deckSize; i++)
        {
            if (i < violenceAmount)
            {
                cards[i] = new Card((int)RockPaperScissors.Violence);
            }
            else if (i < violenceAmount + kindnessAmount && i >= violenceAmount)
            {
                cards[i] = new Card((int)RockPaperScissors.Kindness);
            }
            else
            {
                cards[i] = new Card((int)RockPaperScissors.Retribution);
            }

            cards[i].Name = "Card_" + i;
            AddChild(cards[i]);
        }

        ResetCardPositions();

    }


    
    private void ResetCardPositions()
    {
        for (int i = 0; i < deckSize; i++)
        {
            Vector2 screenSize = new Vector2(1152,648);
            Vector2 offset = new Vector2(screenSize.X/2, screenSize.Y);

            cards[i].CardPosition = offset + new Vector2(
                ((i - deckSize / 2) * (screenSize.X / 1.5f)) / deckSize, -200); // Slight offset for visibility
            
        }
    }
}
