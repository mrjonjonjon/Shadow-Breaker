using System;
using System.Collections.Generic;

namespace HFSM {
    
    public abstract class StateMachine {
        
        private StateMachine currentSubState;
        private StateMachine defaultSubState;
        private StateMachine parent;
        
        private Dictionary<Type, StateMachine> subStates = new Dictionary<Type, StateMachine>();
        private Dictionary<int, StateMachine> transitions = new Dictionary<int, StateMachine>();


       
        public void EnterStateMachine() {
            OnEnter();
            if (currentSubState == null && defaultSubState != null) {
                currentSubState = defaultSubState;
            }
            currentSubState?.EnterStateMachine();
        }

        public void UpdateStateMachine() {
            OnUpdate();
            currentSubState?.UpdateStateMachine();
        }

        public void ExitStateMachine() {
            currentSubState?.ExitStateMachine();
            OnExit();
        }


        protected virtual void HandleTransition(){}
                
        protected virtual void OnEnter() { }
        
        protected virtual void OnUpdate() { }
        
        protected virtual void OnExit() { }

        //add child
        public void LoadSubState(StateMachine subState) {
            if (subStates.Count == 0) {
                defaultSubState = subState;
            }

            subState.parent = this;
            try {
                subStates.Add(subState.GetType(), subState);
            }
            catch (ArgumentException) {
                throw new Exception($"State {GetType()} already contains a substate of type {subState.GetType()}");
            }
            
        }
        
        public void AddTransition(StateMachine from, StateMachine to, int trigger) {
            if (!subStates.TryGetValue(from.GetType(), out _)) {
                throw new Exception($"State {GetType()} does not have a substate of type {from.GetType()} to transition from.");
            }
            
            if (!subStates.TryGetValue(to.GetType(), out _)) {
                throw new Exception($"State {GetType()} does not have a substate of type {to.GetType()} to transition from.");
            }
            
            try {
                from.transitions.Add(trigger, to);
            }
            catch (ArgumentException) {
                throw new Exception($"State {from} already has a transition defined for trigger {trigger}");
            }

        }


        
        private void ChangeSubState(StateMachine state) {
            currentSubState?.ExitStateMachine();
            var newState = subStates[state.GetType()];
            currentSubState = newState;
            newState.EnterStateMachine();
        }
    }
}