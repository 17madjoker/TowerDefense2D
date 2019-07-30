using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Node
{
    public int x;
    public int y;

    public Node(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    public static bool operator ==(Node n1, Node n2)
    {
        if (n1.x == n2.x && n1.y == n2.y)
            return true;
        
        return false;
    }

    public static bool operator !=(Node n1, Node n2)
    {
        if (n1.x != n2.x && n1.y != n2.y)
            return true;
        
        return false;
    }
}
