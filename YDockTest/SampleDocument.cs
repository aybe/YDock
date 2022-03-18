using System.Windows.Controls;
using System.Windows.Media;
using YDock.Interface;

namespace YDockTest;

public class SampleDocument : TextBlock, IDockSource
// TODO update interface with nullable properties where appropriate
{
    public SampleDocument(string header)
    {
        Header = header;
    }

    public IDockControl DockControl { get; set; } = null!;

    public string Header { get; }

    public ImageSource Icon { get; init; } = null!;
}