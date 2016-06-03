using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodFactory
{
    public class TestFactory : ITestFactory
    {
        Dictionary<CalculatorEnum, Type> dic;

        public TestFactory(Dictionary<string,string> calculatorDic)
        {
            dic = ConvertStringsDictToDictOfTypes(calculatorDic);
            CheckIfAllValuesFromDictImplementsProperInterface();
        }

        public ICalculator GetCalculator(CalculatorEnum calculatorEnum)
        {
            Type calculator;

            if (!dic.TryGetValue(calculatorEnum, out calculator))
            {
                throw new NotImplementedException("There is no implementation of IAccountDiscountCalculatorFactory interface for given Account Status");
            }

            return (ICalculator)Activator.CreateInstance(calculator);
        }

        private void CheckIfAllValuesFromDictImplementsProperInterface()
        {
            foreach (var item in dic)
            {
                if (!typeof(ICalculator).IsAssignableFrom(item.Value))
                {
                    throw new ArgumentException("The type: " + item.Value.FullName + " does not implement IAccountDiscountCalculatorFactory interface!");
                }
            }
        }

        private Dictionary<CalculatorEnum, Type> ConvertStringsDictToDictOfTypes(Dictionary<string, string> calculatorDic)
        {
            return calculatorDic.ToDictionary(
                                    t => (CalculatorEnum)Enum.Parse(typeof(CalculatorEnum), t.Key, true),
                                    t => Type.GetType(t.Value));
        }
    }
}
