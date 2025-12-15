using System.Collections;
using System.Collections.Generic;
using extOSC;
using UnityEngine;

public class TowerDefendOsc : MonoBehaviour
{
    public OSCReceiver receiver;
    public OSCTransmitter transmitter;

    public Player playerScript;

    int previousButtonState = 1;

    // Start is called before the first frame update
    void Start()
    {
        receiver.Bind(
            "/button",
            (message) =>
            {
                int value = message.Values[0].IntValue;

                if (value != previousButtonState)
                {
                    previousButtonState = value;
                    if (value == 0)
                        playerScript.Shoot();
                }
            }
        );

        receiver.Bind(
            "/angle",
            (message) =>
            {
                int value = message.Values[0].IntValue;
                float angle = (float)value / 4095f * 240f - 120f;
                playerScript.SetPlayerRotation(angle);
            }
        );
    }

    void FixedUpdate()
    {
        if (playerScript.getCanFire())
        {
            OSCMessage message = new OSCMessage("/pixel");
            message.AddValue(OSCValue.Int(0));
            message.AddValue(OSCValue.Int(255));
            message.AddValue(OSCValue.Int(0));
            transmitter.Send(message);
        }
        else
        {
            OSCMessage message = new OSCMessage("/pixel");
            message.AddValue(OSCValue.Int(0));
            message.AddValue(OSCValue.Int(0));
            message.AddValue(OSCValue.Int(0));
            transmitter.Send(message);
        }
    }

    // Update is called once per frame
    void Update() { }
}
