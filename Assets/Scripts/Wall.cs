using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private List<Brick> bricks;

    private void Awake()
    {
        bricks = new List<Brick>();
    }

    public void SetBrick(Brick brick)
    {
        bricks.Add(brick);
    }

}
