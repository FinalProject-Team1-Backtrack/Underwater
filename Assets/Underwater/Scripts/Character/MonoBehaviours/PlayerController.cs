using System.Collections;
using System.Collections.Generic;
using Backtrack.Core;
using UnityEditor;
using UnityEngine;

// A class used to control a player in a Runner
// game. Includes logic for player movement as well as 
// other gameplay logic.
public class PlayerController : MonoBehaviour
{
    // Returns the PlayerController
    public static PlayerController Instance => s_Instance;
    static PlayerController s_Instance;

    enum PlayerSpeedMode
    {
        Slow,
        Medium,
        Fast
    }

    [SerializeField]
    PlayerSpeedMode m_PlayerSpeedMode = PlayerSpeedMode.Slow;

    float[] m_PlayerSpeed = new float[]
    {
        5.0f, // slow
        9.0f, // mid
        15.0f, // fast
    };

    Transform m_Transform;
    Transform m_TrackTransform;
    private Rigidbody m_Rigid;
    Vector3 m_StartPosition;
    Quaternion m_StartRotation;
    float m_Speed;

    void Awake()
    {
        if (s_Instance != null && s_Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;

        Initialize();
    }

    // Set up all necessary values for the PlayerController.
    public void Initialize()
    {
        m_Transform = gameObject.GetComponent<Transform>();
        m_StartPosition = m_Transform.position;
        m_StartRotation = m_Transform.rotation;
        m_Rigid = gameObject.GetComponent<Rigidbody>();

        SetSpeed();
    }

    // Set the player's speed based on speed mode
    private void SetSpeed()
    {
        m_Speed = m_PlayerSpeed[(int)m_PlayerSpeedMode];
        Debug.Log("Current Speed: " + m_Speed + " Current Speed Mode: " + m_PlayerSpeedMode);
    }

    public void Move(Vector2 playerInput)
    {
        m_Rigid.velocity = new Vector3(playerInput.x, playerInput.y, m_Speed);
        if(m_TrackTransform != null)
        {
            m_Rigid.velocity = m_TrackTransform.rotation * m_Rigid.velocity;
        }
    }

    // Returns player to their starting position
    //public void ResetPlayer()
    //{
    //    m_Transform.position = m_StartPosition;
    //    //m_Transform.rotation = m_StartRotation * m_Transform.rotation;
    //    m_PlayerSpeedMode = PlayerSpeedMode.Slow;
    //    SetSpeed();
    //    m_Reset = true;
    //}

    public void Accelerate()
    {
        if (m_PlayerSpeedMode == PlayerSpeedMode.Slow)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Medium;
        }
        else if(m_PlayerSpeedMode == PlayerSpeedMode.Medium)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Fast;
        }

        SetSpeed();
    }

    public void Decelerate()
    {
        if (m_PlayerSpeedMode == PlayerSpeedMode.Medium)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Slow;
        }
        else if (m_PlayerSpeedMode == PlayerSpeedMode.Fast)
        {
            m_PlayerSpeedMode = PlayerSpeedMode.Medium;
        }
        SetSpeed();
    }

    void OnTriggerEnter(Collider col)
    {
        const int tunnel = 6;
        if (col.gameObject.layer == tunnel)
        {
            m_TrackTransform = col.gameObject.transform;
            m_Transform.rotation = m_TrackTransform.rotation * m_Transform.rotation; // 非正交时会有诸多问题
        }
    }
}
