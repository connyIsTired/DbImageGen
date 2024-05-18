namespace DbImageGen;

public interface IBuilder
{
	void BuildParentSvg();
	void BuildCloseSvgTag();
	string BuildText(string input);
	void BuildAllText();
	void BuildRect();
	string Build();
}
