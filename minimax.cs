using System;
using System.Collections.Generic;

namespace minMaxApp {
  //Creation of Move object that stores index and score of move
  public class Move {
    private int xCoor;
    private int yCoor;
    private int zCoor;
    private int score;
    public Move(int x, int y, int z, int s) {
      xCoor = x;
      yCoor = y;
      zCoor = z;
      score = s;
    }
    public int getXCoor() {
      return this.xCoor;
    }
    public int getYCoor() {
      return this.yCoor;
    }
    public int getZCoor() {
      return this.zCoor;
    }
    public int getScore() {
      return this.score;
    }
    public void setXCoor(int x) {
      this.xCoor = x;
    }
    public void setYCoor(int y) {
      this.yCoor = y;
    }
    public void setZCoor(int z) {
      this.zCoor = z;
    }
    public void setScore(int s) {
      this.score = s;
    }
  }

  public class minMaxMain {
    static string hPlayer = "X";
    static string cPlayer = "O";

    /*
    //Returns list of empty spaces
    public static string[,,] emptyIndexes(string[,,] board) {
      for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
          for (int k = 0; k < 3; k++) {
            if (!(board[i,j,k].Equals("O")) && !(board[i,j,k].Equals("X"))) {
              newBoard[i,j,k] = "0";
            }
          }
        }
      }
      return newBoard;
    }
    */

    public static bool checkFull(string[,,] board) {
      for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
          for (int k = 0; k < 3; k++) {
            if (!(board[i,j,k].Equals("O")) || !(board[i,j,k].Equals("X"))) {
              return false;
            }
          }
        }
      }
      return true;
    }

    public static bool checkSpaceFull(string[,,] board, int x, int y, int z) {
      if(board[x,y,z] == "O" || board[x,y,z] == "X") {
        return true;
      }
      else {
        return false;
      }
    }

    //Chekcs win conditions
    public static bool checkWin(string[,,] board, string player) {
      
    }


    //Minimax algorithm
    public static Move minMax(string[,,] newBoard, string player) {
      //List<string> availSpots = emptyIndexes(newBoard);
      //Checks win conditions and returning score if met
      if (checkWin(newBoard,hPlayer)) {
        Move m = new Move(-1,-10);
        return m;
      }
      else if (checkWin(newBoard,cPlayer)) {
        Move m = new Move(-1,10);
        return m;
      }
      else if (checkFull(newBoard)) {
        Move m = new Move(-1,0);
        return m;
      }
      List<Move> moves = new List<Move>();
      //Looping through possible moves in each space
      int index = 0;
      for (int i = 0; i < 3; i++) {
        for (int j = 0; j < 3; j++) {
          for (int k = 0; k < 3; k++) {
            if (!(checkSpaceFull(newBoard,i,j,k))) {
              Move move = new Move(j,k,l,-1);
              newBoard[j,k,l] = player;
              if (player == cPlayer) {
                //Recursion to check all possible moves
                Move result = new Move(-1,-1,-1,-1);
                result = minMax(newBoard,hPlayer);
                move.setScore(result.getScore());
              }
              else {
                Move result = new Move(-1,-1,-1,-1);
                result = minMax(newBoard,cPlayer);
                move.setScore(result.getScore());
              }
              //Replacing lost element and adding potential move to list
              newBoard[j,k,l] = 0;
              moves.Add(move);
            }
          }
        }
      }
      int bestMove = 0;
      //Checking best possible move from move list
      if (player == cPlayer) {
        int bestScore = -10000;
        for (int i = 0; i < moves.Count; i++) {
          if (moves[i].getScore() > bestScore) {
            bestScore = moves[i].getScore();
            bestMove = i;
          }
        }
      }
      else {
        int bestScore = 10000;
        for (int i = 0; i < moves.Count; i++) {
          if (moves[i].getScore() < bestScore) {
            bestScore = moves[i].getScore();
            bestMove = i;
          }
        }
      }
      return moves[bestMove];
    }
    public static void Main(string[] args) {
      int[,,] origBoard =
      [[["","","O"],
      ["O","X","O"],
      ["X","","X"]],

      [["X","O",""],
      ["","O",""],
      ["X","",""]],

      [["","X","O"],
      ["O","X",""],
      ["","","O"]]]
      Move r = new Move(-1,-1);
      r = minMax(origBoard,cPlayer);
      Console.WriteLine(r.getIndex());
      Console.WriteLine(r.getScore());
    }
  }
}
