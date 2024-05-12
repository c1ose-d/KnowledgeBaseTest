namespace KnowledgeBase.Windows;

public partial class AnswersWindow : Window
{
    public AnswersWindow()
    {
        InitializeComponent();
    }

    private List<Answer> Answers { get; set; } = [.. new KnowledgeBaseContext().Answers];

    private int CurrentSelect { get; set; }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        if (Add.Content.ToString() == "")
        {
            TextBox textBox = new()
            {
                Height = 60,
                MaxWidth = 456,
                Style = (Style)Application.Current.FindResource("TextBox"),
                TextWrapping = TextWrapping.Wrap
            };
            textBox.TextChanged += TextBox_TextChanged;

            ListView.Items.Clear();
            _ = ListView.Items.Add(textBox);
            _ = textBox.Focus();

            Add.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
            Add.Content = "";
            Add.IsEnabled = false;
            Edit.IsEnabled = false;
            ListView.SelectedIndex = 0;

            Cancel.Visibility = Visibility.Visible;
        }
        else
        {
            Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Add.Content = "";
            Add.IsEnabled = true;
            Edit.IsEnabled = true;

            KnowledgeBaseContext knowledgeBaseContext = new();
            _ = knowledgeBaseContext.Answers.Add(new()
            {
                Value = ((TextBox)ListView.SelectedItem).Text
            });
            _ = knowledgeBaseContext.SaveChanges();

            Refresh();
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        TextBox? textBox = (TextBox)ListView.SelectedItem;
        if (textBox != null)
        {
            CurrentSelect = ListView.SelectedIndex;

            if (Edit.Content.ToString() == "")
            {
                textBox.IsEnabled = true;
                _ = textBox.Focus();

                Edit.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Edit.Content = "";
                Add.IsEnabled = false;

                Cancel.Visibility = Visibility.Visible;
            }
            else
            {
                textBox.IsEnabled = false;

                Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                Edit.Content = "";

                KnowledgeBaseContext knowledgeBaseContext = new();

                Answer? answer = knowledgeBaseContext.Answers.FirstOrDefault(x => x == textBox.DataContext);

                if (answer != null)
                {
                    answer.Value = textBox.Text;
                    _ = knowledgeBaseContext.SaveChanges();
                }

                Refresh();
            }
        }
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        TextBox? textBox = (TextBox)ListView.SelectedItem;
        if (textBox != null)
        {
            KnowledgeBaseContext knowledgeBaseContext = new();
            Answer? answer = knowledgeBaseContext.Answers.FirstOrDefault(x => x == (Answer)textBox.DataContext);
            if (answer != null)
            {
                knowledgeBaseContext.Answers.Remove(answer);
                _ = knowledgeBaseContext.SaveChanges();
            }
        }

        if (Edit.Content.ToString() == "" || Add.Content.ToString() == "")
        {
            Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Edit.Content = "";
            Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
            Add.Content = "";
            Edit.IsEnabled = true;
            Add.IsEnabled = true;
        }

        Refresh();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        Refresh();
    }

    private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        TextBox? textBox = (TextBox)ListView.SelectedItem;
        if (textBox != null)
        {
            CurrentSelect = ListView.SelectedIndex;

            if (Edit.Content.ToString() == "")
            {
                textBox.IsEnabled = true;
                _ = textBox.Focus();

                Edit.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Edit.Content = "";
                Add.IsEnabled = false;

                Cancel.Visibility = Visibility.Visible;
            }
            else
            {
                textBox.IsEnabled = false;

                Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                Edit.Content = "";

                KnowledgeBaseContext knowledgeBaseContext = new();

                Answer? answer = knowledgeBaseContext.Answers.FirstOrDefault(x => x == textBox.DataContext);

                if (answer != null)
                {
                    answer.Value = textBox.Text;
                    _ = knowledgeBaseContext.SaveChanges();
                }

                Refresh();
            }
        }
    }

    private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (Edit.Content.ToString() == "")
        {
            ListView.SelectedIndex = CurrentSelect;
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox textBox = (TextBox)sender;

        Add.IsEnabled = textBox.Text != string.Empty && Add.Content.ToString() == "";

        Edit.IsEnabled = textBox.Text != string.Empty && Edit.Content.ToString() == "";
    }

    private void Refresh()
    {
        ListView.Items.Clear();

        Answers = [.. new KnowledgeBaseContext().Answers];
        foreach (Answer answer in Answers)
        {
            TextBox textBox = new()
            {
                DataContext = answer,
                IsEnabled = false,
                Height = 60,
                MaxWidth = 456,
                Style = (Style)Application.Current.FindResource("TextBox"),
                Text = answer.Value,
                TextWrapping = TextWrapping.Wrap
            };
            textBox.TextChanged += TextBox_TextChanged;

            _ = ListView.Items.Add(textBox);
        }

        Add.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
        Add.Content = "";
        Add.IsEnabled = true;

        Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
        Edit.Content = "";
        Edit.IsEnabled = true;

        Cancel.Visibility = Visibility.Collapsed;
    }
}