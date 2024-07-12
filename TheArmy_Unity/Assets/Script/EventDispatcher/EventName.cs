using System.Collections.Generic;

public enum EventName
{
    NONE,
    MONEY_CHANGE,
    STAR_CHANGE,
    UPGRADE_POWER,
    UPGRADE_HEALTH,
    UPGRADE_ATKSPD,
    UNLOCK_NEWUNIT,
    LOCK_NEWUNIT,
    ALLY_CHANGE
}

public class EventTypeComparer : IEqualityComparer<EventName>
{
    public bool Equals(EventName x, EventName y)
    {
        return x == y;
    }

    public int GetHashCode(EventName t)
    {
        return (int)t;
    }
}
