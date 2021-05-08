using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace design_patterns
{
    //https://refactoring.guru/design-patterns/factory-method
    public class FactoryMethod
    {
        [Theory]
        [InlineData("web", nameof(WebDialogButton))]
        [InlineData("win", nameof(WinDialogButton))]
        public void Test(string platform, string renderedText)
        {
            var application = new FactoryMethodApplication();
            Assert.Equal(renderedText, application.RenderDialog(platform));
        }
    }

    public class FactoryMethodApplication
    {
        public string RenderDialog(string platform)
        {
            Dialog dialog = platform == "web"
                ? new WebDialog()
                : new WinDialog();

            return dialog.Render();
        }
    }

    #region Abstract Class

    public abstract class Dialog
    {
        protected abstract IDialogButton CreateButton();

        public string Render()
        {
            var button = CreateButton();
            return button.Render();
        }
    }

    #endregion

    #region Interface

    public interface IDialogButton
    {
        string Render();
    }

    #endregion

    #region Implementation

    public class WinDialogButton : IDialogButton
    {
        public string Render()
        {
            return nameof(WinDialogButton);
        }
    }

    public class WebDialogButton : IDialogButton
    {
        public string Render()
        {
            return nameof(WebDialogButton);
        }
    }

    public class WinDialog : Dialog
    {
        protected override IDialogButton CreateButton()
        {
            return new WinDialogButton();
        }
    }

    public class WebDialog : Dialog
    {
        protected override IDialogButton CreateButton()
        {
            return new WebDialogButton();
        }
    }

    #endregion
}
