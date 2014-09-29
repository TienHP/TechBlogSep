using UnityEngine;
using System.Collections;
using StateMachine.Action;

namespace StateMachine{
	public class StateMachineBehaviour: MonoBehaviour {
		public StateMachine stateMachine;
		[HideInInspector]
		public StateMachine executingStateMachine;
		[HideInInspector]
		public AnyState anyState;
		[HideInInspector]
		public State currentState;
		public int group=0;
		public bool start=true;
		private bool isPaused=true;
		private bool initialized=false;
		public delegate void CustomEvent(string eventName);
		public event CustomEvent onReceiveEvent;

		private void Start () {
			enabled = (stateMachine != null && stateMachine.states.Count > 0);
			if (enabled) {
				stateMachine.behaviour=this;
				executingStateMachine=ScriptableObject.CreateInstance<StateMachine>();
				executingStateMachine.owner=gameObject;
				StateMachine.Copy(stateMachine,executingStateMachine,false);
				executingStateMachine.Initialize(this);
				executingStateMachine.name=stateMachine.name;
				currentState= executingStateMachine.states.Find (state => state.isDefaultState == true);
				anyState = (AnyState)executingStateMachine.states.Find (state => state.GetType() == typeof(AnyState));
				if(anyState == null){
					enabled=false;
					return;
				}
				initialized=true;
				if(start){
					EnableStateMachine();
				}
			}
		}

		private State state;
		public virtual void Update () {
			if (isPaused) {
				return;
			}
		//	state = CheckTransition ();
		//	SetState (state);

			anyState.DoUpdate ();
			if (currentState != null) {
				currentState.DoUpdate ();
			}

			state = CheckTransition ();
			SetState (state);
			state = null;
		}

		private void FixedUpdate(){
			if (isPaused) {
				return;
			}
			anyState.DoFixedUpdate ();
			if (currentState != null) {
				currentState.DoFixedUpdate ();
			}
		}

		private void OnAnimatorIK(int layer) {
			if (isPaused) {
				return;
			}
			anyState.DoOnAnimatorIK (layer);
			if (currentState != null) {
				currentState.DoOnAnimatorIK (layer);
			}
		}

		private void OnAnimatorMove() {
			if (isPaused) {
				return;
			}
			anyState.DoAnimatorMove ();
			if (currentState != null) {
				currentState.DoAnimatorMove ();
			}
		}

		public void SetParameter(object[] data){
			executingStateMachine.SetParameter ((string)data [0], data [1]);		
		}

		public NamedParameter GetParameter(string name){
			return executingStateMachine.GetPrameter (name);		
		}

		public void SetStateMachine(StateMachine stateMachine){
			this.stateMachine = stateMachine;
			Start ();
		}

		public void SetDefaultState(){
			currentState.DoExit();
			currentState= executingStateMachine.states.Find (state => state.isDefaultState == true);
			currentState.DoEnter();
		}

		public void SetState(string _name){
			currentState.DoExit();
			currentState= executingStateMachine.states.Find (state => state.name == _name);
			currentState.DoEnter();
		}

		private State CheckTransition(){
			state = (currentState!= null)? currentState.ValidateTransitions ():null;
			if (state == null) {
				state = anyState.ValidateTransitions ();
			}
			return state;
		}

		public void SetState(State state){
			if (state != null) {
				currentState.DoExit();
				currentState = executingStateMachine.states.Find(x=> x.id==state.id);
				currentState.DoEnter();
			}
		}

		public void EnableStateMachine(){
			if (!initialized) {
				Start();			
			}
			isPaused = false;
			anyState.DoEnter ();
			if (currentState != null) {
				currentState.DoEnter();		
			}	
			//state = CheckTransition ();
			//SetState (state);

		}

		public void DisableStateMachine(bool pause){
			if (pause) {
				isPaused = true;
			} else {
				enabled=false;	
			}
		}

		public void SendEvent(string eventName){
			if (onReceiveEvent != null) {
				onReceiveEvent (eventName);
			}
		}
	}
}
