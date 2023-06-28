namespace MassiveCore.Framework.Runtime
{
    public class ExampleScreen : Screen
    {
        public AnimatedNumericText CurrencyText => Control<AnimatedNumericText>("currency_text");
        public AnimatedNumericText AutoIncreasedNumericText => Control<AnimatedNumericText>("auto_increased_numeric_text");

        public Button CloseButton => Control<Button>("close_button");
        public Button ShowAppReviewButton => Control<Button>("show_app_review_button");
        public Button PlayVfxButton => Control<Button>("play_vfx_button");
        public Button IncreaseCurrencyButton => Control<Button>("increase_currency_button");
        public Button SpendCurrencyButton => Control<Button>("spend_currency_button");
    }
}
