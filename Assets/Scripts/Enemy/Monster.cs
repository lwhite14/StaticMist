using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{ 
    string name;

    public Monster() { }

    public Monster(string name)
    {
        this.name = name;
    }

    public void SetName(string name)
    {
        this.name = name;
    }

    public string GetName() 
    {
        return name;
    }

}
