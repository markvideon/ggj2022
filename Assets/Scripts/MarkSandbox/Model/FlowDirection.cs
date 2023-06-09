using System;

// If enum is the first value,
// there's some kind of error in logic.
public enum FlowDirection
{
  error,
  forward,
  backward
}

public static class FlowDirectionUtility
{
  public static float stasisThreshold { get { return 0.1f;  } }
  public static bool sameFlowDirection(FlowDirection thisDirection, FlowDirection other)
  {
    return thisDirection == FlowDirection.forward && other == FlowDirection.forward ||
      thisDirection == FlowDirection.backward && other == FlowDirection.backward;
  }

  public static string directionAsString(FlowDirection direction)
  {
    switch (direction)
    {
      case FlowDirection.forward:
        return "Forward";
      case FlowDirection.backward:
        return "Backward";
      default:
        throw new Exception("Not defined.");
    }
  }
}
