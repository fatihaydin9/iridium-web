namespace Iridium.Domain.Common;

public class KeyValueDto<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}

public static class DtoMapper
{
    public static KeyValueDto<TKey, TValue> MapToKeyValueDto<TDto, TKey, TValue>(TDto dto, Func<TDto, TKey> keySelector, Func<TDto, TValue> valueSelector)
    {
        return new KeyValueDto<TKey, TValue>
        {
            Key = keySelector(dto),
            Value = valueSelector(dto)
        };
    }

    public static List<KeyValueDto<TKey, TValue>> MapToKeyValueDtoList<TDto, TKey, TValue>(List<TDto> dtoList, Func<TDto, TKey> keySelector, Func<TDto, TValue> valueSelector)
    {
        var keyValueDtoList = new List<KeyValueDto<TKey, TValue>>();
        foreach (var dto in dtoList)
        {
            var keyValueDto = MapToKeyValueDto(dto, keySelector, valueSelector);
            keyValueDtoList.Add(keyValueDto);
        }
        return keyValueDtoList;
    }
}
