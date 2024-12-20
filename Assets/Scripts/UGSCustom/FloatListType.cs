using System;
using System.Collections.Generic;

namespace GoogleSheet.Type
{
    [Type(typeof(List<float>), "List<Single>")]
    public class FloatListType : IType
    {
        public object DefaultValue => new List<float>(); // 기본값은 빈 리스트

        public object Read(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DefaultValue;

            var list = new List<float>();
            var values = value.Split(',');

            foreach (var v in values)
            {
                if (float.TryParse(v.Trim(), out float result))
                {
                    list.Add(result);
                }
                else
                {
                    // 오류 처리: 파싱 실패 시 예외를 던지거나 기본값을 반환할 수 있습니다.
                    throw new FormatException($"Invalid float value: {v}");
                }
            }
            return list;
        }

        public string Write(object value)
        {
            if (value is List<float> list)
            {
                return string.Join(",", list);
            }
            else
            {
                throw new ArgumentException("Value must be of type List<float>");
            }
        }
    }
}
