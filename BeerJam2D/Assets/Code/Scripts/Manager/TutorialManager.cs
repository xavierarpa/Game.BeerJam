using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int _index;
    public int Index
    {
        get
        {
            return _index;
        }
        set
        {
            if (_index == value)
            {
                //Nada
            }
            else
            {
                //
                ActivityTutorialScreen(_index, value);

            }
            _index = value;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ActivityTutorialScreen(int lastIndex, int nextIndex)
    {

    }

}
