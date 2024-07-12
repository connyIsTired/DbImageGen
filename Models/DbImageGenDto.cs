namespace DbImageGen;

public class DbImageGenDto
{
	public List<TableDto> Tables { get; set;} = new List<TableDto>();
	public List<ForeignKeyLine> ForeignKeys { get; set; } = new List<ForeignKeyLine>();
}
