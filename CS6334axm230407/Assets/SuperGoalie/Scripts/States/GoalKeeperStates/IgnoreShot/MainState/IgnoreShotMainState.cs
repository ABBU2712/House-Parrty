using Assets.SuperGoalie.Scripts.Entities;
using Assets.SuperGoalie.Scripts.FSMs;
using Assets.SuperGoalie.Scripts.States.GoalKeeperStates.Idle.MainState;
using RobustFSM.Base;

namespace Assets.SuperGoalie.Scripts.States.GoalKeeperStates.IgnoreShot.MainState
{
    public class IgnoreShotMainState : BState
    {
        public override void Enter()
        {
            base.Enter();

            //run the IdleMainState enter function
            Machine.GetState<IdleMainState>().Enter();
        }

        public override void Exit()
        {
            base.Exit();

            //run the IdleMainState exit function
            Machine.GetState<IdleMainState>().Exit();
        }

        GoalKeeper Owner
        {
            get
            {
                return ((GoalKeeperFSM)SuperMachine).Owner;
            }
        }
    }
}
