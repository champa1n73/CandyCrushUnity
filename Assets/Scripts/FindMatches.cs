using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindAllMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private List<GameObject> IsAdjacentBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot1.column, dot1.row));
        }

        if (dot2.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot2.column, dot2.row));
        }

        if (dot3.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot3.column, dot3.row));
        }
        return currentDots;
    }
         
    private List<GameObject> IsRowCandy(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowCandy)
        {
            currentMatches.Union(getRowPieces(dot1.row));
        }

        if (dot2.isRowCandy)
        {
            currentMatches.Union(getRowPieces(dot2.row));
        }

        if (dot3.isRowCandy)
        {
            currentMatches.Union(getRowPieces(dot3.row));
        }
        return currentDots;
    }

    private List<GameObject> IsColumnCandy(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnCandy)
        {
            currentMatches.Union(getColumnPieces(dot1.column));
        }

        if (dot2.isColumnCandy)
        {
            currentMatches.Union(getColumnPieces(dot2.column));
        }

        if (dot3.isColumnCandy)
        {
            currentMatches.Union(getColumnPieces(dot3.column));
        }
        return currentDots;
    }

    private void AddToListAndMatch(GameObject dot)
    {
        if (!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3)
    {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }
        

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                
                if (currentDot != null)
                {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];                       
                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null)
                        {
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            if (leftDot != null && rightDot != null)
                            {
                                if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                                {
                                    currentMatches.Union(IsRowCandy(leftDotDot, currentDotDot, rightDotDot));
                                    currentMatches.Union(IsColumnCandy(leftDotDot, currentDotDot, rightDotDot));
                                    currentMatches.Union(IsAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));
                                    GetNearbyPieces(leftDot, currentDot, rightDot);
                                }
                            }
                        }                    
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];                     
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            if (upDot != null && downDot != null)
                            {
                                if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                                {
                                    currentMatches.Union(IsColumnCandy(upDotDot, currentDotDot, downDotDot));
                                    currentMatches.Union(IsRowCandy(upDotDot, currentDotDot, downDotDot));
                                    currentMatches.Union(IsAdjacentBomb(upDotDot, currentDotDot, downDotDot));
                                    GetNearbyPieces(upDot, currentDot, downDot);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void MatchPiecesOfColor(string color)
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                // Check if that piece exists 
                if (board.allDots[i, j] != null)
                {
                    // Check the tag on that dot
                    if (board.allDots[i, j].tag == color)
                    {
                        // Set that dot to be matched
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetAdjacentPieces(int column, int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = column - 1; i <= column + 1; i++)
        {
            for (int j = row - 1; j <= column;j++)
            {
                    // Check if the piece is inside the board
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    dots.Add(board.allDots[i, j]);
                    board.allDots[i, j].GetComponent<Dot>().isMatched = true; 
                }
            }
        }
        return dots;
    }

    List<GameObject> getColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.height; i++)
        {
            if ((board.allDots[column, i] != null))
            {
                dots.Add(board.allDots[column, i]);
                board.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }

    List<GameObject> getRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if ((board.allDots[i, row] != null))
            {
                dots.Add(board.allDots[i, row]);
                board.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }
        }
        return dots;
    }

    public void CheckBombs()
    {
        // Did the player move something?
        if (board.currentDot != null)
        {
            // Is the piece they moved matched?
            if (board.currentDot.isMatched)
            {
                // make it unmatched 
                board.currentDot.isMatched = false;
                // Decide what kind of bomb to make
                if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                        || board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135)
                {
                    board.currentDot.MakeRowCandy();
                }
                else
                {
                    board.currentDot.MakeColumnCandy();
                }
            }
            // Is the other piece matched?
            else if (board.currentDot.otherDot != null)
            {
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                // Is the other Dot matched?
                if (otherDot.isMatched)
                {
                    // Make it unmatched 
                    otherDot.isMatched = false;
                    // Decide what kind of bomb to make
                    /*int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        // Make a Row Bomb
                        otherDot.MakeRowCandy();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        // Make a Column Bomb
                        otherDot.MakeColumnCandy();
                    }*/
                    if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                        || board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135)
                    {
                        otherDot.MakeRowCandy();
                    }
                    else
                    {
                        otherDot.MakeColumnCandy();
                    }
                }    
            }
        }
    }

    
}
