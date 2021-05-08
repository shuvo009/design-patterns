using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace design_patterns
{
    //https://refactoring.guru/design-patterns/abstract-factory
    public class AbstractFactory
    {
        [Theory]
        [InlineData("win", "Win Button", "Win CheckBox")]
        [InlineData("mac", "Mac Button", "Mac CheckBox")]
        public void Test(string os, string button, string checkbox)
        {
            IGuiFactory abstractFactory = os == "win" 
                ? new WinGuiFactory() 
                : new MacGGuiFactor();

            var application = new Application(abstractFactory);

            Assert.Equal(button, application.GetButton());
            Assert.Equal(checkbox, application.GetCheckBox());
        }
    }

    public class Application
    {
        private readonly IGuiFactory _guiFactory;

        public Application(IGuiFactory guiFactory)
        {
            _guiFactory = guiFactory;
        }

        public string GetButton()
        {
            return _guiFactory.CreateButton().Print();
        }

        public string GetCheckBox()
        {
            return _guiFactory.CreateCheckBox().Print();
        }
    }

    #region Interfaces

    public interface IButton
    {
        string Print();
    }

    public interface ICheckBox
    {
        string Print();
    }

    public interface IGuiFactory
    {
        IButton CreateButton();
        ICheckBox CreateCheckBox();
    }

    #endregion

    #region Implementation

    public class WinButton : IButton
    {
        public string Print()
        {
            return "Win Button";
        }
    }

    public class WinCheckBox : ICheckBox
    {
        public string Print()
        {
            return "Win CheckBox";
        }
    }

    public class MacButton : IButton
    {
        public string Print()
        {
            return "Mac Button";
        }
    }

    public class MacCheckBox : ICheckBox
    {
        public string Print()
        {
            return "Mac CheckBox";
        }
    }

    public class WinGuiFactory : IGuiFactory
    {
        public IButton CreateButton()
        {
            return new WinButton();
        }

        public ICheckBox CreateCheckBox()
        {
            return new WinCheckBox();
        }
    }

    public class MacGGuiFactor : IGuiFactory
    {
        public IButton CreateButton()
        {
            return new MacButton();
        }

        public ICheckBox CreateCheckBox()
        {
            return new MacCheckBox();
        }
    }
    #endregion

}
