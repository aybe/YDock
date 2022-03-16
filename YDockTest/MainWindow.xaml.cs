using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows;
using System.Xml.Linq;
using YDock.Enum;
using YDock.LayoutSetting;

namespace YDockTest;

public partial class MainWindow
    // BUG at initialization (without a layout file), one tool in two is on the opposite side
    // BUG at layout file reload, the height of docked tools is not preserved
{
    private const string LayoutName = "MainWindow";

    private const string LayoutPath = "Layout.xml";

    [SuppressMessage("ReSharper", "RedundantArgumentDefaultValue")]
    public MainWindow()
    {
        InitializeComponent();

        Document1 = new SampleDocument("Document 1");
        Document2 = new SampleDocument("Document 2");
        Document3 = new SampleDocument("Document 3");
        Document4 = new SampleDocument("Document 4");

        LeftTool1 = new SampleDocument("Left Tool 1");
        LeftTool2 = new SampleDocument("Left Tool 2");

        TopTool1 = new SampleDocument("Top Tool 1");
        TopTool2 = new SampleDocument("Top Tool 2");

        RightTool1 = new SampleDocument("Right Tool 1");
        RightTool2 = new SampleDocument("Right Tool 2");

        BottomTool1 = new SampleDocument("Bottom Tool 1");
        BottomTool2 = new SampleDocument("Bottom Tool 2");

        DockManager.RegisterDocument(Document1);
        DockManager.RegisterDocument(Document2);
        DockManager.RegisterDocument(Document3);
        DockManager.RegisterDocument(Document4);

        DockManager.RegisterDock(LeftTool1, DockSide.Left);
        DockManager.RegisterDock(LeftTool2, DockSide.Left);

        DockManager.RegisterDock(TopTool1, DockSide.Top);
        DockManager.RegisterDock(TopTool2, DockSide.Top);

        DockManager.RegisterDock(RightTool1, DockSide.Right);
        DockManager.RegisterDock(RightTool2, DockSide.Right);

        DockManager.RegisterDock(BottomTool1, DockSide.Bottom);
        DockManager.RegisterDock(BottomTool2, DockSide.Bottom);
    }

    private SampleDocument Document1 { get; }

    private SampleDocument Document2 { get; }

    private SampleDocument Document3 { get; }

    private SampleDocument Document4 { get; }

    private SampleDocument LeftTool1 { get; }

    private SampleDocument LeftTool2 { get; }

    private SampleDocument TopTool1 { get; }

    private SampleDocument TopTool2 { get; }

    private SampleDocument RightTool1 { get; }

    private SampleDocument RightTool2 { get; }

    private SampleDocument BottomTool1 { get; }

    private SampleDocument BottomTool2 { get; }

    private bool TryLoadLayout()
    {
        if (!File.Exists(LayoutPath))
            return false;

        var text = File.ReadAllText(LayoutPath);

        var document = XDocument.Parse(text);

        if (document.Root is null)
            return false;

        foreach (var item in document.Root.Elements())
        {
            var name = item.Attribute("Name")?.Value ?? throw new InvalidDataException();

            if (DockManager.Layouts.ContainsKey(name))
            {
                DockManager.Layouts[name].Load(item);
            }
            else
            {
                DockManager.Layouts[name] = new LayoutSetting(name, item);
            }
        }

        DockManager.ApplyLayout(LayoutName);

        return true;
    }

    private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
    {
        if (TryLoadLayout())
            return;

        Document1.DockControl.Show();
        Document2.DockControl.Show();
        Document3.DockControl.Show();
        Document4.DockControl.Show();

        LeftTool1.DockControl.Show();
        LeftTool2.DockControl.Show();

        TopTool1.DockControl.Show();
        TopTool2.DockControl.Show();

        RightTool1.DockControl.Show();
        RightTool2.DockControl.Show();

        BottomTool1.DockControl.Show();
        BottomTool2.DockControl.Show();
    }

    private void MainWindow_OnClosed(object? sender, EventArgs e)
    {
        DockManager.SaveCurrentLayout(LayoutName);

        var document = new XDocument();

        var rootNode = new XElement("Layouts");

        foreach (var layout in DockManager.Layouts.Values)
        {
            layout.Save(rootNode);
        }

        document.Add(rootNode);

        document.Save(LayoutPath);

        DockManager.Dispose();
    }

    private void MenuItemFileClose_OnClick(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }

    private void LeftTool1MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        LeftTool1.DockControl.Show();
    }

    private void LeftTool2MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        LeftTool2.DockControl.Show();
    }

    private void TopTool1MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        TopTool1.DockControl.Show();
    }

    private void TopTool2MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        TopTool2.DockControl.Show();
    }

    private void RightTool1MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        RightTool1.DockControl.Show();
    }

    private void RightTool2MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        RightTool2.DockControl.Show();
    }

    private void BottomTool1MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        BottomTool1.DockControl.Show();
    }

    private void BottomTool2MenuItem_OnClick(object sender, RoutedEventArgs e)
    {
        BottomTool2.DockControl.Show();
    }

    private void Document1_OnClick(object sender, RoutedEventArgs e)
    {
        Document1.DockControl.Show();
    }

    private void Document2_OnClick(object sender, RoutedEventArgs e)
    {
        Document2.DockControl.Show();
    }

    private void Document3_OnClick(object sender, RoutedEventArgs e)
    {
        Document3.DockControl.Show();
    }

    private void Document4_OnClick(object sender, RoutedEventArgs e)
    {
        Document4.DockControl.Show();
    }
}