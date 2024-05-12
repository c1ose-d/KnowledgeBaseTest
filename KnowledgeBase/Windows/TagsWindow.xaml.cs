namespace KnowledgeBase.Windows;

public partial class TagsWindow : Window
{
    public TagsWindow()
    {
        InitializeComponent();
    }

    private List<Tag> Tags { get; set; } = [.. new KnowledgeBaseContext().Tags];

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
                MaxWidth = 456,
                Style = (Style)Application.Current.FindResource("TextBox")
            };
            textBox.TextChanged += TextBox_TextChanged;

            ListView.Items.Clear();
            ListView.Items.Add(textBox);
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
            knowledgeBaseContext.Tags.Add(new()
            {
                Value = ((TextBox)ListView.SelectedItem).Text
            });
            knowledgeBaseContext.SaveChanges();

            Refresh();
        }
    }

    private void Edit_Click(object sender, RoutedEventArgs e)
    {
        TextBox? textBox = (TextBox)ListView.SelectedItem;
        if (textBox != null)
        {
            CurrentSelect = ListView.SelectedIndex;

            if (Edit.Content.ToString() == EditIcon)
            {
                textBox.IsEnabled = true;
                _ = textBox.Focus();

                Edit.Style = (Style)Application.Current.FindResource("Button.Accent.IconOnly");
                Edit.Content = AcceptIcon;
                Add.IsEnabled = false;

                Cancel.Visibility = Visibility.Visible;
            }
            else
            {
                textBox.IsEnabled = false;

                Edit.Style = (Style)Application.Current.FindResource("Button.Standart.IconOnly");
                Edit.Content = EditIcon;

                KnowledgeBaseContext knowledgeBaseContext = new();

                Tag? tag = knowledgeBaseContext.Tags.FirstOrDefault(x => x == textBox.DataContext);

                if (tag != null)
                {
                    tag.Value = textBox.Text;
                    knowledgeBaseContext.SaveChanges();
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
            Tag? tag = knowledgeBaseContext.Tags.FirstOrDefault(x => x == (Tag)textBox.DataContext);
            if (tag != null)
            {
                knowledgeBaseContext.Tags.Remove(tag);
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

                Tag? tag = knowledgeBaseContext.Tags.FirstOrDefault(x => x == textBox.DataContext);

                if (tag != null)
                {
                    tag.Value = textBox.Text;
                    knowledgeBaseContext.SaveChanges();
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

        if (textBox.Text != string.Empty && Add.Content.ToString() == "")
        {
            Add.IsEnabled = true;
        }
        else
        {
            Add.IsEnabled = false;
        }

        if (textBox.Text != string.Empty && Edit.Content.ToString() == "")
        {
            Edit.IsEnabled = true;
        }
        else
        {
            Edit.IsEnabled = false;
        }
    }

    private void Refresh()
    {
        ListView.Items.Clear();

        Tags = [.. new KnowledgeBaseContext().Tags];
        foreach (Tag tag in Tags)
        {
            TextBox textBox = new()
            {
                DataContext = tag,
                IsEnabled = false,
                MaxWidth = 456,
                Style = (Style)Application.Current.FindResource("TextBox"),
                Text = tag.Value
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