using MancalaAssessment.Model;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MancalaAssessment.Views
{
    /// <summary>
    /// Interaction logic for Pit.xaml
    /// </summary>
    public partial class Pit : Button
    {


        public PitModel PitModel
        {
            get { return (PitModel)GetValue(PitModelProperty); }
            set { SetValue(PitModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PitModelProperty =
            DependencyProperty.Register(nameof(PitModel), typeof(PitModel), typeof(Pit), new PropertyMetadata(null));


        public SolidColorBrush Stroke
        {
            get { return (SolidColorBrush)GetValue(StrokeProperty); }
            set { SetValue(StrokeProperty, value); }
        }

        public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(
            nameof(Stroke),
            typeof(SolidColorBrush),
            typeof(Pit),
            new PropertyMetadata(Brushes.Black)
        );

        public int Stones
        {
            get { return (int)GetValue(StonesProperty); }
            set { SetValue(StonesProperty, value); }
        }

        public static readonly DependencyProperty StonesProperty = DependencyProperty.Register(
            nameof(Stones),
            typeof(int),
            typeof(Pit),
            new PropertyMetadata(0)
        );

        public Pit()
        {
            InitializeComponent();
        }
    }
}
