using Normal.Realtime.Serialization;

[RealtimeModel]
public partial class FlashlightModel
{
    [RealtimeProperty(1, true, true)]
    private bool _flashlightOn;
}