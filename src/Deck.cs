using Godot;
using System;

public partial class Deck : Node2D
{
    const int deckSize = 12;
    Card[] cards = new Card[deckSize];

    //card dimensions are very unliekly to change
    Vector2 CardDimensions = new Vector2(104, 160);



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
                cards[i] = new Card(GameManager.RockPaperScissors.Bite);
            }
            else if (i < bite + hiss && i >= bite)
            {
                cards[i] = new Card(GameManager.RockPaperScissors.Hiss);
            }
            else
            {
                cards[i] = new Card(GameManager.RockPaperScissors.Constrict);
            }

            cards[i].Name = "Card_" + i;
            AddChild(cards[i]);
        }



    }

    
    public override void _Ready()
    {
        base._Ready();
    }


    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        if(GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            lastScreenSize = GetViewport().GetVisibleRect().Size;
            ResetCardPositions();
        }
    }



    private void ResetCardPositions()
    {
        for (int i = 0; i < deckSize; i++)
        {
            if(cards[i] == null)
            {
                continue;
            }

            Vector2 screenSize = GetViewport().GetVisibleRect().Size;
            Vector2 offset = new Vector2(screenSize.X / 2, screenSize.Y);


            cards[i].CardStaticPosition = offset + new Vector2(
                ((i - deckSize / 2) * (screenSize.X / 1.5f)) / deckSize, -CardDimensions.Y / 2 - 20); // Slight offset for visibility

        }
    }
    
    public void RemoveCardFromDeck(Card card)
    {
        for (int i = 0; i < deckSize; i++)
        {
            if(cards[i] == card)
            {
                cards[i] = null;
            }

        }
    }
}
