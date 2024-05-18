namespace DbImageGen;

public class Builder : IBuilder
{
	private string returnObj = string.Empty;
	public Builder()
	{
	}

	public void BuildParentSvg()
	{
		returnObj = returnObj + "<svg width=\"1000\" height=\"1000\" xmlns=\"http://www.w3.org/2000/svg\">";
	}

	public void BuildCloseSvgTag()
	{
		returnObj = returnObj + "</svg>";
	}

	public void BuildText(string input)
	{
	    var svgText = $"<text font-size=\"10pt\" x=\"55\" y=\"20\" class=\"small\">{input}</text>";
	    returnObj = returnObj + svgText;
	}

	public void BuildRect()
	{
		returnObj = returnObj + "<rect x=\"50\" y=\"10\" width=\"250\" height=\"250\" fill=\"none\" stroke=\"blue\"/>";
	}
	
	public string Build(string input)
	{
		BuildParentSvg();
		BuildRect();
		BuildText(input);
		BuildCloseSvgTag();
		return returnObj;
	}

}
