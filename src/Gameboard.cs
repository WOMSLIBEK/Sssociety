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

    int playerIndex = 0;
    Player[] players = new Player[10];

    public override void _Ready()
    {
        Godot.Collections.Array<Node> list = GetChildren();
        for (int i = 0; i < list.Count; i++)
        {
            Node child = list[i];
            if (child is Player)
            {
                players[playerIndex] = (Player)child;
                //no need for checks if theres 10 players youve done something wrong
                playerIndex += 1;
            }
            
        }

    }

    //useful for some things but shouldnt give direct acces to cards in practice
    public int GetCardCount()
    {
        return cardsInPlay.Count;
    }

    //returns true if valid card and false if invalid
    public bool AddCardToStack(Card card)
    {
        if (cardsInPlay.Count != 0)
        {
            if (!ValidInteraction((RockPaperScissors)cardsInPlay.Peek().cardType, (RockPaperScissors)card.cardType))
            {
                return false;
            }
        }


        LoadNextPlayer();

        cardsInPlay.Push(card);



        return true;
    }

    private void LoadNextPlayer()
    {
        playerIndex += 1;
        if (players[playerIndex] == null)
        {

            playerIndex = 0;
        }

        for (int i = 0; i < players.Length; i++)
        {

            if (players[i] == null)
            {
                break;
            }

            FuckThePlayerAway(players[i]);

        }
        
        players[playerIndex].Position = Vector2.Zero;

    }
    
    private void FuckThePlayerAway(Player player)
    {
        player.Position = new Vector2(0, 5000);
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
