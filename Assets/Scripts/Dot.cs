using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    [Header("Board Variables")]
    public int column;
    public int row;
    public int prerviousColumn;
    public int prerviousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;


    private FindMatches findMatches;
    private Board board;
    public GameObject otherDot;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe Stuff")]
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    [Header("Powerup Stuff")]
    public bool isColorBomb;
    public bool isColumnCandy;
    public bool isRowCandy;
    public bool isAdjacentBomb;
    public GameObject adjacentBomb;
    public GameObject rowCandy;
    public GameObject columnCandy;
    public GameObject colorBomb;
    // Start is called before the first frame update
    void Start()
    {
        isColumnCandy = false; 
        isRowCandy = false;
        isColorBomb = false;
        isAdjacentBomb = false;
        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        /*targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        prerviousColumn = column;
        prerviousRow = row;*/
    }

    // This is for testing and debug only

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isColorBomb = true;
            GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
            color.transform.parent = this.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*FindMatches();*/
        /*if(isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(0f, 0f, 0f, .2f);
        }*/
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            // Move Towards the target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.05f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            // Directly set the position
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            
        }
        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            // Move Towards the target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, 0.05f);
            if (board.allDots[column, row] != this.gameObject)
            {
                board.allDots[column, row] = this.gameObject;
            }
            findMatches.FindAllMatches();
        }
        else
        {
            // Directly set the position
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }
        
    public IEnumerator CheckMoveCo() // Coroutine function
    {   
        if (isColorBomb)
        {
            // This piece is a color bomb, and the other piece is the color to destroy
            findMatches.MatchPiecesOfColor(otherDot.tag);
            isMatched = true;
        }
        else if (otherDot.GetComponent<Dot>().isColorBomb)
        {
            // The other piece is a color bomb, and this piece has the color to destroy
            findMatches.MatchPiecesOfColor(this.gameObject.tag);
            otherDot.GetComponent<Dot>().isMatched = true;
        }
            
            
        yield return new WaitForSeconds(0.3f);
        if (otherDot != null)
        {
            if(!isMatched && !otherDot.GetComponent<Dot>().isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;
                row = prerviousRow;
                column = prerviousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentDot = null;
                board.currentState = GameState.move;

            }
            else
            {
                board.DestroyMatches();                
            }
            /*otherDot = null;*/
        }    
    }    

    private void OnMouseDown()
    {
        if (board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if (board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle() // calculate the swipe angle
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MovePieces();
            board.currentState = GameState.wait;
            board.currentDot = this;
        }
        else
        {
            board.currentState = GameState.move;
        }
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            // Right Swipe
            otherDot = board.allDots[column + 1, row];
            prerviousColumn = column;
            prerviousRow = row;
                otherDot.GetComponent<Dot>().column -= 1;
            column += 1;    
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            // Up Swipe
            otherDot = board.allDots[column, row + 1];
            prerviousColumn = column;
            prerviousRow = row;
            otherDot.GetComponent<Dot>().row -= 1;
            row += 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0)
        {
            // Left Swipe
            otherDot = board.allDots[column - 1, row];
            prerviousColumn = column;
            prerviousRow = row;
            otherDot.GetComponent<Dot>().column += 1;
            column -= 1;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            // Down Swipe
            otherDot = board.allDots[column, row - 1];
            prerviousColumn = column;
            prerviousRow = row;
            otherDot.GetComponent<Dot>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    void FindMatches()
    {
        if (column > 0 && column < board.width - 1)
        {
            GameObject leftDot1 = board.allDots[column - 1, row];
            GameObject rightDot1 = board.allDots[column + 1, row];
            if ((leftDot1 != null) && (rightDot1 != null))
            {
                if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag)
                {
                    leftDot1.GetComponent<Dot>().isMatched = true;
                    rightDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
            
        }
        if (row > 0 && row < board.height - 1)
        {
            GameObject upDot1 = board.allDots[column, row + 1];
            GameObject downDot1 = board.allDots[column, row - 1];
            if ((upDot1 != null) && (downDot1 != null))
            {
                if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag)
                {
                    upDot1.GetComponent<Dot>().isMatched = true;
                    downDot1.GetComponent<Dot>().isMatched = true;
                    isMatched = true;
                }
            }
        }
    }

    public void MakeRowCandy()
    {
        isRowCandy = true;
        GameObject arrow = Instantiate(rowCandy, transform.position,Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColumnCandy()
    {
        isColumnCandy = true;
        GameObject arrow = Instantiate(columnCandy, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColorBomb()
    {
        isColorBomb = true;
        GameObject color = Instantiate(colorBomb, transform.position, Quaternion.identity);
        color.transform.parent = this.transform;
    }
    public void MakeAdjacentBomb()
    {
        isAdjacentBomb = true;
        GameObject marker = Instantiate(adjacentBomb, transform.position, Quaternion.identity);
        marker.transform.parent = this.transform;   
    }
}
