using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Gameboard : Node2D
{
    [Signal]
    public delegate void NextTurnEventHandler();

    public Stack<Card> cardsInPlay = new Stack<Card>();

    [Export]
    SFXPlayer theSFXPlayer;
    [Export]
    EggPile eggPile;

    int eggPrizePool = 4;





    Player[] players;

    public override void _Ready()
    {
        //GeneratePlayers and puts them on the gameboard
        PackedScene packedPlayer = GD.Load<PackedScene>("res://scene_objects/Player.tscn");
        players = new Player[GameManager.gameManager.players.Count()];

        
        int playerIndex = 0;
        foreach ((GameManager.RockPaperScissors playerType, bool isAI) playerInfo in GameManager.gameManager.players)
        {
            Player newPlayer = (Player)packedPlayer.Instantiate();
            newPlayer.InitialisePlayer(playerInfo.playerType, playerInfo.isAI);
            AddChild(newPlayer);
            players[playerIndex] = newPlayer;
            //makes it so only the first player plays first
            if (playerIndex != 0)
            {
                FuckThePlayerAway(newPlayer);
            }
            playerIndex += 1;

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
        //this is where the special cards abilities are activated
        if(card.cardType == GameManager.RockPaperScissors.Special)
        {
            card.ActivateSpecialAbility();
        }

        if (cardsInPlay.Count != 0)
        {
            if (!GameManager.gameManager.ValidInteraction(
                (GameManager.RockPaperScissors)cardsInPlay.Peek().cardType,
                (GameManager.RockPaperScissors)card.cardType))
            {
                return false;
            }
        }

        theSFXPlayer.PlaySFXBasedOnCard(card);

        cardsInPlay.Push(card);

        LoadNextTurn();

        
        return true;
    }

    private void LoadNextTurn()
    {
        GameManager.gameManager.UpdatePlayerIndex();
        if (!players[GameManager.gameManager.PlayerIndex].isAnAI)
        {
            //since this is purely visual
            LoadNextPlayer();
        }

        players[GameManager.gameManager.PlayerIndex].StartTurn();
        EmitSignal(SignalName.NextTurn);
    }

    //starting a new round means loading a new player, giving eggs to the correct player, reshuffling deck, reseting card stack
    public void StartRound()
    {
        //this sets it to the previous player the victor as the starting player of the next round
        GameManager.gameManager.turnDirection = -GameManager.gameManager.turnDirection;
        GameManager.gameManager.UpdatePlayerIndex();
        //int victorIndex = GameManager.gameManager.PlayerIndex;
        //now we must load the new player so thatthe visuals of egg giving make sense
        LoadNextPlayer();
        eggPile.PlayEggAnimation();
        GivePlayerEggs();

        cardsInPlay = new Stack<Card>();
        foreach (Player player in players)
        {
            player.StartRound();
        }

        //have to remove children from card stack position
        CardStackMaintainer oldStack = GetNode<CardStackMaintainer>("CardStackPosition");
        foreach(Node child in oldStack.GetChildren())
        {
            child.QueueFree();
        }

        GameManager.gameManager.ResetVariables();

        //this is load next turn without the update player index
        LoadNextPlayer();
        //players[GameManager.gameManager.PlayerIndex].StartTurn();
        //EmitSignal(SignalName.NextTurn);




    }

    private void GivePlayerEggs()
    {
        players[GameManager.gameManager.PlayerIndex].NumberOfEggs += eggPrizePool;
    }



    private void LoadNextPlayer()
    {

        for (int i = 0; i < players.Length; i++)
        {

            if (players[i] == null)
            {
                break;
            }

            FuckThePlayerAway(players[i]);

        }

        players[GameManager.gameManager.PlayerIndex].Position = Vector2.Zero;
        players[GameManager.gameManager.PlayerIndex].eggCounter.GetParent<Control>().Visible = true;


    }
    
    private void FuckThePlayerAway(Player player)
    {
        player.Position = new Vector2(0, 5000);
        player.eggCounter.GetParent<Control>().Visible = false;

    }
    
}
