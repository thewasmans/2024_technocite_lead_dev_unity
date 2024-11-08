using System.Linq;
using UnityEngine;

public class DebugReplaceBrick : MonoBehaviour
{
    public ReplaceBrick ReplaceBrick;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        ReplaceBrick.Pattern.ToList().ForEach(t => Gizmos.DrawSphere(t.transform.position, .1f));

        Gizmos.color = Color.red;
        
        if(ReplaceBrick.Current)
            Gizmos.DrawSphere(ReplaceBrick.Current.transform.position, .1f);
    }
}