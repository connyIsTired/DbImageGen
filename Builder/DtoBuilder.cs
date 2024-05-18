using DbImageGen.Models;
namespace DbImageGen;

public static class DtoBuilder
{

	public static DbImageGenDto CreateDto(DbImageGenRequest incoming)
	{
		var offset = 25;
		var tableList = new List<TableDto>();
		var tableDto = new TableDto();
		var fields = new List<FieldDto>();
		foreach (var f in incoming.Table.First().fields)
		{
			var fieldsDto = new FieldDto();
			fieldsDto.offset = offset;
			fieldsDto.FieldName = f;
			offset += 20;
			fields.Add(fieldsDto);
		}
		tableDto.Fields = fields;
		tableDto.TableName = incoming.Table.First().tableName;
		tableList.Add(tableDto);
		return new DbImageGenDto{ Tables = tableList};
	}

}
