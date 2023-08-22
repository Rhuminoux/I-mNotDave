using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningWheel : MonoBehaviour
{
    
    [SerializeField] private Transform m_needle;
    [SerializeField] [Range(1,360)]private int m_degreePerSecond = 180;

    [Header("TargetAngles")]
    [SerializeField]private MyTuple m_lowBonusRange;
    [SerializeField]private MyTuple m_mediumBonusRange;
    [SerializeField]private MyTuple m_highBonusRange;

    #region UPDATE
    private bool _isBlocked = false;
    private float _timeBlocked = 0;
    void Update()
    {
        if (!_isBlocked)
        {
            m_needle.Rotate(0, 0, m_degreePerSecond * Time.deltaTime);
        }
        else
        {
            if (_timeBlocked < 0.5f)
                _timeBlocked += Time.deltaTime;
            else
            {
                _timeBlocked = 0;
                _isBlocked = false;
            }
        }
        if (Input.GetMouseButton(0))
            PlayerClicked();
    }

    private void PlayerClicked()
    {
        _isBlocked = true;
    }
    #endregion
}
