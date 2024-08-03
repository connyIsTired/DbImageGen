namespace DbImageGen;

public class LineVerticalState : ILineBuilderState
{

	ForeignKeyLineBuilder Fklb;

	public LineVerticalState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{
		var currTuple = (currentPoint.XPosition, currentPoint.YPosition);
		var returnTuple = new LinePoint{XPosition=currentPoint.XPosition, YPosition=currentPoint.YPosition};
		var filteredList = new List<(int X, int Y, string PointType)>();
		if (CanGoToEndPoint(currentPoint))
		{
			Fklb.State = Fklb.EndState;
			return Fklb.EndPoint;
		}
		if (CanGoToTerminal(currentPoint))
		{
			var endTuple = (Fklb.EndPoint.XPosition, Fklb.EndPoint.YPosition, "E");
			var terminalTuple = Fklb.PointList[endTuple].First();
			Fklb.State = Fklb.HorizontalState;
			return new LinePoint{XPosition=terminalTuple.X, YPosition=terminalTuple.y};
		}
		if (Convert.ToBoolean(Fklb.Direction & 8))
		{
			var filteredYKeys = Fklb.PointList.Keys.Where(k => k.X == currTuple.XPosition && k.Y > currTuple.YPosition && k.PointType == "E");
			foreach(var key in filteredYKeys)
			{

				var result = Convert.ToBoolean(Fklb.Direction & 1) ?
					Fklb.PointList[key].Where(t => t.X > currTuple.XPosition) :
					Fklb.PointList[key].Where(t => t.X < currTuple.XPosition);
				if (result.ToList().Count == 0) {continue;};
				filteredList.Add(key);
			}
			if (filteredList.Count == 0)
			{
				Fklb.Direction = Fklb.Direction - 8 + 4;
				return Fklb.VerticalState.MakePoint(currentPoint);
			}
			var resultTuple = filteredList.MinBy(t => t.Y);
			Fklb.State = Fklb.HorizontalState;
			return new LinePoint{XPosition=resultTuple.X, YPosition=resultTuple.Y};
		}
		if (Convert.ToBoolean(Fklb.Direction & 4))
		{
			var filteredYKeys = Fklb.PointList.Keys.Where(k => k.X == currTuple.XPosition && k.Y < currTuple.YPosition );
			foreach(var key in filteredYKeys)
			{

				var resultList = Convert.ToBoolean(Fklb.Direction & 1) ?
					Fklb.PointList[key].Where(t => t.X > currTuple.XPosition) :
					Fklb.PointList[key].Where(t => t.X < currTuple.XPosition);
				if (resultList.ToList().Count == 0) {continue;};
				filteredList.Add(key);
			}
			var resultTuple = filteredList.MaxBy(t => t.Y);
			Fklb.State = Fklb.HorizontalState;
			return new LinePoint{XPosition=resultTuple.X, YPosition=resultTuple.Y};
		}
		Fklb.State = Fklb.EndState;
		return Fklb.EndPoint;
	}

	private bool CanGoToEndPoint(LinePoint currentPoint)
	{
		// Why am I doing this method in the vertical state?
		var currTuple = (currentPoint.XPosition, currentPoint.YPosition, "T");
		var endTuple = (Fklb.EndPoint.XPosition, Fklb.EndPoint.YPosition, "E");
		return Fklb.PointList[endTuple].Contains(currTuple);
	}

	private bool CanGoToTerminal(LinePoint currentPoint)
	{
		var currTuple = (currentPoint.XPosition, currentPoint.YPosition, "T");
		var endTuple = (Fklb.EndPoint.XPosition, Fklb.EndPoint.YPosition, "E");
		var terminalTuple = Fklb.PointList[endTuple];
		return terminalTuple.First().X == currTuple.XPosition;
	}
}
