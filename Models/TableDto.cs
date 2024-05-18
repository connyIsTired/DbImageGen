namespace DbImageGen;

public class TableDto
{
	public string TableName { get; set; } = string.Empty;
	public List<FieldDto> Fields { get; set; } = new List<FieldDto>();
}
