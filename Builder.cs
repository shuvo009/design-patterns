using Xunit;

namespace design_patterns
{
    public class Builder
    {
        [Theory]
        [InlineData("Sport", "2,V12,GTR")]
        [InlineData("Family", "4,V6,BMW")]
        public void Test(string carType, string result)
        {
            var application = new BuilderApplication();
            Assert.Equal(result, application.GetCar(carType));
        }
    }

    #region Interface

    public interface IBuilder
    {
        void SetSeat(int numberOfSeat);
        void SetEngine(string engineName);
        void SetSteering(string steering);
        void Reset();
    }

    #endregion

    #region Implementation

    public class BuilderApplication
    {
        public string GetCar(string carType)
        {
            var builder = new CarBuilder();
            var director = new Director(builder);

            if (carType == "Sport")
                director.BuildSportCar();
            else
                director.BuildFamilyCar();

            return builder.GetCar.ToString();
        }
    }

    public class Car
    {
        public int Seat { get; set; }
        public string Engine { get; set; }
        public string Steering { get; set; }

        public override string ToString()
        {
            return $"{Seat},{Engine},{Steering}";
        }
    }

    public class CarBuilder : IBuilder
    {
        public Car GetCar { get; private set; } = new();

        public void SetSeat(int numberOfSeat)
        {
            GetCar.Seat = numberOfSeat;
        }

        public void SetEngine(string engineName)
        {
            GetCar.Engine = engineName;
        }

        public void SetSteering(string steering)
        {
            GetCar.Steering = steering;
        }

        public void Reset()
        {
            GetCar = new Car();
        }
    }

    public class Director
    {
        private readonly IBuilder _builder;

        public Director(IBuilder builder)
        {
            _builder = builder;
        }

        public void BuildSportCar()
        {
            _builder.Reset();
            _builder.SetSeat(2);
            _builder.SetEngine("V12");
            _builder.SetSteering("GTR");
        }

        public void BuildFamilyCar()
        {
            _builder.Reset();
            _builder.SetSeat(4);
            _builder.SetEngine("V6");
            _builder.SetSteering("BMW");
        }
    }

    #endregion
}