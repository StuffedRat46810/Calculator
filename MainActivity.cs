using Android.App;
using Android.OS;
using Android.Widget;



namespace Calculator
{
    [Activity(Label = "@string/app_name", Theme = "@android:style/Theme.Material.Light", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private TextView resultTextView;
        private Button[] numberButtons = new Button[10];
        private Button buttonPlus, buttonMinus, buttonMultiply, buttonDivide, buttonEquals, buttonDecimal, buttonDelete, buttonClear;
        private string currentNumber = "";
        private string currentOperator = "";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            // Initialize the result TextView
            resultTextView = FindViewById<TextView>(Resource.Id.tv1);

            // Initialize number buttons
            for (int i = 0; i <= 9; i++)
            {
                string buttonID = "button" + i;
                int resID = Resources.GetIdentifier(buttonID, "id", PackageName);
                numberButtons[i] = FindViewById<Button>(resID);
                numberButtons[i].Click += NumberButton_Click;
            }

            // Initialize operator buttons
            buttonPlus = FindViewById<Button>(Resource.Id.buttonPlus);
            buttonMinus = FindViewById<Button>(Resource.Id.buttonMinus);
            buttonMultiply = FindViewById<Button>(Resource.Id.buttonMultiply);
            buttonDivide = FindViewById<Button>(Resource.Id.buttonDivide);
            buttonEquals = FindViewById<Button>(Resource.Id.buttonEquals);
            buttonDecimal = FindViewById<Button>(Resource.Id.buttonDecimal);
            buttonDelete = FindViewById<Button>(Resource.Id.buttonDelete);
            buttonClear = FindViewById<Button>(Resource.Id.buttonClear);

            // Set click events for operator buttons
            buttonPlus.Click += OperatorButton_Click;
            buttonMinus.Click += OperatorButton_Click;
            buttonMultiply.Click += OperatorButton_Click;
            buttonDivide.Click += OperatorButton_Click;
            buttonEquals.Click += EqualsButton_Click;
            buttonDecimal.Click += DecimalButton_Click;
            buttonDelete.Click += DeleteButton_Click;
            buttonClear.Click += ClearButton_Click;
        }

        private void NumberButton_Click(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            string pressed = button.Text;
            string currentText = resultTextView.Text;
            if (currentText == "0")
            {
                resultTextView.Text = pressed;
            }
            else
            {
                resultTextView.Text += pressed;
            }

        }

        private void OperatorButton_Click(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            currentOperator = button.Text;

            if (!string.IsNullOrEmpty(resultTextView.Text))
            {
                currentNumber = resultTextView.Text;
                resultTextView.Text = "0";  // Reset for next input
            }
        }

        private void EqualsButton_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber) && !string.IsNullOrEmpty(currentOperator))
            {
                double firstNumber = double.Parse(currentNumber);
                double secondNumber = double.Parse(resultTextView.Text);
                double result = 0;
                switch (currentOperator)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "x":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber;
                        }
                        else
                        {
                            resultTextView.Text = "Error: Divide by zero";
                            currentNumber = "";
                            currentOperator = "";
                            return;
                        }
                        break;
                }
                resultTextView.Text = result.ToString();
                currentNumber = "";
                currentOperator = "";
            }
        }

        private void DecimalButton_Click(object sender, System.EventArgs e)
        {
            // Placeholder for decimal button click logic
            bool hasDecimal = false;
            for (int i = 0; i < resultTextView.Text.Length; i++)
            {
                if (resultTextView.Text[i] == '.')
                    hasDecimal = true;
            }
            if (!hasDecimal)
                resultTextView.Text += ".";
        }

        private void DeleteButton_Click(object sender, System.EventArgs e)
        {
            string currentText = resultTextView.Text;

            if (!string.IsNullOrEmpty(currentText))
            {
                // Remove the last character
                currentText = currentText.Substring(0, currentText.Length - 1);

                // If all characters are removed, set text to "0"
                if (string.IsNullOrEmpty(currentText))
                {
                    resultTextView.Text = "0";
                }
                else
                {
                    resultTextView.Text = currentText;
                }
            }
        }
        private void ClearButton_Click(object sender, System.EventArgs e)
        {
            if (!string.IsNullOrEmpty(resultTextView.Text))
            {
                resultTextView.Text = "0";
            }
        }

    }
}