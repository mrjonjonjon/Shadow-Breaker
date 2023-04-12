using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.Timeline;
public class SuperMoveTrackMixer : PlayableBehaviour
{
 public override void ProcessFrame(Playable playable, FrameData info, object playerData){
    Animator anim = playerData as Animator;
 }
}
