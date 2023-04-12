using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.Timeline;
[TrackBindingType(typeof(Animator))]
[TrackClipType(typeof(SuperMoveClip))]
public class SuperMoveTrack : TrackAsset
{
   public override Playable CreateTrackMixer(PlayableGraph graph,GameObject go, int inputCount){
        return ScriptPlayable<SuperMoveTrackMixer>.Create(graph,inputCount);
   }
}
