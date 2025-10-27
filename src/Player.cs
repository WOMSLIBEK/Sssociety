using Godot;
using System;

public partial class Player : Node2D
{
    [Export]
    Sprite2D gorgon;
    [Export]
    public RichTextLabel eggCounter;
    [Export]
    public RichTextLabel playerNumber;

    //default is none of the main presets
    int[] deckComposition = new int[3] { 4, 4, 4 };

    Deck playerDeck;

    public bool isAnAI = false;


    int numberOfEggs = 0;

    public int NumberOfEggs {
        get
        {
            return numberOfEggs;
        }
        set
        {
            numberOfEggs = value;
            UpdateEggCounter();
        } 
    }

    public void InitialisePlayer(GameManager.RockPaperScissors presetChoice, bool AI)
    {
        if (presetChoice == GameManager.RockPaperScissors.Bite)
        {
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon3.png");
        }
        if (presetChoice == GameManager.RockPaperScissors.Hiss)
        {
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon2.png");

        }
        if (presetChoice == GameManager.RockPaperScissors.Constrict)
        {
            gorgon.Texture = ResourceLoader.Load<Texture2D>("res://assets/images/characters/Gorgon1.png");
        }
        playerDeck = new Deck(presetChoice);
        playerDeck.Name = "Deck";
        AddChild(playerDeck);

        isAnAI = AI;

    } 

    private void UpdateEggCounter()
    {
        eggCounter.Text = "Number of [color=#a4ea88]EGGS[/color]: " + numberOfEggs.ToString();
        
        
    }

    double cardPlayCounter = 0;
    bool AIAboutToPlayCard = false;
    public void StartTurn()
    {
        playerDeck.StartTurn();
        playerNumber.Text = "Player " + (GameManager.gameManager.PlayerIndex + 1).ToString();

        if (isAnAI)
        {
            
            AIAboutToPlayCard = true;
            
        }
    }


    
    public void StartRound()
    {
        playerDeck.ResetHand();
    }

    


    Vector2 lastScreenSize = Vector2.Zero;
    public override void _Process(double delta)
    {
        if (GetViewport().GetVisibleRect().Size != lastScreenSize)
        {
            lastScreenSize = GetViewport().GetVisibleRect().Size;
            gorgon.Position = GetViewport().GetVisibleRect().Size - .5f * gorgon.Texture.GetSize() * gorgon.Scale; ;
        }
        
        if (AIAboutToPlayCard)
        {
            cardPlayCounter += delta;
            if (cardPlayCounter > 1)
            {
                Card card = playerDeck.AIDecision();
                GetNode<CardInteractor>("CardInteractor").PlaceCard(card);
                AIAboutToPlayCard = false;
                cardPlayCounter = 0;
            }
            
        }
    }



    

}
