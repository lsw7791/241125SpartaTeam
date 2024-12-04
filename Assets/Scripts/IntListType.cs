using System;
using System.Collections.Generic;

namespace GoogleSheet.Type
{
    [Type(typeof(List<int>), "List<Int32>")]
    public class IntListType : IType
    {
        public object DefaultValue => new List<int>(); // �⺻���� �� ����Ʈ

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
                    // ���� ó��: �Ľ� ���� �� ���ܸ� �����ų� �⺻���� ��ȯ�� �� �ֽ��ϴ�.
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
