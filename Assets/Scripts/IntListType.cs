using System;
using System.Collections.Generic;

namespace GoogleSheet.Type
{
    [Type(typeof(List<int>), "List<Int32>")]
    public class IntListType : IType
    {
        public object DefaultValue => new List<int>(); // 기본값은 빈 리스트

        public object Read(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DefaultValue;

            var list = new List<int>();
            var values = value.Split(',');

            foreach (var v in values)
            {
                if (int.TryParse(v.Trim(), out int result))
                {
                    list.Add(result);
                }
                else
                {
                    // 오류 처리: 파싱 실패 시 예외를 던지거나 기본값을 반환할 수 있습니다.
                    throw new FormatException($"Invalid integer value: {v}");
                }
            }
            return list;
        }

        public string Write(object value)
        {
            if (value is List<int> list)
            {
                return string.Join(",", list);
            }
            else
            {
                throw new ArgumentException("Value must be of type List<int>");
            }
        }
    }
}
