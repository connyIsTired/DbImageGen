namespace DbImageGen;

public interface ILineBuilderState
{
	public LinePoint MakePoint(LinePoint currentPoint);
}
