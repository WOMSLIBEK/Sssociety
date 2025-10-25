using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class Gameboard : Node2D
{
    Stack<Card> cardsInPlay = new Stack<Card>();

    enum RockPaperScissors
    {
        Bite,
        Hiss,
        Constrict
    }

    public override void _Ready()
    {


    }

    //useful for some things but shouldnt give direct acces to cards in practice
    public int GetCardCount()
    {
        return cardsInPlay.Count;
    }

    //returns true if valid card and false if invalid
    public bool AddCardToStack(Card card)
    {
        if(cardsInPlay.Count != 0)
        {
            if(!ValidInteraction((RockPaperScissors)cardsInPlay.Peek().cardType, (RockPaperScissors)card.cardType) )
            {
                return false;
            }
        }


        cardsInPlay.Push(card);
        return true;
    }


    //value1 is the established card and value2 is the new card
    private bool ValidInteraction(RockPaperScissors value1, RockPaperScissors value2)
    {
        if (value1 == value2)
        {
            return false;
        }

        if (value1 == RockPaperScissors.Bite && value2 == RockPaperScissors.Constrict)
        {
            return false;
        }

        if (value1 == RockPaperScissors.Hiss && value2 == RockPaperScissors.Bite)
        {
            return false;
        }
        if (value1 == RockPaperScissors.Constrict && value2 == RockPaperScissors.Hiss)
        {
            return false;
        }
        

        return true;
    }


    
}
