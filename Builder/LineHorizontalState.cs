namespace DbImageGen;

public class LineHorizontalState : ILineBuilderState
{

	ForeignKeyLineBuilder Fklb;

	public LineHorizontalState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{
		if (CanGoToEndPoint(currentPoint))
		{
			Fklb.State = Fklb.EndState;
			return Fklb.EndPoint;
		}
		var currTuple = (currentPoint.XPosition, currentPoint.YPosition, "E");
		if(currTuple == (Fklb.StartingPoint.XPosition, Fklb.StartingPoint.YPosition, "E"))
		{
			var point = Fklb.PointList[currTuple].First();
			Fklb.State = Fklb.VerticalState;
			return new LinePoint{XPosition=point.X, YPosition=point.y};
		}
		// Need to create enum for direction
		// 1 = right or increasing x value
		// 2 = left or decreasing x value
		if (Convert.ToBoolean(Fklb.Direction & 1))
		{
			var filteredSet = Fklb.PointList[currTuple].Where(t => t.y == currTuple.YPosition && t.X > currTuple.XPosition && t.X <= Fklb.EndPoint.XPosition);
			(int X, int Y, string PointType) nextPoint = filteredSet.Count() != 0 ? filteredSet.Last() : currTuple;
			Fklb.State = Fklb.VerticalState;
			return new LinePoint{XPosition=nextPoint.X, YPosition=nextPoint.Y};
		}
		if (Convert.ToBoolean(Fklb.Direction & 2))
		{
			var filteredSet = Fklb.PointList[currTuple].Where(t => t.y == currTuple.YPosition && t.X < currTuple.XPosition && t.X >= Fklb.EndPoint.XPosition-25);
			(int X, int Y, string PointType) nextPoint = filteredSet.Count() != 0 ? filteredSet.Last() : currTuple;
			Fklb.State = Fklb.VerticalState;
			return new LinePoint{XPosition=nextPoint.X, YPosition=nextPoint.Y};
		}
		// Need to think of a better default state. Or maybe not. Is this fine? At this point I am so tired who even cares. It is whatever. just go to 0,0. 
		return new LinePoint{XPosition=0, YPosition = 0};
	}

	private bool CanGoToEndPoint(LinePoint currentPoint)
	{
		var currTuple = (currentPoint.XPosition, currentPoint.YPosition, "T");
		var endTuple = (Fklb.EndPoint.XPosition, Fklb.EndPoint.YPosition, "E");
		return Fklb.PointList[endTuple].Contains(currTuple);
	}
}
