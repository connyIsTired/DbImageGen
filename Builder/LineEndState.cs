namespace DbImageGen;

public class LineEndState : ILineBuilderState
{

	ForeignKeyLineBuilder Fklb;

	public LineEndState(ForeignKeyLineBuilder fklb)
	{
		Fklb = fklb;
	}
	public LinePoint MakePoint(LinePoint currentPoint)
	{
		return new LinePoint{XPosition=currentPoint.XPosition + 50, YPosition=currentPoint.YPosition};
	}
}
