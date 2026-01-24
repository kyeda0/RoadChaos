using UnityEngine;

[CreateAssetMenu(fileName = "EnemyCarFactory", menuName = "Scriptable Objects/EnemyCarFactory")]
public class EnemyCarFactory : ScriptableObject,ITransportFactory
{
    public Transport prefabEnemyCar; 
    
    public Transport Create()
    {
        return Instantiate(prefabEnemyCar);
    }
}
