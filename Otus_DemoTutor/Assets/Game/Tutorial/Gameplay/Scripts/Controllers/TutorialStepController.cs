using System;
using Game.Tutorial.App;
using GameSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Tutorial.Gameplay
{
    public abstract class TutorialStepController : MonoBehaviour,
        IGameConstructElement,
        IGameReadyElement,
        IGameStartElement,
        IGameFinishElement
    {
        [FormerlySerializedAs("step")]
        [SerializeField]
        private TutorialStep step;

        private TutorialManager tutorialManager;

        public virtual void ConstructGame(GameContext context)
        {
            this.tutorialManager = TutorialManager.Instance;
        }

        protected virtual string DescriptionForTutorialAnalytics => step.ToString();
        
        public virtual void ReadyGame()
        {
            this.tutorialManager.OnStepFinished += this.CheckForFinish;
            this.tutorialManager.OnNextStep += this.CheckForStart;
        }

        public virtual void StartGame()
        {
            //Debug.Log($"[{this.name}]: [{this.step}] IsStepPassed[{this.tutorialManager.IsStepPassed(this.step)}] CurrentStep[{this.tutorialManager.CurrentStep}]");
            var stepFinished = this.tutorialManager.IsStepPassed(this.step);
            if (!stepFinished)
            {
                this.CheckForStart(this.tutorialManager.CurrentStep);
            }
        }

        public virtual void FinishGame()
        {
            this.tutorialManager.OnStepFinished -= this.CheckForFinish;
            this.tutorialManager.OnNextStep -= this.CheckForStart;
        }

        protected virtual void OnStart()
        {
            TutorialAnalytics.LogEventAndCache($"{GetBodyLogMsg}_started");
        }

        protected virtual void OnStop()
        {
            TutorialAnalytics.LogEventAndCache($"{GetBodyLogMsg}_completed");
        }

        protected void NotifyAboutComplete()
        {
            if (this.tutorialManager.CurrentStep != this.step)
                throw new NotSupportedException("[TutorialStepController]: TutorialStepController wrong step");
            this.tutorialManager.FinishCurrentStep();
        }

        protected void NotifyAboutMoveNext()
        {
            if (this.tutorialManager.CurrentStep != this.step)
                throw new NotSupportedException("[TutorialStepController]: TutorialStepController wrong step");
            this.tutorialManager.MoveToNextStep();
        }

        protected void NotifyAboutCompleteAndMoveNext()
        {
            if (this.tutorialManager.CurrentStep != this.step)
                throw new NotSupportedException("[TutorialStepController]: TutorialStepController wrong step");
            this.tutorialManager.FinishCurrentStep();
            this.tutorialManager.MoveToNextStep();
        }

        protected bool IsStepFinished()
        {
            return this.tutorialManager.IsStepPassed(this.step);
        }

        private void CheckForFinish(TutorialStep step)
        {
            if (this.step == step)
            {
                this.OnStop();
            }
        }

        private void CheckForStart(TutorialStep step)
        {
            if (this.step == step)
            {
                this.OnStart();
            }
        }

        public string GetCurrentNumStep() => $"{this.tutorialManager.CurrentIndex + 1}";
        
        private string GetBodyLogMsg => $"tutorial_step_{this.GetCurrentNumStep()}__{this.DescriptionForTutorialAnalytics}";
    }
}