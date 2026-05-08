using Sandbox;
using Sandbox.Citizen;

public sealed class Npcmover : Component
{

	[RequireComponent] CitizenAnimationHelper Animator {get;set;}

	[RequireComponent] NavMeshAgent Agent {get;set;}

	[RequireComponent] Collider Col {get;set;}

	[RequireComponent] ModelPhysics MP {get;set;}

	[Property] GameObject Target {get;set;}

	public bool IsRagDolled {get;set;}
	[Button("Ragdoll")]
	public void Ragdoll(){
		if(IsRagDolled)return;

		Animator.Enabled = false;
		Agent.Enabled = false;
		Col.Enabled = false;

		MP.Enabled = true;
		IsRagDolled = true;

	}

		protected override void OnStart()
		{
			base.OnStart();
			if(Target !=null){
				Agent.MoveTo(Target.WorldPosition);
			}
		}

	protected override void OnUpdate()
	{
		if(Input.Pressed("follow")){
			if(Target !=null){
				Agent.MoveTo(Target.WorldPosition);
			}
		}		
		var targetRot = Rotation.LookAt(Agent.WishVelocity, Vector3.Up);
		WorldRotation = Rotation.Slerp(WorldRotation, targetRot, Time.Delta * 8f);

		Animator.WithVelocity(Agent.Velocity);
		Animator.WithWishVelocity(Agent.WishVelocity);
	}
}
