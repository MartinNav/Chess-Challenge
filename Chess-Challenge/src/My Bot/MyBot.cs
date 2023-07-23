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

            switch (moves[i].MovePieceType)
            {
                case PieceType.King:
                    score[i] = (moves[i].IsCapture ? 8 : 0);break;
                case PieceType.Queen:
                    score[i] = 9 + (moves[i].IsCapture? 10:-4);break;
                case PieceType.Pawn:
                    score[i] = 10;break;
                case PieceType.Knight:
                    score[i] = 6+dist/10+ (moves[i].IsCapture? 4:-1);break;
                
                case PieceType.Rook:
                    score[i] = dist/16;break;
                default:score[i] = 2; break;
            }
           //score[i]+= (moves[i].IsCapture ? (5+dist) : (moves[i].IsPromotion )? 8:dist/2);
        }
        return score.ToList().IndexOf(score.Max());
    }

}