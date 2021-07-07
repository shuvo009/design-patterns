using System;
using Xunit;

namespace design_patterns
{
    public class Strategy
    {
        [Theory]
        [InlineData(1, 1, 2, "+")]
        [InlineData(1, 1, 0, "-")]
        [InlineData(1, 1, 1, "*")]
        [InlineData(4, 2, 2, "/")]
        public void TestCalculator(int input1, int input2, int result, string operation)
        {
            var context = new CalculatorContext();
            switch (operation)
            {
                case "+":
                    context.SetOperationStrategy(new AddOperation());
                    break;
                case "-":
                    context.SetOperationStrategy(new SubtractOperation());
                    break;
                case "*":
                    context.SetOperationStrategy(new MultiplyOperation());
                    break;
                case "/":
                    context.SetOperationStrategy(new DivideOperation());
                    break;
            }

            var output = context.Execute(input1, input2);

            Assert.Equal(result, output);
        }
    }

    public class CalculatorContext
    {
        private ICalculatorOperation _calculatorOperation;

        public void SetOperationStrategy(ICalculatorOperation calculatorOperation)
        {
            _calculatorOperation = calculatorOperation;
        }

        public int Execute(int input1, int input2)
        {
            if (_calculatorOperation == null)
                throw new Exception("Strategy is not set");

            return _calculatorOperation.Execute(input1, input2);
        }
    }

    public interface ICalculatorOperation
    {
        int Execute(int input1, int input2);
    }

    #region Stages

    public class AddOperation : ICalculatorOperation
    {
        public int Execute(int input1, int input2)
        {
            return input1 + input2;
        }
    }

    public class SubtractOperation : ICalculatorOperation
    {
        public int Execute(int input1, int input2)
        {
            return input1 - input2;
        }
    }

    public class MultiplyOperation : ICalculatorOperation
    {
        public int Execute(int input1, int input2)
        {
            return input1 * input2;
        }
    }

    public class DivideOperation : ICalculatorOperation
    {
        public int Execute(int input1, int input2)
        {
            return input1 / input2;
        }
    }

    #endregion
}