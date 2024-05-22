using DbImageGen.Models;
namespace DbImageGen;

public class DtoBuilder
{
	private DbImageGenRequest Incoming { get; init; }
	private int Offset { get; set; }
	private int TableLength { get; set; }

	public DtoBuilder(DbImageGenRequest incoming)
	{
		Offset = 45;
		Incoming = incoming;
	}

	public DbImageGenDto CreateDto()
	{
		var tableList = new List<TableDto>();
		var tableDto = new TableDto();
		var fields = new List<FieldDto>();
		foreach (var f in Incoming.Table.First().fields)
		{
			var fieldsDto = new FieldDto();
			fieldsDto.Offset = Offset;
			fieldsDto.FieldName = f;
			Offset += 20;
			fields.Add(fieldsDto);
		}
		tableDto.Fields = fields;
		tableDto.TableName = Incoming.Table.First().tableName;
		tableDto.TableSize = SetTableLength();
		tableList.Add(tableDto);
		return new DbImageGenDto{ Tables = tableList};
	}

	public int SetTableLength()
	{
		var fieldCount = Incoming.Table.First().fields.Count();
		return 20 + fieldCount * 20;
	}
	

}
