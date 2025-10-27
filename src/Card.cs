using Godot;
using System;

public partial class Card : Sprite2D
{
    public GameManager.RockPaperScissors cardType;
    private AllCardTypes trueType;

    //tthis stops certain processes
    bool usable = true;

    //this is the resting position of the card and where it snaps back to if moved
    private Vector2 cardStaticPosition; 

    public Vector2 CardStaticPosition
    {
        get { return cardStaticPosition; }
        set
        {
            cardStaticPosition = value;
            Position = cardStaticPosition;
        }
    }

    public enum AllCardTypes
    {
        Empty = -1,
        Bite,
        Hiss,
        Constrict,
        Skip,
        Reverse,
        Inverse,
        BiteUnique,
        HissUnique,
        ConstrictUnique
    }
    
    //this is to determine what cards are best for being picked up by the player
    public float Selectability = 0;


    public Card (AllCardTypes cardType)
    {
        trueType = cardType;

        //card type is for external use this simplification makes code less messy
        if ((GameManager.RockPaperScissors)cardType == GameManager.RockPaperScissors.Bite ||
        (GameManager.RockPaperScissors)cardType == GameManager.RockPaperScissors.Hiss ||
        (GameManager.RockPaperScissors)cardType == GameManager.RockPaperScissors.Constrict)
        {
            this.cardType = (GameManager.RockPaperScissors)cardType;
        }
        else
        {
            this.cardType = GameManager.RockPaperScissors.Special;
        }



        if (cardType == AllCardTypes.Bite)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_bite.png");
            return;
        }

        if (cardType == AllCardTypes.Hiss)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_hiss.png");
            return;
        }

        if (cardType == AllCardTypes.Constrict)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_constrict.png");
            return;
        }

        if (cardType == AllCardTypes.Skip)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_wild.png");
            return;
        }
        if (cardType == AllCardTypes.Reverse)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_reverse.png");
            return;
        }
        if (cardType == AllCardTypes.Inverse)
        {
            Texture = ResourceLoader.Load<Texture2D>("res://assets/images/cards/card_inverse.png");
            return;
        }
        
        Texture = ResourceLoader.Load<Texture2D>("res://assets/images/UI/Egg.png");


        

    }

    public override void _Ready()
    {

    }

    public override void _Process(double delta)
    {
        if (usable)
        {
            HighlightCard();

        }


    }

    private void HighlightCard()
    {
        Texture.GetSize();
        Rect2 box = new Rect2(GlobalPosition - .5f * Texture.GetSize(), Texture.GetSize());
        if (box.HasPoint(GetGlobalMousePosition()))
        {
            Vector2 thingy = new Vector2(GetGlobalMousePosition().X, GlobalPosition.Y);
            float closeness = 1 - (thingy.DistanceTo(GlobalPosition) / Texture.GetSize().X);

            Scale = Vector2.One + .5f * new Vector2(
                closeness,
                closeness);

            Selectability = closeness;
            ZIndex = (int)Mathf.Floor(closeness * 6);
        }
        else
        {
            Scale = Vector2.One;
            Selectability = 0;
            ZIndex = 0;
        }
    }
    //this just looks for a type of script and runs it
    public void ActivateSpecialAbility()
    {
        if (trueType == AllCardTypes.Reverse)
        {
            //this should reverse direction
            GameManager.gameManager.turnDirection *= -1;
        }

        if (trueType == AllCardTypes.Skip)
        {
            //makes the game go for an extra turn before loading the new one
            GameManager.gameManager.UpdatePlayerIndex(GameManager.gameManager.turnDirection);
        }

        if (trueType == AllCardTypes.Inverse)
        {
            if (GameManager.gameManager.RuleOffset == 0)
            {
                GameManager.gameManager.RuleOffset = 1;
                return;
            }

            if(GameManager.gameManager.RuleOffset == 1)
            {
                GameManager.gameManager.RuleOffset = 0;
                return;
            }
        }
        

        
        
    }

    
    public void DeactivateCard()
    {
        usable = false;
        Selectability = 0;
        
    }

    public void ResetPosition()
    {
        Position = cardStaticPosition;
        
    }
    

}
