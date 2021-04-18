using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handle everything to do with our tank movement
/// </summary>
[System.Serializable]
public class TankMovement
{
    #region public variables
    public float speed = 12f; // the speed our tank moves
    public float turnSpeed = 180f; // the speed that we can turn in degrees in seconds.
    public float turretTurnSpeed = 20f; // the speed our turret turns in degrees in seconds

    public TankSoundEffects tankSoundEffects = new TankSoundEffects(); // creating a new instance of our tank sound effects class
    public Transform turretTransform; // a reference to the transform of the turret
    public Resources resources; // a reference to the Resource script
    // public Stats stats; // a reference to our stats script

    public bool debuggingEnabled = false;
    #endregion

    #region private variables
    private bool enableMovement = true; // if this is true we are allowed to accept input from the player

    private TankParticleEffects tankParticleEffects = new TankParticleEffects(); // creating a new instance of our tank particle effects class
    private Transform tankReference; // a reference to the tank gameobject
    private Rigidbody rigidbody;// a reference to the rigidbody on our tank
    #endregion

    /// <summary>
    /// Handles the set up of our tank movement script
    /// </summary>
    /// <param name="Tank"></param>
    public void SetUp(Transform Tank)
    {
        tankReference = Tank;
        if (tankReference.GetComponent<Rigidbody>())
        {
            rigidbody = tankReference.GetComponent<Rigidbody>(); // grab a reference to our tanks rigidbody
        }
        else
        {
            Debug.LogError("No Rigidbody attached to the tank");
        }
        tankParticleEffects.SetUpEffects(tankReference); // set up our tank effects
        tankSoundEffects.SetUp(tankReference);
        tankParticleEffects.PlayDustTrails(true);// start playing tank particle effects
        EnableTankMovement(false);
    }

    /// <summary>
    /// Tells our tank if it's allowed to move or not
    /// </summary>
    /// <param name="Enabled"></param>
    public void EnableTankMovement(bool Enabled)
    {
        enableMovement = Enabled;
    }

    /// <summary>
    /// Handles the movement of our tank and our turret
    /// </summary>
    public void HandleMovement(float ForwardMovement, float RotationMovement, float TurretRotationMovement)
    {
        // if we can't move don't
        if(enableMovement == false || resources.fuel.CurrentFuel <= 0) // checks enable movement or fuel value
        {
            #region debugging
            if (debuggingEnabled)
            {
                Debug.Log("Player unable to move");
            }
            #endregion
            return;
        }
        
        Move(ForwardMovement); // moves tank forward/back
        Turn(RotationMovement); // turns tank left/right on y axis
        TurretTurn(TurretRotationMovement); // turns turret left/right on y axis
        tankSoundEffects.PlayTankEngine(ForwardMovement, RotationMovement); // update our audio based on our input
    }

    /// <summary>
    /// Moves the tank forward and back
    /// </summary>
    private void Move(float ForwardMovement)
    {
        // create a vector based on the forward vector of our tank, move it forwad or backwards on nothing based on the key input, multiplied by the speed, multipled by the time between frames rendered to make it smooth
        Vector3 movementVector = tankReference.forward * ForwardMovement * speed * Time.deltaTime;
        //Debug.Log(movementVector);
        if(ForwardMovement > 0 || ForwardMovement < 0) // if fuel is not zero, use fuel when moving
        {
            resources.fuel.UseFuel(0.01f);
        }
        rigidbody.MovePosition(rigidbody.position + movementVector); // move our rigibody based on our current position + our movement vector
    }
    /// <summary>
    /// Rotates the tank on the Y axis
    /// </summary>
    private void Turn(float RotationalAmount)
    {
        // get the key input value, multiply it by the turn speed, multiply it by the time between frames
        float turnAngle = RotationalAmount * turnSpeed * Time.deltaTime; // the angle in degrees we want to turn our tank
        Quaternion turnRotation = Quaternion.Euler(0f, turnAngle, 0); // essentially turn our angle into a quarternion for our rotation

        if (RotationalAmount > 0 || RotationalAmount < 0) // if fuel is not zero, use fuel while turning
        {
            resources.fuel.UseFuel(0.005f);
        }

        // update our rigidboy with this new rotation
        rigidbody.MoveRotation(rigidbody.rotation * turnRotation); // rotate our rigidbody based on our input.
    }

    /// <summary>
    /// Rotates the Turret on the Y axis
    /// </summary>
    /// <param name="RotationalAmount"></param>
    private void TurretTurn(float RotationalAmount)
    {
        turretTransform.Rotate(0, RotationalAmount * turretTurnSpeed * Time.deltaTime, 0, 0); // rotates the turrets transform on the y axis, by the turret speed per second
    }

    public void UpgradeTurretSpeed(float amount)
    {
        turretTurnSpeed += amount;

        if (debuggingEnabled)
        {
            Debug.Log("turret speed is " + turretTurnSpeed);
        }
    }

    public void UpgradeTankSpeed(float amount)
    {
        speed += amount;

        if (debuggingEnabled)
        {
            Debug.Log("tank speed is " + speed);
        }
    }
}

