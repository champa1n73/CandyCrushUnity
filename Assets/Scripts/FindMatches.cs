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
                    if (i > 0 && i < board.width - 1)
                    {
                        GameObject leftDot = board.allDots[i - 1, j];
                        GameObject rightDot = board.allDots[i + 1, j];
                        if (leftDot != null && rightDot != null)
                        {
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {
                                if (currentDot.GetComponent<Dot>().isRowCandy
                                    || leftDot.GetComponent<Dot>().isRowCandy
                                    || rightDot.GetComponent<Dot>().isRowCandy)
                                {
                                    currentMatches.Union(getRowPieces(j));
                                }

                                if (currentDot.GetComponent<Dot>().isColumnCandy)
                                {
                                    currentMatches.Union(getColumnPieces(i));
                                }

                                if (leftDot.GetComponent<Dot>().isColumnCandy)
                                {
                                    currentMatches.Union(getColumnPieces(i-1));
                                }

                                if (rightDot.GetComponent<Dot>().isColumnCandy)
                                {
                                    currentMatches.Union(getColumnPieces(i + 1));
                                }

                                if (!currentMatches.Contains(leftDot)) 
                                {
                                    currentMatches.Add(leftDot);
                                }
                                if (!currentMatches.Contains(rightDot))
                                {
                                    currentMatches.Add(rightDot);
                                }
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                leftDot.GetComponent<Dot>().isMatched = true;
                                rightDot.GetComponent<Dot>().isMatched = true;
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];
                        GameObject downDot = board.allDots[i, j - 1];
                        if (upDot != null && downDot != null)
                        {
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {
                                if (currentDot.GetComponent<Dot>().isColumnCandy
                                    || upDot.GetComponent<Dot>().isColumnCandy
                                    || downDot.GetComponent<Dot>().isColumnCandy)
                                {
                                    currentMatches.Union(getColumnPieces(i));
                                }

                                if (currentDot.GetComponent<Dot>().isRowCandy)
                                {
                                    currentMatches.Union(getRowPieces(j));
                                }

                                if (upDot.GetComponent<Dot>().isRowCandy)
                                {
                                    currentMatches.Union(getRowPieces(j + 1));
                                }

                                if (downDot.GetComponent<Dot>().isRowCandy)
                                {
                                    currentMatches.Union(getRowPieces(j - 1));
                                }

                                if (!currentMatches.Contains(upDot))
                                {
                                    currentMatches.Add(upDot);
                                }
                                if (!currentMatches.Contains(downDot))
                                {
                                    currentMatches.Add(downDot);
                                }
                                if (!currentMatches.Contains(currentDot))
                                {
                                    currentMatches.Add(currentDot);
                                }
                                upDot.GetComponent<Dot>().isMatched = true;
                                downDot.GetComponent<Dot>().isMatched = true;
                                currentDot.GetComponent<Dot>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
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
