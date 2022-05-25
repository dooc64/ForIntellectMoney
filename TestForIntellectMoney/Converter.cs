using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestForIntellectMoney
{
    public class Converter
    {
        // Потому что словарь хорошо подходит для перевода цифр в слова, думал также о енаме.
        private Dictionary<int, string> _one = new Dictionary<int, string>()
        {
            {0, "zero " },
            {1, "one " },
            {2, "two " },
            {3, "three " },
            {4, "four " },
            {5, "five " },
            {6, "six " },
            {7, "seven " },
            {8, "eight " },
            {9, "nine " }
        };
        private Dictionary<int, string> _tens = new Dictionary<int, string>()
        {
            {1, "eleven, " },
            {2, "twelve, " },
            {3, "thirteen, " },
            {4, "fourteen, " },
            {5, "fifteen, " },
            {6, "sixteen, " },
            {7, "seventeen, " },
            {8, "eighteen, " },
            {9, "nineteen, " }
        };
        private Dictionary<int, string> _dozens = new Dictionary<int, string>()
        {
            {1, "ten " },
            {2, "twenty " },
            {3, "thirty " },
            {4, "fourty " },
            {5, "fifty " },
            {6, "sixty " },
            {7, "seventy " },
            {8, "eighty " },
            {9, "ninety " }
        };
        private Dictionary<int, string> _hundred = new Dictionary<int, string>()
        {
            {1, "one hundred and " },
            {2, "two hundred and " },
            {3, "three hundred and " },
            {4, "four hundred and " },
            {5, "five hundred and " },
            {6, "six hundred and " },
            {7, "seven hundred and " },
            {8, "eight hundred and " },
            {9, "nine hundred and " }
        };
        private Dictionary<int, string> _thousand = new Dictionary<int, string>()
        {
            {1, "one thousand, " },
            {2, "two thousand, " },
            {3, "three thousand, " },
            {4, "four thousand, " },
            {5, "five thousand, " },
            {6, "six thousand, " },
            {7, "seven thousand, " },
            {8, "eight thousand, " },
            {9, "nine thousand, " }
        };
        private Dictionary<int, string> _million = new Dictionary<int, string>()
        {
            {1, "one million, " },
            {2, "two million, " },
            {3, "three million, " },
            {4, "four million, " },
            {5, "five million, " },
            {6, "six million, " },
            {7, "seven million, " },
            {8, "eight million, " },
            {9, "nine million, " }
        };

        // Инкапсулировал отдельный метод, т.к он нужен только тут и нету смысла делать его публичным
        private void ConvertCents(StringBuilder result, double sum)
        {
            result.Append("AND ");
            string numericString = sum.ToString();

            string cents = numericString.Substring(numericString.LastIndexOf(",") + 1);

            var centsArray = cents.ToCharArray();

            for (int i = 0; i < cents.Length; i++)
            {
                switch (i)
                {
                    case 1:
                        if (int.Parse(centsArray[i].ToString()) == 1 && int.Parse(centsArray[i + 1].ToString()) != 0)
                        {
                            result.Append(_tens[int.Parse(centsArray[i].ToString())]);
                            break;
                        }
                        result.Append(_one[int.Parse(centsArray[i].ToString())]);
                        break;
                    case 0:
                        if (int.Parse(centsArray[i + 1].ToString()) == 1)
                        {
                            break;
                        }
                        result.Append(_dozens[int.Parse(centsArray[i].ToString())]);
                        break;
                    default:
                        continue;
                }
            }

            result.Append("CENTS");
        }

        // Думаю можно было бы попилить ещё на 2 метода и сделать один из них приватным по типу публичный принимает два числа
        // А другие уже обрабатывают это всё.
        public string Start(double sum)
        {
            string numericString = string.Empty;

            if (sum.ToString().Contains(","))
            {
                numericString = sum.ToString().Substring(0, sum.ToString().LastIndexOf(","));
            }
            else
            {
                numericString = sum.ToString();
            }
            char[] numerics = numericString.ToCharArray();
            StringBuilder sb = new StringBuilder();
            // Потому что так удобнее работать с десятками, сотками и т.п
            var listOfNumbers = new List<List<string>>()
            {
                new List<string>(),
                new List<string>(),
                new List<string>()
            };
            
            int countForList = 1;

            StringBuilder result = new StringBuilder();
            for (int i = numerics.Length - 1; i >= 0; i--)
            {
                if (countForList < 4)
                {
                    listOfNumbers[0].Add(numerics[i].ToString());
                }
                if (countForList > 3 && countForList < 7)
                {
                    listOfNumbers[1].Add(numerics[i].ToString());
                }
                if (countForList > 6 && countForList < 10)
                {
                    listOfNumbers[2].Add(numerics[i].ToString());
                }
                if (countForList >= 10)
                {
                    result.Append(_one[int.Parse(numerics[i].ToString())] + "billion, ");
                }
                countForList++;
            }

            for (int i = listOfNumbers.Count - 1; i >= 0; i--)
            {
                for (int y = listOfNumbers[i].Count - 1; y >= 0; y--)
                {
                    //Millions
                    if (i == 2)
                    {
                        switch (y)
                        {
                            case 2:
                                result.Append(_hundred[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            case 1:
                                if (int.Parse(listOfNumbers[i][y]) == 1 && int.Parse(listOfNumbers[i][y - 1]) != 0)
                                {
                                    result.Append(_tens[int.Parse(listOfNumbers[i][y].ToString())]);
                                    break;
                                }
                                result.Append(_dozens[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            case 0:
                                result.Append(_million[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            default:
                                continue;
                        }
                    }
                    //Thousand
                    if (i == 1)
                    {
                        switch (y)
                        {
                            case 2:
                                result.Append(_hundred[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            case 1:
                                if (int.Parse(listOfNumbers[i][y]) == 1 && int.Parse(listOfNumbers[i][y - 1]) != 0)
                                {
                                    result.Append(_tens[int.Parse(listOfNumbers[i][y].ToString())]);
                                    y--;
                                    break;
                                }
                                result.Append(_dozens[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            case 0:
                                result.Append(_thousand[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            default:
                                continue;
                        }
                    }
                    //hundred
                    if (i == 0)
                    {
                        switch (y)
                        {
                            case 2:
                                result.Append(_hundred[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            case 1:
                                if (int.Parse(listOfNumbers[i][y]) == 1 && int.Parse(listOfNumbers[i][y - 1]) != 0)
                                {
                                    result.Append(_tens[int.Parse(listOfNumbers[i][y].ToString())]);
                                    break;
                                }
                                result.Append(_dozens[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            case 0:
                                result.Append(_one[int.Parse(listOfNumbers[i][y].ToString())]);
                                break;
                            default:
                                continue;
                        }
                    }
                } 
            }

            result.Append("DOLLARS ");

            if (sum.ToString().Contains(","))
            {
                ConvertCents(result, sum);
            }

            return result.ToString();
        }
    }
}
