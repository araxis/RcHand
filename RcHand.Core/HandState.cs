namespace RcHand.Core;

public readonly struct HandState
{
    public int ThumbAngle { get; }
    public int IndexAngle { get; }
    public int MiddleAngle { get; }
    public int RingAngle { get; }
    public int LittleAngle { get; }
    public HandState(int thumbAngle, int indexAngle, int middleAngle, int ringAngle, int littleAngle)
    {
        ThumbAngle = thumbAngle;
        IndexAngle = indexAngle;
        MiddleAngle = middleAngle;
        RingAngle = ringAngle;
        LittleAngle = littleAngle;
    }

}