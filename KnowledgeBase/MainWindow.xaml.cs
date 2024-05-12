namespace KnowledgeBase;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Tags_Click(object sender, RoutedEventArgs e)
    {
        new TagsWindow().Show();
    }

    private void Softs_Click(object sender, RoutedEventArgs e)
    {
        new SoftsWindow().Show();
    }

    private void Answers_Click(object sender, RoutedEventArgs e)
    {
        new AnswersWindow().Show();
    }
}