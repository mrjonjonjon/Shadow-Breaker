using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using TMPro;
using UnityEngine.Timeline;
public class SuperMoveClip : PlayableAsset
{

    public string dummy;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner){

        var playable = ScriptPlayable<SuperMoveBehaviour>.Create(graph);
        SuperMoveBehaviour supermoveBehaviour = playable.GetBehaviour();
        supermoveBehaviour.dummy = "dummytext";

        return playable;
   }

}
