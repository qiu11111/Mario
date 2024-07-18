using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{

    void onEnter();

    void onUpdate();

    void onFixedUpdate();

    void onExit();
}
