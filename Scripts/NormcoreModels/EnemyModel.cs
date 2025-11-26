using Normal.Realtime;
using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class EnemyModel
{
    [RealtimeProperty(1, true, true)] private string _currentAnimation;
    [RealtimeProperty(2, true, true)] private bool _behaviorTreeEnabled;
}
