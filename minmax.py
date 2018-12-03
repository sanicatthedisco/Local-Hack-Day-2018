# O |   | X
# ---------
# X |   | X
# ---------
#   | O | O

origBoard = ["O",1,"X","X",4,"X",6,"O","O"]
hPlayer = "O"
cPlayer = "X"

#Returns list of empty spaces
def emptyIndexes(board) :
    newBoard = []
    for i in board :
        if i != "O" and i != "X" :
            newBoard.append(i)
    return newBoard

#Checks win conditions
def checkWin(board, player) :
    if (board[0] == player and board[1] == player and board[2] == player) or (board[3] == player and board[4] == player and board[5] == player) or (board[6] == player and board[7] == player and board[8] == player) or (board[0] == player and board[3] == player and board[6] == player) or (board[1] == player and board[4] == player and board[7] == player) or (board[2] == player and board[5] == player and board[8] == player) or (board[0] == player and board[4] == player and board[8] == player) or (board[2] == player and board[4] == player and board[6] == player) :
        return True
    else:
        return False

#Minimax algorithm
def minMax(newBoard, player) :
    availSpots = emptyIndexes(newBoard)
    #Checking win conditions and returning score if met
    if checkWin(newBoard,hPlayer) :
        return {"score":-10}
    elif checkWin(newBoard, cPlayer) :
        return {"score":10}
    elif len(availSpots) == 0 :
        return {"score":0}
    moves = []
    #Looping through possible moves in each space
    for i in range(len(availSpots)) :
        move = {
            "index":None,
            "score":None
        }
        move["index"] = newBoard[availSpots[i]]
        newBoard[availSpots[i]] = player
        if (player == cPlayer) :
            #Recursion to check all possible moves
            result = minMax(newBoard, hPlayer)
            move["score"] = result["score"]
        else :
            result = minMax(newBoard, cPlayer)
            move["score"] = result["score"]
        #Replacing lost element and adding potential move to list
        newBoard[availSpots[i]] = move["index"]
        moves.append(move)
    #Checking best possible move from move list
    if player == cPlayer :
        bestScore = -10000
        for i in range(len(moves)) :
            if moves[i]["score"] > bestScore :
                bestScore = moves[i]["score"]
                bestMove = i
    else :
        bestScore = 10000
        for i in range(len(moves)) :
            if moves[i]["score"] < bestScore :
                bestScore = moves[i]["score"]
                bestMove = i
    return moves[bestMove]

def main() :
    print(minMax(origBoard, cPlayer)["score"])

main()
