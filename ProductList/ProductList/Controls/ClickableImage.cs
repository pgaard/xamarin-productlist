namespace ProductList.Controls
{
    using Xamarin.Forms;

    public class ClickableImage : Image
    {
        public static BindableProperty OnClickProperty = BindableProperty.Create(
            "OnClick",
            typeof(Command),
            typeof(ClickableImage));

        public Command OnClick
        {
            get { return (Command)this.GetValue(OnClickProperty); }
            set { this.SetValue(OnClickProperty, value);}
        }

        public ClickableImage()
        {
            this.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(Tap)});
        }

        private void Tap(object sender)
        {
            if (this.OnClick != null)
            {
                this.OnClick.Execute(sender);
            }
        }
    }
}
