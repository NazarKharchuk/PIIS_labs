using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIS_labs
{
    class RabinKarpAlgorithm
    {
        private string text;
        private string template;

        public RabinKarpAlgorithm(string _text, string _template)
        {
            text = _text;
            template = _template;
        }

        public int search()
        {
            int count = 0;
            int template_heuristic = calculate_hash(template);
            int text_heuristic;
            string sub_str;

            for (int i = 0; i < (text.Length - template.Length + 1); i++)
            {
                sub_str = text.Substring(i, template.Length);
                text_heuristic = calculate_hash(sub_str);
                if(template_heuristic == text_heuristic)
                {
                    if(sub_str == template)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int calculate_hash(string str)
        {
            int digit = 0;
            int num;

            for (int i = 0; i < str.Length; i++)
            {
                num = (int)str[i];
                /*if (num < 48 || num > 57)
                {
                    for(int j = 0; j < num.ToString().Length; j++)
                    {
                        digit *= 10;
                    }
                    digit += num;
                }
                else
                {
                    digit = digit * 10 + num - 48;
                }*/
                digit += num;
            }
            //Console.WriteLine(str + " - " + digit);
            return (digit % 25);
        }
    }
}
