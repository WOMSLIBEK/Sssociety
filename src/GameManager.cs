using Godot;
using System;

public partial class GameManager : Node
{
    public enum RockPaperScissors
    {
        Bite,
        Hiss,
        Constrict
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
