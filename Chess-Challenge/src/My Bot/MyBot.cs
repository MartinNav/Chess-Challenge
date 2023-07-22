using ChessChallenge.API;
using System;
using System.Collections.Generic;
using System.Linq;

public class MyBot : IChessBot
{
    
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        return moves[ScoreMoves(moves)];
    }
    int ScoreMoves(Move[] moves)
    {
        int[] score = new int[moves.Length];
        for (int i = 0; i < moves.Length; i++)
        {
            int dist = Math.Abs(moves[i].TargetSquare.Index - moves[i].StartSquare.Index);

            //score[i] += moves[i].MovePieceType == PieceType.King ? -5 : (moves[i].MovePieceType == PieceType.Queen) ? 3 : 0;
            switch (moves[i].MovePieceType)
            {
                case PieceType.King:
                    score[i] = 0;break;
                case PieceType.Queen:
                    score[i] = 3;break;
                case PieceType.Pawn:
                    score[i] = 3;break;
                case PieceType.Knight:
                    score[i] = 4;break;
                default:score[i] = 1; break;
            }
           score[i]*= (moves[i].IsCapture ? (5+dist) : (moves[i].IsPromotion /*|| moves[i].CapturePieceType!=null*/)? 8:dist/2);
        }
        return score.ToList().IndexOf(score.Max());
    }

}