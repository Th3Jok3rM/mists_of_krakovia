using UnityEngine;

[CreateAssetMenu(fileName = "PlayerClass", menuName = "Scriptables/newPlayerClass")]
public class ClassesScriptable : ScriptableObject
{
    public string className;
    public float classSpeed;
    public Transform classVisual;

    public string animIdle;
    public string animWalk;
    public string animAttack;
    public string animDie;
}
