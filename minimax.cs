using System;
using System.Collections.Generic;

namespace minMaxApp {
  public class Move {
    private int index;
    private int score;
    public Move(int i, int s) {
      index = i;
      score = s;
    }
    public int getIndex() {
      return this.index;
    }
    public int getScore() {
      return this.score;
    }
    public void setIndex(int i) {
      this.index = i;
    }
    public void setScore(int s) {
      this.score = s;
    }
  }

  public class minMaxMain {
    static string hPlayer = "X";
    static string cPlayer = "O";

    public static List<string> emptyIndexes(List<string> board) {
       List<string> newBoard = new List<string>();
      for (int i = 0; i < board.Count; i++) {
        if (!(i.Equals("O")) && !(i.Equals("X"))) {
          newBoard.Add("" + i);
        }
      }
      return newBoard;
    }

    public static bool checkWin(List<string> board, string player) {
      if( (board[0] == player && board[1] == player && board[2] == player) ||
      (board[3] == player && board[4] == player && board[5] == player) ||
      (board[6] == player && board[7] == player && board[8] == player) ||
      (board[0] == player && board[3] == player && board[6] == player) ||
      (board[1] == player && board[4] == player && board[7] == player) ||
      (board[2] == player && board[5] == player && board[8] == player) ||
      (board[0] == player && board[4] == player && board[8] == player) ||
      (board[2] == player && board[4] == player && board[6] == player) ) {
        return true;
      }
      else {
        return false;
      }
    }
    public static Move minMax(List<string> newBoard, string player) {
      List<string> availSpots = emptyIndexes(newBoard);
      if (checkWin(newBoard,hPlayer)) {
        Move m = new Move(-1,-10);
        return m;
      }
      else if (checkWin(newBoard,cPlayer)) {
        Move m = new Move(-1,10);
        return m;
      }
      else if (availSpots.Count == 0) {
        Move m = new Move(-1,0);
        return m;
      }
      List<Move> moves = new List<Move>();
      for (int i = 0; i < availSpots.Count; i++) {
        Move move = new Move(-1,-1);
        int n = Int32.Parse(availSpots[i]);
        move.setIndex(n);
        newBoard[n] = player;
        if (player == cPlayer) {
          Move result = new Move(-1,-1);
          result = minMax(newBoard,hPlayer);
          move.setScore(result.getScore());
        }
        else {
          Move result = new Move(-1,-1);
          result = minMax(newBoard,cPlayer);
          move.setScore(result.getScore());
        }
        newBoard[n] = "" + move.getIndex();
        moves.Add(move);
      }
      int bestMove = 0;
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
      List<string> origBoard = new List<string>();
      origBoard.Add("O");
      origBoard.Add("1");
      origBoard.Add("X");
      origBoard.Add("X");
      origBoard.Add("4");
      origBoard.Add("X");
      origBoard.Add("6");
      origBoard.Add("O");
      origBoard.Add("O");
      Console.WriteLine(minMax(origBoard,cPlayer));
    }
  }
}
