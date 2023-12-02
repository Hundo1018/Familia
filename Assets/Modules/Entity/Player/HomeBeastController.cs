using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using System;

public class HomeBeastController : MonoBehaviourEntity
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private List<PlayerController> _passengers;
    
    public PlayerController Driver = null;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Hitted(EffectPacket effectPacket)
    {
    }

    public void OnDrive(Vector2 vector2)
    {

    }

    /// <summary>
    /// 登上
    /// </summary>
    /// <param name="playerController"></param>
    /// <returns>是否成為駕駛員</returns>
    public bool TryOnBoard(in PlayerController playerController)
    {
        _passengers.Add(Driver);
        if (Driver == null)
        {
            Driver = playerController;
            playerController.Driving += OnDrive;
            return true;
        }

        return false;
    }

    public bool TryOffBoard(in PlayerController playerController)
    {
        if (playerController == Driver)
        {
            Driver = null;
            playerController.Driving -= OnDrive;
        }
        return _passengers.Remove(playerController);
    }
}
