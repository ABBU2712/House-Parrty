using Assets.SuperGoalie.Scripts.States.GoalKeeperStates.InteractWithBall.SubStates;
using RobustFSM.Base;

namespace Assets.SuperGoalie.Scripts.States.GoalKeeperStates.InteractWithBall.MainState
{
    public class InteractWithBallMainState : BHState
    {
        public override void AddStates()
        {
            AddState<CatchBall>();
            AddState<CheckIfBallIsCatchableOrPunchable>();
            AddState<ClearBall>();

            SetInitialState<CheckIfBallIsCatchableOrPunchable>();
        }
    }
}
