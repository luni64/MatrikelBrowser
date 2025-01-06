class Program
{
    static void Main(string[] args)
    {
        // Find the browser window
        AutomationElement browser = AutomationElement.RootElement
            .FindFirst(TreeScope.Children,
                       new PropertyCondition(AutomationElement.NameProperty, "Your Browser Title"));

        // Find the input field within the browser
        AutomationElement inputField = browser.FindFirst(TreeScope.Descendants,
            new PropertyCondition(AutomationElement.AutomationIdProperty, "inputFieldId"));

        // Set value in the input field
        ValuePattern valuePattern = (ValuePattern)inputField.GetCurrentPattern(ValuePattern.Pattern);
        valuePattern.SetValue("Your Input Text");
    }
}

