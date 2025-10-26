using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class Gameboard : Node2D
{
    [Signal]
    public delegate void NextTurnEventHandler();

    Stack<Card> cardsInPlay = new Stack<Card>();


    //these are gameplay variables that are changed during gameplay
    int turnDirection = 1;


    int playerIndex = 0;
    public int PlayerIndex{get{return playerIndex;}}

    Player[] players;

    public override void _Ready()
    {
        //GeneratePlayers and puts them on the gameboard
        PackedScene packedPlayer = GD.Load<PackedScene>("res://scene_objects/Player.tscn");
        players = new Player[GameManager.gameManager.players.Count()];

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
        //makes it so the first player is first to play
        playerIndex = 0;
    }

    private void GeneratePlayers()
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
        //this is where the special cards abilities are activated
        if(card.cardType == GameManager.RockPaperScissors.Special)
        {
            card.ActivateSpecialAbility();
        }

        if (cardsInPlay.Count != 0)
        {
            if (!GameManager.ValidInteraction(
                (GameManager.RockPaperScissors)cardsInPlay.Peek().cardType,
                (GameManager.RockPaperScissors)card.cardType))
            {
                return false;
            }
        }

        LoadNextTurn();
        

        cardsInPlay.Push(card);



        return true;
    }

    private void LoadNextTurn()
    {
        DetermineNextIndex();
        LoadNextPlayer();
        EmitSignal(SignalName.NextTurn);
    }

    private void DetermineNextIndex()
    {
        
        if(turnDirection < 0)
        {
            for (int i = 0; i < -turnDirection; i++)
            {
                playerIndex -= 1;
                if (playerIndex < 0)
                {
                    playerIndex = players.Count() - 1;
                }
            }
            return;
            
        }

        for(int i = 0; i < turnDirection; i++)
        {
            playerIndex += 1;
            if(playerIndex >= players.Count())
            {
                playerIndex = 0;
            }
        }
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
        
        players[playerIndex].Position = Vector2.Zero;

    }
    
    private void FuckThePlayerAway(Player player)
    {
        player.Position = new Vector2(0, 5000);
    }
    
    



    
}
