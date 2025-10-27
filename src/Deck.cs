using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public partial class Deck : Node2D
{
    const int deckSize = 3;
    Card[] cards = new Card[deckSize];

    //card dimensions are very unliekly to change
    Vector2 CardDimensions = new Vector2(104, 160);

    Card.AllCardTypes[] BitePreset = new Card.AllCardTypes[15] {
        Card.AllCardTypes.Bite, Card.AllCardTypes.Bite,Card.AllCardTypes.Bite,
        Card.AllCardTypes.Bite,Card.AllCardTypes.Bite,Card.AllCardTypes.Bite,
        Card.AllCardTypes.Hiss, Card.AllCardTypes.Hiss,Card.AllCardTypes.Hiss,
        Card.AllCardTypes.Hiss,Card.AllCardTypes.Constrict,Card.AllCardTypes.Constrict,
        Card.AllCardTypes.Skip,Card.AllCardTypes.Reverse,Card.AllCardTypes.Inverse};
    Card.AllCardTypes[] HissPreset = new Card.AllCardTypes[15] {
        Card.AllCardTypes.Hiss, Card.AllCardTypes.Hiss,Card.AllCardTypes.Hiss,
        Card.AllCardTypes.Hiss,Card.AllCardTypes.Hiss,Card.AllCardTypes.Hiss,
        Card.AllCardTypes.Constrict, Card.AllCardTypes.Constrict,Card.AllCardTypes.Constrict,
        Card.AllCardTypes.Constrict,Card.AllCardTypes.Bite,Card.AllCardTypes.Bite,
        Card.AllCardTypes.Skip,Card.AllCardTypes.Reverse,Card.AllCardTypes.Inverse};

    Card.AllCardTypes[] ConstrictPreset = new Card.AllCardTypes[15] {
        Card.AllCardTypes.Constrict, Card.AllCardTypes.Constrict,Card.AllCardTypes.Constrict,
        Card.AllCardTypes.Constrict,Card.AllCardTypes.Constrict,Card.AllCardTypes.Constrict,
        Card.AllCardTypes.Bite, Card.AllCardTypes.Bite,Card.AllCardTypes.Bite,
        Card.AllCardTypes.Bite,Card.AllCardTypes.Hiss,Card.AllCardTypes.Hiss,
        Card.AllCardTypes.Skip,Card.AllCardTypes.Reverse,Card.AllCardTypes.Inverse};

    //this is so the deck can be reset after a round
    Card.AllCardTypes[] DeckSet;
    Card.AllCardTypes[] DeckPool;


    public Deck(GameManager.RockPaperScissors preset)
    {
        if (GameManager.RockPaperScissors.Bite == preset)
        {
            DeckSet = BitePreset;
        }
        if (GameManager.RockPaperScissors.Hiss == preset)
        {
            DeckSet = HissPreset;
        }
        if (GameManager.RockPaperScissors.Constrict == preset)
        {
            DeckSet = ConstrictPreset;
        }
    }


    public override void _Ready()
    {
        ResetHand();
    }

    //this is called at the begining of a new round/game
    public void ResetHand()
    {
        DeckPool = DeckSet;
        ShuffleDeck();

        EmptyHand();
        RefillHand();

        ResetCardPositions();
    }

    private void EmptyHand(){
        for (int i = 0; i < cards.Count(); i++)
        {
            cards[i] = null;
        }

        //so visuals match
        foreach (Node child in GetChildren())
        {
            child.QueueFree();
        }
    }

    private void RefillHand()
    {
        for (int i = 0; i < cards.Count(); i++)
        {
            if (cards[i] == null)
            {
                cards[i] = PullFromDeckPool();
            }
        }
    }

    //this is called by the player when your turn starts
    public void StartTurn()
    {
        RefillHand();
        ResetCardPositions();
        if (!DoesValidMoveExist())
        {
            //end the round, give the victor the eggs and start a new round
            GetParent().GetParent<Gameboard>().StartRound();
        }
    }
    
    //the decision the ai makes just the first valid choice
    public Card AIDecision(){
        if (GetParent().GetParent<Gameboard>().cardsInPlay.Count <= 0)
        {
            return cards[0];
        }

        Card topOfDeck = GetParent().GetParent<Gameboard>().cardsInPlay.Peek();

        foreach (Card card in cards)
        {
            if (card == null)
            {
                continue;
            }

            if (GameManager.gameManager.ValidInteraction(topOfDeck.cardType, card.cardType))
            {

                return card;
            }

        }

        return null;
    }

    private bool DoesValidMoveExist(){
        if (GetParent().GetParent<Gameboard>().cardsInPlay.Count <= 0)
        {
            return true;
        }

        Card topOfDeck = GetParent().GetParent<Gameboard>().cardsInPlay.Peek();
        bool valid = false;
        foreach (Card card in cards)
        {
            if(card == null){
                continue;
            }

            if (GameManager.gameManager.ValidInteraction(topOfDeck.cardType, card.cardType))
            {
                
                valid = true;
            }

        }
        return valid;
    }
    
    private void ShuffleDeck()
    {
        Random rnd = new Random();
        for(int i = 0; i < DeckPool.Count();i++)
        {
            int randomIndex = rnd.Next(0, DeckPool.Count()-1); // Generate random number between 0 and i
            // Swap elements
            Card.AllCardTypes temp = DeckPool[i];
            DeckPool[i] = DeckPool[randomIndex];
            DeckPool[randomIndex] = temp;
            
        }
        
    }
    
    private Card PullFromDeckPool()
    {
        for(int i = 0; i < DeckPool.Count(); i++)
        {
            if(DeckPool[i] != Card.AllCardTypes.Empty)
            {
                Card newCard = new Card(DeckPool[i]);
                AddChild(newCard);
                DeckPool[i] = Card.AllCardTypes.Empty;
                return newCard;
                
            }
        }
        


        return null;
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
                ((i - deckSize / 2) * (screenSize.X / 4.5f)) / deckSize, -CardDimensions.Y / 2 - 20); // Slight offset for visibility

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
