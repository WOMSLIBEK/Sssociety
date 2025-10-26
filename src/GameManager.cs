using Godot;
using System;
using System.Linq;

public partial class GameManager : Node
{
    //these are gameplay variables that are changed during gameplay
    public int turnDirection = 1;
    int playerIndex = 0;
    public int PlayerIndex { get { return playerIndex; } }
    

    //this calls at the start of a new round
    public void ResetVariables()
    {
        turnDirection = 1;
    }


    public void UpdatePlayerIndex(int customAmount = 0)
    {
        
        if(GameManager.gameManager.turnDirection < 0)
        {
            for (int i = 0; i < -GameManager.gameManager.turnDirection + customAmount; i++)
            {
                playerIndex -= 1;
                if (playerIndex < 0)
                {
                    playerIndex = players.Count() - 1;
                }
            }
            return;
            
        }

        for(int i = 0; i < GameManager.gameManager.turnDirection+ customAmount; i++)
        {
            playerIndex += 1;
            if(playerIndex >= players.Count())
            {
                playerIndex = 0;
            }
        }
    }

    public enum RockPaperScissors
    {
        Bite,
        Hiss,
        Constrict,
        Special
    }




    public (RockPaperScissors playerType, bool isAI)[] players;

    public static GameManager gameManager;

    public override void _Ready()
    {
        gameManager = new GameManager();
    }



    //value1 is the established card and value2 is the new card
    static public bool ValidInteraction(RockPaperScissors value1, RockPaperScissors value2)
    {
        //special cards are always valid
        if (value2 == RockPaperScissors.Special || value1 == RockPaperScissors.Special)
        {
            return true;
        }
        

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
