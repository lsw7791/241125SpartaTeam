using System;
using System.Collections.Generic;

namespace GoogleSheet.Type
{
    [Type(typeof(List<float>), "List<Single>")]
    public class FloatListType : IType
    {
        public object DefaultValue => new List<float>(); // �⺻���� �� ����Ʈ

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
                    // ���� ó��: �Ľ� ���� �� ���ܸ� �����ų� �⺻���� ��ȯ�� �� �ֽ��ϴ�.
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
