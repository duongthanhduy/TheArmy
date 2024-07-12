
using System;

[Serializable]
public class BaseStatus // live, dead, pause
{

    public BaseStatus() {
        Live = true;
        Pause = false;
    }

    public void SetLive(bool _live) {
        Live = _live;
    }
    public void SetPause(bool _pause) {
        Pause = _pause;
    }
    public bool Live { get;private set;} = true;
    public bool Pause { get;private set;} = false;
    
}
