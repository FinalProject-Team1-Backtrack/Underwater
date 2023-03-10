using UnityEngine;
using System.Collections;


namespace Backtrack.Core
{
    /// <summary>
    /// The event is triggered when the player loses a level.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(LevelLostEvent),
        menuName = "Runner/" + nameof(LevelLostEvent))]
    public class LevelLostEvent : AbstractGameEvent
    {
        public override void Reset()
        {

        }
    }
}


