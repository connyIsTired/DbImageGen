namespace DbImageGen;

public interface IBuilder
{
	void BuildParentSvg();
	void BuildCloseSvgTag();
	void BuildText(string input);
	void BuildRect();
	string Build(string input);
}
