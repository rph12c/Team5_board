using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClassBase
{
    private int health;
    private int strength;
    private int range;
    private int travelDistance;
    private string name;

    protected CharacterClassBase(int h, int s, int r, int t, string n)
    {
        health = h;
        strength = s;
        range = r;
        travelDistance = t;
        name = n;
    }

    public int getHealth()
    {
        return health;
    }

    public int getStrength()
    {
        return strength;
    }

    public int getRange()
    {
        return range;
    }

    public int getTravelDistance()
    {
        return travelDistance;
    }

    public string getName()
    {
        return name;
    }

    public bool takeDamage(int points)
    {
        health -= points;
        if (health <= 0)
        {
            return false;
        } else
        {
            return true;
        }
    }
}

public class Cowboy : CharacterClassBase
{
    public Cowboy()
        : base(5, 2, 1, 1, "Cowboy")
    {

    }
}

public class Ghost : CharacterClassBase
{
    public Ghost()
        : base(5, 2, 1, 2, "Ghost")
    {

    }
}

public class Priestess : CharacterClassBase
{
    public Priestess()
        : base(5, 2, 1, 1, "Priestess")
    {

    }
}

public class Rebel : CharacterClassBase
{
    public Rebel()
        : base(5, 2, 1, 1, "Rebel")
    {

    }
}

public class Wanderer : CharacterClassBase
{
    public Wanderer()
        : base(5, 2, 2, 1, "Wanderer")
    {

    }
}

public class Guard : CharacterClassBase
{
    public Guard()
        : base(2, 2, 1, 1, "Guard")
    {

    }
}

public class Berserker : CharacterClassBase
{
    public Berserker()
        : base(2, 2, 1, 1, "Berserker")
    {

    }
}

public class Builder : CharacterClassBase
{
    public Builder()
        : base(1, 1, 1, 1, "Builder")
    {

    }
}

public class Swordsman : CharacterClassBase
{
    public Swordsman()
        : base(1, 1, 1, 1, "Swordsman")
    {

    }
}


