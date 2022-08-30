using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Chase, Flee, Patrol, Attack };
    public AIState currentState;

    public PlayerController target;
    public PlayerController activeTarget;

    public float hearingDistance;
    public float fieldOfView;
    public float attemptShootDistance;

    //list for patrol points
    public List<GameObject> patrolPoints;
    private int i = 0;

    private Health health;

    private int j = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //if we have a game manager
        if (GameManager.instance != null)
        {
            //tracks the AI
            if (GameManager.instance.aIControllers != null)
            {
                //adds it to the game managers
                GameManager.instance.aIControllers.Add(this);
            }
        }
        health = GetComponent<Health>();
        //sets the tank to default of patrol on start
        currentState = AIState.Patrol;
    }

    public override void Update()
    {
        MakeDecisions();
    }

    //switch cases for the state changes
    public void MakeDecisions()
    {
        switch (currentState)
        {
            //patrol case
            case AIState.Patrol:
                //runs the targeting system
                targetPlayer();
                //moves the AI along the list of patrol points
                if (Vector3.Distance(transform.position, patrolPoints[i].transform.position) > 0.5)
                {
                    pawn.RotateTowards(patrolPoints[i].transform.position);
                    pawn.MoveForward();

                    if (Vector3.Distance(transform.position, patrolPoints[i].transform.position) <= 0.7)
                    {
                        i++;
                    }
                }
                if (i == patrolPoints.Count)
                {
                    i = 0;
                }
                //if the AI has a target
                if (activeTarget != null)
                {
                    //if the AI can hear or can see the target
                    if (CanHear(activeTarget) || CanSee(activeTarget))
                    {
                        //if the AI health is above 50
                        if (health.currentHealth > 50)
                        {
                            //switches to chase
                            currentState = AIState.Chase;
                        }
                        //otherwise if its under 50 
                        else
                        {
                            //change state to flee
                            currentState = AIState.Flee;
                        }
                    }
                }
                break;
                //chase case
            case AIState.Chase:
                //if the AI has an active target
                if (activeTarget.pawn != null)
                {
                    //move towards the target
                    pawn.RotateTowards(activeTarget.pawn.gameObject.transform.position);
                    pawn.MoveForward();
                }
                else
                {
                    //otherwise has no target
                    activeTarget = null;
                }
                //if the AI has a target
                if (activeTarget != null)
                {
                    //if the AI can not see or hear
                    if (!CanSee(activeTarget) && !CanHear(activeTarget))
                    {
                        //change to patrol and set target to null
                        currentState = AIState.Patrol;
                        activeTarget = null;
                    }
                }
                else
                {
                    //otherwise if they AI doesnt have a target set to patrol
                    currentState = AIState.Patrol;
                }
                //if the AI has a target
                if (activeTarget != null)
                {
                    //if the AI is within shooting distance
                    if (Vector3.Distance(transform.position, activeTarget.pawn.gameObject.transform.position) < attemptShootDistance)
                    {
                        //change to Attack
                        currentState = AIState.Attack;
                    }
                }
                //otherwise change to patrol
                else
                {
                    currentState = AIState.Patrol;
                }
                //if the health is under 50
                if (health.currentHealth < 50)
                {
                    //if the AI has a target
                    if (activeTarget != null)
                    {
                        //if the AI can see or can hear
                        if (CanSee(activeTarget) || CanHear(activeTarget))
                        {
                            //Flee
                            currentState = AIState.Flee;
                        }
                    }
                    //otherwise patrol
                    else
                    {
                        currentState = AIState.Patrol;
                    }
                }
                break;
                //case for flee
            case AIState.Flee:
                //if the AI has a target
                if (activeTarget.pawn != null)
                {
                    //move away from the target
                    pawn.RotateTowards(-activeTarget.pawn.gameObject.transform.position);
                    pawn.MoveForward();
                }
                //otherwise set target to null
                else
                {
                    activeTarget = null;
                }
                // if AI has a target
                if (activeTarget != null)
                {
                    //if it cannot see or cannot hear
                    if (!CanSee(activeTarget) && !CanHear(activeTarget))
                    {
                        //go back to patrolling and set target to null
                        currentState = AIState.Patrol;
                        activeTarget = null;
                    }
                }
                //otherwise patrol
                else
                {
                    currentState = AIState.Patrol;
                }
                //if health goes above 50
                if (health.currentHealth > 50)
                {
                    //if AI has a target
                    if (activeTarget != null)
                    {
                        //if the AI can see or can hear
                        if (CanSee(activeTarget) || CanHear(activeTarget))
                        {
                            //chase
                            currentState = AIState.Chase;
                        }
                    }
                    //otherwise patrol
                    else
                    {
                        currentState = AIState.Patrol;
                    }
                }
                break;
                //case for Attack
            case AIState.Attack:
                //if the AI has a target
                if (activeTarget.pawn != null)
                {
                    //rotate towards target and shoot
                    pawn.RotateTowards(activeTarget.pawn.gameObject.transform.position);
                    pawn.Shoot();
                }
                //otherwise set target to null
                else
                {
                    activeTarget = null;
                }

                //if the AI has a target
                if (activeTarget != null)
                {
                    //if the AI can see or can hear
                    if (CanSee(activeTarget) || CanHear(activeTarget))
                    {
                        //if the AI is not in shooting distance
                        if (Vector3.Distance(transform.position, activeTarget.pawn.gameObject.transform.position) > attemptShootDistance)
                        {
                            //chase
                            currentState = AIState.Chase;
                        }
                    }
                }
                //otherwise patrol
                else
                {
                    currentState = AIState.Patrol;
                }
                //if target health goes under 50
                if (health.currentHealth < 50)
                {
                    //Flee
                    currentState = AIState.Flee;
                }
                break;
        }
    }
    //AI targeting
    public void targetPlayer()
    {
        //if the game manager exists
        if (GameManager.instance != null)
        {
            //if the player controller exists
            if (GameManager.instance.playerControllers != null)
            {
                //if the playercontroller in game is more than 1
                if (GameManager.instance.playerControllers.Count > 0)
                {
                    //if j is greather than or equal to player controllers count target the player controller
                    if (j >= GameManager.instance.playerControllers.Count)
                    {
                        j = 0;
                    }
                    target = GameManager.instance.playerControllers[j];
                    //if AI can see or can hear that target then set that target as the active target
                    if (CanSee(target) || CanHear(target))
                    {
                        activeTarget = target;
                    }
                }
            }
        }
    }
    //can hear bool
    public bool CanHear(PlayerController target)
    {
        //if the target is null return false
        if (target.pawn == null)
        {
            return false;
        }
        // If they are making 0 noise, they also can't be heard
        if (target.volumeDistance <= 0)
        {
            return false;
        }
        // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
        float playerTankVolume = target.volumeDistance;
        //if target is moving
        if (target.isMoving)
        {
            //player tank volume is added to moving volume
            playerTankVolume += target.movingVolumeDistance;
        }
        //total distance is equal to the player tank volume plus the hearing distance
        float totalDistance = playerTankVolume + hearingDistance;
        // If the distance between our pawn and target is closer than this...
        if (Vector3.Distance(pawn.transform.position, target.pawn.gameObject.transform.position) <= totalDistance)
        {
            // ... then we can hear the target
            return true;
        }
        else
        {
            // Otherwise, we are too far away to hear them
            return false;
        }
    }
    //can see bool
    public bool CanSee(PlayerController target)
    {
        //if target is null return false
        if (target.pawn == null)
        {
            return false;
        }
        GameObject targetTank = target.pawn.gameObject;
        // Find the vector from the agent to the target
        Vector3 agentToTargetVector = targetTank.transform.position - transform.position;
        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        // if that angle is less than our field of view
        if (angleToTarget < fieldOfView)
        {
            //casts a ray to see if it hits the target
            RaycastHit hit;
            //if raycast does hit the tank return true
            if (Physics.Raycast(transform.forward, targetTank.transform.position - transform.forward, out hit))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == targetTank)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    //removes the Ai controller from the game manager if destroyed
    public override void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.aIControllers != null)
            {
                GameManager.instance.aIControllers.Remove(this);
            }
        }
    }
}
